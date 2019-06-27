using Byui.WSO2Checker.Enterprise.Interfaces;

namespace Byui.WSO2Checker.Enterprise.Configuration
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        public string ServiceUrl { get; }
        public string ServiceUser { get; }
        public string ServicePassword { get; }
        public string ServiceDomain { get; }
        public int TimeoutInSeconds { get; }

        public ServiceConfiguration(string serviceUrl, string serviceUser, string servicePassword, string serviceDomain, int timeoutInSeconds)
        {
            ServiceUrl = serviceUrl;
            ServiceUser = serviceUser;
            ServicePassword = servicePassword;
            ServiceDomain = serviceDomain;
            TimeoutInSeconds = timeoutInSeconds;
        }
    }
}
