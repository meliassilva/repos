using Byui.Something.Infrastructure.Services;

namespace Byui.Something.Infrastructure.Configurations
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        public string ServiceUrl { get; set; }
        public string ServiceUser { get; set; }
        public string ServicePassword { get; set; }
        public string ServiceDomain { get; set; } = "byui";
        public int TimeoutInSeconds { get; set; } = 30;

        public void SetAccount(string user, string password)
        {
            ServiceUser = user;
            ServicePassword = password;
        }
    }
}
