using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Byui.repos.Console
{
    public class Startup
    {
        private readonly bool _decryptSecrets;
        private readonly IConfigurationRoot _config;
        private readonly ServiceCollection _services;
        private string _environment;

        private const string Development = "Development";
        private const string EnvironmentVariable = "ASPNETCORE_ENVIRONMENT";

        public ServiceProvider ServiceProvider { get; private set; }

        public Startup(string[] args)
        {
            ConfigureEnvironment(args);
            _decryptSecrets = File.Exists("secrets.json");

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (!_decryptSecrets)
            {
                builder.AddUserSecrets<Program>();
            }

            _config = builder.Build();
            
            if (_environment != Development)
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(_config)
                    .CreateLogger();
            }
            _services = new ServiceCollection();
        }
        public void ConfigureServices()
        {
            if (_environment != Development)
            {
                _services.AddSingleton(new LoggerFactory()
                    .AddConsole()
                    .AddSerilog()
                    .AddDebug());
                _services.AddLogging();
            }

            // configure automapper and setup dependency injection
            Business.Utilities.Configuration.Configure(_services, _config, !_decryptSecrets);
            ServiceProvider = _services.BuildServiceProvider();
        }

        private void ConfigureEnvironment(string[] args)
        {
            _environment = Environment.GetEnvironmentVariable(EnvironmentVariable);

            if (args != null && args.Length == 1)
            {
                _environment = args[0];
            }

            if(string.IsNullOrWhiteSpace(_environment))
            {
                _environment = Development;
            }
            System.Console.WriteLine(_environment);
        }
        
    }
}