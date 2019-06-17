using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Byui.testapi.Web.Helpers
{
    public class CustomOpenIdConnectHandler : OpenIdConnectHandler
    {
        public CustomOpenIdConnectHandler(IOptionsMonitor<OpenIdConnectOptions> options, ILoggerFactory logger,
            HtmlEncoder htmlEncoder, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, htmlEncoder, encoder, clock)
        {

        }

        // The connection to the WSO2 server is terminated abnormally
        // https://github.com/aspnet/Security/issues/1407
        protected override async Task<OpenIdConnectMessage> RedeemAuthorizationCodeAsync(
            OpenIdConnectMessage tokenEndpointRequest)
        {
            try
            {
                return await base.RedeemAuthorizationCodeAsync(tokenEndpointRequest);
            }
            catch
            {
                return await base.RedeemAuthorizationCodeAsync(tokenEndpointRequest);
            }
        }
    }
}
