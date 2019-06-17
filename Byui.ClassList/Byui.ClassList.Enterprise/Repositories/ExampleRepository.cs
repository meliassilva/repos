using Byui.Byui.ClassList.Enterprise.Configuration;
using Byui.Byui.ClassList.Enterprise.Interfaces;

namespace Byui.Byui.ClassList.Enterprise.Repositories
{
    /// <summary>
    /// This is an example implementation of a repository.  The person service is created in each method. You want to take this approach when you:
    /// 1. Don't care to Unit Test your repository
    /// 2. Don't care to control when the service clients are instantiated and disposed
    /// </summary>
    public class ExampleRepository : SoaAbstractRepository, IExampleRepository
    {

        public ExampleRepository(ExampleServiceConfiguration serviceConfiguration) : base(serviceConfiguration)
        {
            
        }

        private void GetService()
        {
//            PersonServiceClient serviceClient;
//            var binding = GetBinding();
//            serviceClient = new PersonServiceClient(binding, new EndpointAddress(_configuration.ServiceUrl);
//            
//
//            serviceClient.ClientCredentials.UserName.UserName = $"{_configuration.ServiceDomain}\\{_configuration.ServiceUser}";
//            serviceClient.ClientCredentials.UserName.Password = _configuration.ServicePassword;
//            (serviceClient as ICommunicationObject).Faulted += InnerChannel_Faulted;
//
//            return serviceClient;
        }
        
    }
    
    // todo once a service reference is added, add this code to the appropiate folder (ie. PersonService).
    // todo use using statements when creating a client
    // todo using(var client = _serviceHelper.GetPersonServiceClient()){}
    
//    public partial class PersonServiceClient : IDisposable
//    {
//        public void Dispose()
//        {
//            if (State == CommunicationState.Faulted)
//            {
//                Abort();
//            }
//            else if (State != CommunicationState.Closed)
//            {
//                CloseAsync();
//            }
//        }
//    }
}
