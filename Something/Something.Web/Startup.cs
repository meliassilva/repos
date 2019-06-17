using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Byui.AppSecrets.Business.BuilderExtensions;
using Byui.AppSecrets.Business.Services;
using Byui.Something.Web.Helpers;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Byui.Something.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        readonly string _contentRootPath;
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            else
            {
                builder.AddAppSecrets();
            }

            Configuration = builder.Build();
            _contentRootPath = env.ContentRootPath;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            if (!_env.IsDevelopment())
            {
                services.Configure<MvcOptions>(options => { options.Filters.Add(new RequireHttpsAttribute()); });
            }

            //string appName = Configuration["AppSettings:AppName"];
            
            services.AddOptions();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            AppSettings appSettings = new AppSettings();

            Configuration.GetSection("AppSettings").Bind(appSettings);

            string thumbprint = Configuration["CertThumb"];
            if (!string.IsNullOrEmpty(appSettings.DataProtectionKeyLocation) && !string.IsNullOrEmpty(thumbprint))
            {
                var cert = RsaEncryptionService.ResolveCertificate(thumbprint);
                services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(appSettings.DataProtectionKeyLocation)).ProtectKeysWithCertificate(cert);
            }

            services.AddSingleton(appSettings);
            services.AddHttpClient();
            services.AddScoped<OAuthClient>();

            if (appSettings.IsUsingOpenIdConnect())
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                services.AddAuthentication(options =>
                    {
                        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    })
                    .AddCookie()
                    .AddCustomOpenIdConnect(o =>
                    {
                        o.MetadataAddress = appSettings.MetadataAddress;
                        o.ClientId = appSettings.ClientId;
                        o.ClientSecret = appSettings.ClientSecret;
                        o.ResponseType = OpenIdConnectResponseType.Code;
                        o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        o.RequireHttpsMetadata = true;
                        o.Scope.Add("openid");
                        o.Scope.Add("profile");
                        o.Scope.Add("roles");
                        o.CallbackPath = new PathString("/signin");
                        o.SaveTokens = true;
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            NameClaimType = JwtClaimTypes.Subject,
                            RoleClaimType = JwtClaimTypes.Role
                        };
                    });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            AppSettings appSettings)
        {
            // Clears the default microsoft namespace for default claim types
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                if (env.IsStaging())
                {
                    app.UseDeveloperExceptionPage();
                }
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            if (appSettings.IsUsingOpenIdConnect())
            {
                app.UseAuthentication();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                // uncomment for non angular SPA app
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}");

                // delete for non angular SPA app
                routes.MapRoute(
                    "CatchAll",
                    "{*url}",
                    new {controller = "Home", action = "Index"}
                );
            });
        }
    }
}