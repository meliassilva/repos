using System.ServiceModel;
using AutoMapper;
using Byui.Something.Infrastructure.Configurations;
using Byui.Something.Infrastructure.ConnectedServices.PersonService;


namespace Byui.Something.Infrastructure.Services.PersonService
{
    public class PersonService : SoaService, ApplicationCore.Services.IPersonService
    {
        private readonly IMapper _mapper;
        
        public PersonService(PersonServiceConfiguration configuration, IMapper mapper) : base(configuration)
        {
            _mapper = mapper;
        }

        
        private PersonServiceClient GetService()
        {
            var binding = GetBinding();
            var serviceClient = new PersonServiceClient(binding, new EndpointAddress(_configuration.ServiceUrl));
            serviceClient.ClientCredentials.Windows.ClientCredential = GetCredential();
            (serviceClient as ICommunicationObject).Faulted += InnerChannel_Faulted;

            return serviceClient;
        }

    }
}