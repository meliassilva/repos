namespace Byui.Something.Infrastructure.Services
{
    public interface IServiceConfiguration
    {
        string ServiceUrl { get; }

        string ServiceUser { get; }

        string ServicePassword { get; }

        string ServiceDomain { get; }

        int TimeoutInSeconds { get; }
    }
}