using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Byui.testapi.Enterprise.Interfaces;
using IdentityModel.Client;

namespace Byui.testapi.Enterprise.Helpers.Wso2
{
    /// <summary>
    /// Client code for working with Refit library. Creates the HttpClient to handle Bearer tokens.
    /// </summary>
    public class Wso2ApiClient : HttpClientHandler, IWso2ApiClient
    {
        private readonly IWso2ApiClientSettings _settings;

        public Wso2ApiClient(IWso2ApiClientSettings settings)
        {
            _settings = settings;
        }

        public async Task<string> GetAuthToken()
        {
            try
            {
                var discoveryClient = new DiscoveryClient(_settings.MetadataAddress);
                discoveryClient.Policy.ValidateEndpoints = false;
                var doc = await discoveryClient.GetAsync();
                var tclient = new TokenClient(
                    doc.TokenEndpoint,
                    _settings.ClientId,
                    _settings.ClientSecret
                );
                var response = await tclient.RequestClientCredentialsAsync();
                return response.AccessToken;
            }
            catch
            {
                throw;
            }
        }

        public HttpClient GetClient(string url)
        {
            return new HttpClient((HttpClientHandler)this)
            {
                BaseAddress = new Uri(url)
            };
        }

        public HttpClient GetClient(string url, int seconds)
        {
            return new HttpClient((HttpClientHandler)this)
            {
                BaseAddress = new Uri(url),
                Timeout = new TimeSpan(0, 0, seconds),
            };
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // See if the request has an authorize header
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                var token = await GetAuthToken().ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
