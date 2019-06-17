using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Byui.Something.ApplicationCore.Common.Interfaces.Clock;
using IdentityModel.Client;
using Polly;

namespace Byui.Something.Infrastructure.Common
{
    public class ApiMessageHandler : DelegatingHandler
    {
        private readonly IUserAuthToken _userToken;
        private readonly AuthToken _restAuth;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IClock _clock;
        private const string Bearer = "Bearer";
        private HttpClient _client;

        public ApiMessageHandler(AuthToken restAuth, IHttpClientFactory clientFactory, IClock clock, IUserAuthToken userToken)
        {
            _restAuth = restAuth;
            _clientFactory = clientFactory;
            _clock = clock;
            _userToken = userToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // only use retry policy if user token is not set.
            if (!string.IsNullOrEmpty(_userToken.Token))
            {
                var auth = _userToken.Token.Split(' ');
                if (auth.Length == 2)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue(auth[0], auth[1]);
                    return await base.SendAsync(request, cancellationToken);
                }
            }

            // retry twice, the first time don't wait, the second, wait 2 seconds before trying.  Also, set authtoken to null
            var retryPolicy = Policy.Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                .WaitAndRetryAsync(2,
                    retryAttempt => TimeSpan.FromSeconds((retryAttempt - 1) * 2),
                    (result, span) =>
                    {
                        _restAuth.Token = null;
                    });
            var responseMessage = await retryPolicy.ExecuteAsync(async () =>
            {
                await SetAuthHeader(request);
                return await base.SendAsync(request, cancellationToken);
            });

            return responseMessage;
        }

        private async Task SetAuthHeader(HttpRequestMessage request)
        {
            if (string.IsNullOrEmpty(_restAuth.Token) ||
                _clock.Now.AddMinutes(-1) > _restAuth.Expires)
            {
                try
                {
                    await _restAuth.Lock();

                    var tokenEndpoint = await GetTokenEndpoint();

                    _client = _clientFactory.CreateClient();
                    var response = await _client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                    {
                        Address = tokenEndpoint,
                        ClientId = _restAuth.ClientId,
                        ClientSecret = _restAuth.ClientSecret
                    });

                    if (response.IsError)
                    {
                        throw new Exception(response.Error);
                    }

                    _restAuth.Token = response.AccessToken;
                    _restAuth.RefreshToken = response.RefreshToken;
                    _restAuth.Timestamp = _clock.Now;
                    _restAuth.Expires = _restAuth.Timestamp.AddSeconds(response.ExpiresIn);
                }
                finally
                {
                    _restAuth.Release();
                }
            }

            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, _restAuth.Token);
        }

        private async Task<string> GetTokenEndpoint()
        {
            var cache = new DiscoveryCache(_restAuth.MetadataAddress, client: null, policy: new DiscoveryPolicy
            {
                ValidateEndpoints = false
            });
            var response = await cache.GetAsync();
            return response.TokenEndpoint;
        }
    }
}
