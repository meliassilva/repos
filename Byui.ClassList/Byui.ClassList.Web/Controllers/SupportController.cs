using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Byui.AppSecrets.Business.Services;
using Byui.Byui.ClassList.Web.Helpers;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Byui.Byui.ClassList.Web.Controllers
{
    [AllowAnonymous]
    [Route("support")]
    public class SupportController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly OAuthClient _oAuthClient;

        public SupportController(AppSettings appSettings, OAuthClient oAuthClient)
        {
            _appSettings = appSettings;
            _oAuthClient = oAuthClient;
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }

        [Route("login")]
        public IActionResult Login(string redirectUrl)
        {
            return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = redirectUrl });
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> Refresh()
        {
            if (_appSettings.AllowAnonymous && !User.Identity.IsAuthenticated)
            {
                var accessToken = await _oAuthClient.GetAccessTokenAsync();
                return Ok(new { token = accessToken });
            }

            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            TokenResponse result = await _oAuthClient.RefreshAuthToken(refreshToken);
            if (result.IsError)
                throw new Exception("Error Validating Token: " + result.ErrorDescription);

            // set refresh token in cookie
            var auth = await HttpContext.AuthenticateAsync("Cookies");
            auth.Properties.StoreTokens(new List<AuthenticationToken>
            {
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = result.AccessToken
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = result.RefreshToken
                }
            });

            await HttpContext.SignInAsync(auth.Principal, auth.Properties);

            return Ok(new { token = result.AccessToken });
        }

        //[Route("Unauthorized")]
        //public IActionResult Unauthorized()
        //{
        //    return View();
        //}
    }
}
