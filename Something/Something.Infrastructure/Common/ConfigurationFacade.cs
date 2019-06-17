using AutoMapper;
using Byui.Something.ApplicationCore.Common.Interfaces.Clock;
using Byui.Something.ApplicationCore.Common.Interfaces.Configurations;
using Byui.Something.ApplicationCore.Common.Interfaces.MapperProxy;
using Byui.Something.ApplicationCore.Common.Interfaces.Persistence;
using Byui.Something.ApplicationCore.Services;
using Byui.Something.Infrastructure.Configurations;
using Byui.Something.Infrastructure.Persistence.Common;
using Byui.Something.Infrastructure.Services.PersonService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Byui.Something.Infrastructure.Common
{
    public class ConfigurationFacade
    {
        private const string Something = "Something";


        public static AppSettings Configure(IServiceCollection services, IConfigurationRoot config, bool isDevelopment, IFileProvider contentRoot)
        {
            ConfigureMapper();
            return ConfigureDependencies(services, config, isDevelopment, contentRoot);
        }

        private static void ConfigureMapper()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfiles(typeof(ConfigurationFacade).Assembly);
            });
        }
        
        private static AppSettings ConfigureDependencies(IServiceCollection services, IConfigurationRoot config, bool isDevelopment, IFileProvider contentRoot)
        {
            var applicationCoreAssemply = typeof(IClock).Assembly;

            // get settings from appsettings.json and secrets.json
            AppSettings settings = new AppSettings();
            config.GetSection("AppSettings").Bind(settings);
            services.AddSingleton(settings);

            // for rest APIs
            services.AddScoped<IUserAuthToken, UserAuthToken>();
            services.AddSingleton(new AuthToken(settings.MetadataAddress, settings.ClientId, settings.ClientSecret));
            services.AddScoped<ApiMessageHandler>();

            // use sql server if a connection string is provided, otherwise use an in memory db
            if (!string.IsNullOrEmpty(settings.ConnectionString))
            {
                services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(settings.ConnectionString));
            }
            else
            {
                services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase(Something));
            }

            // Person Service Config
            settings.PersonService.SetAccount(settings.ServiceUser, settings.ServicePassword);
            services.AddSingleton(settings.PersonService);
            
            // App config
            IAppConfiguration appConfiguration = new AppConfiguration(settings.AppUrl);
            services.AddSingleton(appConfiguration);
            
            // Setup MediatR
            services.AddMediatR(applicationCoreAssemply);
            
            // Services
            services.AddScoped<IPersonService, PersonService>();
            
            // Common
            services.AddScoped<IClock, Clock>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMapperProxy, MapperProxy.MapperProxy>();
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(s => new Mapper(s.GetRequiredService<IConfigurationProvider>(), s.GetService));

            // how to use a REST API
//            services.AddRefitClient<IApiInterface>()
//                .ConfigureHttpClient(c => c.BaseAddress = new Uri(settings.UrlToApi))
//                .AddHttpMessageHandler<ApiMessageHandler>();

            // File Providers
            var embeddedProvider = new EmbeddedFileProvider(applicationCoreAssemply);
            var compositeProvider = new CompositeFileProvider(contentRoot, embeddedProvider);
            services.AddSingleton<IFileProvider>(compositeProvider);

            return settings;
        }
    }
}