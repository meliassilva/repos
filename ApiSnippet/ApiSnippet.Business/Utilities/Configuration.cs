using AutoMapper;
using Byui.ApiSnippet.Business.Business;
using Byui.ApiSnippet.Business.Entities;
using Byui.ApiSnippet.Enterprise.Configuration;
using Byui.ApiSnippet.Enterprise.Interfaces;
using Byui.ApiSnippet.Enterprise.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Byui.ApiSnippet.Business.Utilities
{
    public static class Configuration
    {
        public static AppSettings Configure(IServiceCollection services, IConfigurationRoot config, bool isDevelopment)
        {
            ConfigureMapper();
            return ConfigureDependencies(services, config, isDevelopment);
        }

        private static void ConfigureMapper()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfiles(typeof(Configuration).Assembly);
            });
        }

        private static AppSettings ConfigureDependencies(IServiceCollection services, IConfigurationRoot config, bool isDevelopment)
        {
            // get settings from appsettings.json and secrets.json
            AppSettings settings = new AppSettings();
            config.GetSection("AppSettings").Bind(settings);
            services.AddSingleton(settings);

            IServiceConfiguration exampleServiceConfiguration = new ExampleServiceConfiguration(
                settings.PersonServiceReadOnlyUrl,
                settings.ServiceUser,
                settings.ServicePassword,
                "byui.edu",
                10
            );

            services.AddScoped<IUserAuthToken, UserAuthToken>();
            services.AddSingleton(exampleServiceConfiguration);
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(s => new Mapper(s.GetRequiredService<IConfigurationProvider>(), s.GetService));
            
            if (!string.IsNullOrEmpty(settings.ConnectionString))
            {
                services.AddDbContext<ApiSnippetContext>(options => options.UseSqlServer(settings.ConnectionString));
            }
            else
            {
                services.AddDbContext<ApiSnippetContext>(options => options.UseInMemoryDatabase("ApiSnippet"));
            }
            services.AddScoped<IExampleRepository, ExampleRepository>();
            services.AddScoped<IClock, Clock>();
            services.AddScoped<ExampleBusiness>();

            return settings;
        }
    }
}
