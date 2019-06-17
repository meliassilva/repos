using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;

namespace Byui.testapi.Web.Helpers
{
    /// <summary>
    /// Helps us work with our OAuth server.
    /// </summary>
    public class OAuthClient
    {
        private readonly AppSettings _settings;
        private readonly bool _isDev;
        private readonly IHttpClientFactory _clientFactory;

        public OAuthClient(AppSettings settings, IHostingEnvironment env, IHttpClientFactory clientFactory)
        {
            _settings = settings;
            _clientFactory = clientFactory;
            _isDev = env.IsDevelopment();
        }

        /// <summary>
        /// Gets an access token using our client id and client secret.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessTokenAsync()
        {
            if (_isDev)
            {
                return GetJwt();
            }

            var tokenEndpoint = await GetTokenEndpoint();
            var client = _clientFactory.CreateClient();
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = tokenEndpoint,
                ClientId = _settings.ClientId,
                ClientSecret = _settings.ClientSecret
            });

            return response.AccessToken;
        }

        public async Task<TokenResponse> RefreshAuthToken(string refreshToken)
        {
            var tokenEndpoint = await GetTokenEndpoint();
            var client = _clientFactory.CreateClient();
            var response = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = tokenEndpoint,
                ClientId = _settings.ClientId,
                ClientSecret = _settings.ClientSecret,
                RefreshToken = refreshToken
            });
            return response;
        }

        private async Task<string> GetTokenEndpoint()
        {
            var cache = new DiscoveryCache(_settings.MetadataAddress, client: null, policy: new DiscoveryPolicy
            {
                ValidateEndpoints = false
            });
            var discovery = await cache.GetAsync();
            return discovery.TokenEndpoint;
        }

        /// <summary>
        /// Creates a JWT with whatever claims are specified.  This should only be used when developing.
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static string GetJwt(List<Claim> claims = null)
        {
            var jwt = new JwtSecurityToken(claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
