using Byui.Something.Infrastructure.Configurations;

namespace Byui.Something.Infrastructure.Common
{
    public class AppSettings
    {
        public string AppUrl { get; set; }
        public string ServiceUser { get; set; }
        public string ServicePassword { get; set; }
        public string MetadataAddress { get; set; }
        public string ConnectionString { get; set; }
        public string PersonServiceReadOnlyUrl { get; set; }
        public string PersonServiceUrl { get; set; }
        public string RegistrationServiceReadOnlyUrl { get; set; }
        public string RegistrationServiceUrl { get; set; }
        public string UtilitiesServiceUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public PersonServiceConfiguration PersonService { get; set; }
    }
}