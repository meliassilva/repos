﻿using System;
using System.Collections.Generic;
using Byui.StudentList.Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Byui.AppSecrets.Business.BuilderExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Constants = Byui.StudentList.Api.Helpers.Constants;
using Byui.LmsClients.LmsDataClient;
using static Byui.AppSecrets.Business.Cryptor;



namespace Byui.StudentList.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// _env
        /// </summary>
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment() || env.ContentRootPath.Contains("C:\\Users"))
            {
                builder.AddUserSecrets<Startup>();
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.RollingFile("logs\\log-dev-{Date}.log")
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.RollingFile("logs\\log-prod-{Date}.log")
                    .CreateLogger();
            }



            Configuration = builder.Build();
        }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddCors();
            }

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(typeof(ApiExceptionFilter));
                options.Filters.Add(new AuthorizeFilter(policy));
                options.Filters.Add(new ProducesAttribute("application/json"));
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                // todo options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "StudentList API",
                    Description = "API for StudentList application",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "", Email = "" },
                });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);

                //Set the comments path for the swagger json and ui.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            AppSettings appSettings = new AppSettings();
            if (_env.IsDevelopment())
            {
                Log.Information("Development");

                appSettings = new Helpers.AppSettings
                {
                    LmsId = Configuration["LmsId"],
                    LmsSecret = Configuration["LmsSecret"],

                };
            }
            else
            {
                Log.Information("Production");
                appSettings = DecryptFile<AppSettings>(Configuration["CertThumb"]);
                //appSettings.MetadataAddress = Configuration["AppSettings:MetadataAddress"];
                //appSettings.LmsClientBaseUrl = Configuration["AppSettings:LmsClientBaseUrl"];
            }

            Configuration.GetSection("AppSettings").Bind(appSettings);

            services.AddScoped<ApiExceptionFilter>();
            services.AddSingleton(appSettings);

            Log.Information("Added LmsDataClient provider");


            if (_env.IsDevelopment())
            {
                services.AddScoped(provider => new LmsDataClient("", "", "http://localhost:56665"));
            }
            else
            {
                try
                {
                    services.AddScoped(provider => new LmsDataClient(appSettings.LmsId, appSettings.LmsSecret, appSettings.LmsClientBaseUrl));
                    Log.Information("Added LmsDataClient provider");
                }
                catch (Exception ex)
                {
                    Log.Error("Unable to add LmsDataClient provider: (ex)", ex);
                }
            }

            // configure automapper and dependancy injection ** For StudentList.Business
            Business.Utilities.Configuration.Configure(services, Configuration, _env.IsDevelopment());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    if (_env.IsDevelopment())
                    {
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            RequireExpirationTime = false,
                            RequireSignedTokens = false,
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateIssuerSigningKey = false,
                            ValidateLifetime = false
                        };
                    }
                    else
                    {
                        o.IncludeErrorDetails = true;
                        o.MetadataAddress = appSettings.MetadataAddress;
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            RequireExpirationTime = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.FromMinutes(5)
                        };
                    }
                    o.TokenValidationParameters.NameClaimType = Constants.Sub;
                    o.TokenValidationParameters.RoleClaimType = Constants.Role;
                });

            // add authorization policies
            services.AddAuthorization((Action<AuthorizationOptions>)(options =>
            {
                options.AddPolicy(Constants.Admin, (policy =>
                {
                    policy.RequireRole(Constants.Admin);
                }));
                options.AddPolicy((string)Constants.Admin, (Action<AuthorizationPolicyBuilder>)(policy =>
                {
                    policy.RequireRole((string[])Constants.Admins);
                }));
                options.AddPolicy(Constants.Employee, policy =>
                {
                    policy.RequireRole(Constants.Employee);
                });

                options.AddPolicy(Constants.SoftwareEngineer, policy =>
                {
                    policy.RequireRole(Constants.SoftwareEngineer);
                });
                options.AddPolicy(Constants.StudentSoftwareEngineer, policy =>
                {
                    policy.RequireRole(Constants.StudentSoftwareEngineer);
                });

                options.AddPolicy(Constants.StudentTechnologyEngineer, policy =>
                {
                    policy.RequireRole(Constants.StudentTechnologyEngineer);
                });

                options.AddPolicy(Constants.ComputerScienceLabAdmin, policy =>
                {
                    policy.RequireRole(Constants.ComputerScienceLabAdmin);
                });

            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // clear out the mappings that the DotNet framework wants to use when processing our claims, so that it doesn't replace our
            // "sub" claim with "Microsoft.something.something.subject"
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // have to have this in order to catch unhandled errors and then log the contents of the request
            app.UseEnableRequestRewind();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            }
            else
            {
                loggerFactory.AddSerilog(Log.Logger);
            }

            // Store the auth token for use in rest api calls in business layer
            app.UseStoreAuthToken();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(c =>
            {
                var endpoint = "v1/swagger.json";
                c.SwaggerEndpoint(endpoint, "StudentList API V1");
                //c.RoutePrefix = string.Empty;
            });
        }
    }
}

