namespace Byui.StudentListApi.Enterprise.Interfaces
{
    /// <summary>
    /// Provides the needed information in order to configure and use enterprise web services.
    /// </summary>
    public interface IServiceConfiguration
    {
        string ServiceUrl { get; }

        string ServiceUser { get; }

        string ServicePassword { get; }

        string ServiceDomain { get; }

        int TimeoutInSeconds { get; }
    }
}
