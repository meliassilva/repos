using Byui.Something.ApplicationCore.Common.Interfaces.Configurations;

namespace Byui.Something.Infrastructure.Configurations
{
    public class AppConfiguration : IAppConfiguration
    {
        public AppConfiguration(string appUrl)
        {
            AppUrl = appUrl;
        }

        public string AppUrl { get; }
    }
}