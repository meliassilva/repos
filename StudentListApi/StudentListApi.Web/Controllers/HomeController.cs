using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Byui.StudentListApi.Web.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Byui.StudentListApi.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly AppSettings _appSettings;
        private readonly OAuthClient _oAuthClient;

        public HomeController(IHostingEnvironment env, AppSettings appSettings, OAuthClient oAuthClient)
        {
            _env = env;
            _appSettings = appSettings;
            _oAuthClient = oAuthClient;
        }

        public async Task<IActionResult> Index()
        {
            /*
             * To allow anon
             */
            if (!_appSettings.AllowAnonymous && !User.Identity.IsAuthenticated && !(_appSettings.Impersonate && _env.IsDevelopment()))
            {
                return RedirectToAction("Login", "Support", new { redirectUrl = Request.PathBase + Request.Path + Request.QueryString });
            }
            await SetConfig();
            ViewData["BaseHref"] = $"{Request.PathBase}/";
            return View();
        }

        private async Task SetConfig()
        {
            Config config = new Config
            {
                AppRoot = $"{Request.PathBase}/",
                ApiUrl = _appSettings.ApiUrl
            };

            if (_appSettings.Impersonate && _env.IsDevelopment())
            {
                config.User = _appSettings.User;
                config.User.Name = $"{_appSettings.User.RestOfName} {_appSettings.User.Surname}";

                var claims = new List<Claim>
                {
                    new Claim(Constants.Sub, _appSettings.User.Inumber),
                    new Claim(Constants.Email, _appSettings.User.Email),
                    new Claim(Constants.RestOfName, _appSettings.User.RestOfName),
                    new Claim(Constants.Surname, _appSettings.User.Surname)
                };
                // the claims in our app settings need to be BYU-I roles
                claims.AddRange(config.User.ByuiRoles.Select(r => new Claim(Constants.Role, r)));

                // get a JWT with the stuff we impersonated
                config.AuthToken = OAuthClient.GetJwt(claims);

                // now make the roles in our user Application roles
                config.User.Roles = SecurityHelper.ConvertByuiRolesToApplicationRoles(config.User.ByuiRoles);
            }
            else if (User.Identity.IsAuthenticated)
            {
                config.AuthToken = await HttpContext.GetTokenAsync("access_token");

                // need these as the real BYU-Idaho roles for our claims, but then we'll convert them to application roles for the User in the config object
                List<string> byuiRoles = User.Claims.FirstOrDefault(c => c.Type == Constants.Role)?.Value.Split(',').Select(r => r.Trim()).ToList();

                config.User = new User
                {
                    Name = $"{User.Claims.FirstOrDefault(c => c.Type == Constants.RestOfName)?.Value} {User.Claims.FirstOrDefault(c => c.Type == Constants.Surname)?.Value}",
                    Email = User.Claims.FirstOrDefault(c => c.Type == Constants.Email)?.Value,
                    Roles = SecurityHelper.ConvertByuiRolesToApplicationRoles(byuiRoles)
                };
                
                if (_env.IsDevelopment())
                {
                    // if it is development, create a jwt to pass to the api
                    var claims = User.Claims.Where(c => c.Type != Constants.Role).ToList();
                    if (byuiRoles?.Any() == true)
                    {
                        claims.AddRange(byuiRoles.Select(r => new Claim(Constants.Role, r)));
                    }

                    config.Token = config.AuthToken;
                    config.AuthToken = OAuthClient.GetJwt(claims);
                }
            }
            
            // if the user isn't logged in, we need to get our token to use to make our anonymous api calls
            if (_appSettings.AllowAnonymous && config.AuthToken == null)
            {
                config.AuthToken = await _oAuthClient.GetAccessTokenAsync();
            }

            ViewData["Config"] = JsonConvert.SerializeObject(config,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
