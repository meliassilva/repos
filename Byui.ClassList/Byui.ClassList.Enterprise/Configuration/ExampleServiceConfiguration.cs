using Byui.Byui.ClassList.Enterprise.Interfaces;

namespace Byui.Byui.ClassList.Enterprise.Configuration
{
    public class ExampleServiceConfiguration : IServiceConfiguration
    {
        public string ServiceUrl { get; }
        public string ServiceUser { get; }
        public string ServicePassword { get; }
        public string ServiceDomain { get; }
        public int TimeoutInSeconds { get; }

        public ExampleServiceConfiguration(string serviceUrl, string serviceUser, string servicePassword, string serviceDomain, int timeoutInSeconds)
        {
            ServiceUrl = serviceUrl;
            ServiceUser = serviceUser;
            ServicePassword = servicePassword;
            ServiceDomain = serviceDomain;
            TimeoutInSeconds = timeoutInSeconds;
        }
    }
}
