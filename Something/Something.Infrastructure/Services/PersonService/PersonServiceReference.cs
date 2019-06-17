using System;
using System.ServiceModel;

namespace Byui.Something.Infrastructure.ConnectedServices.PersonService
{
    internal partial class PersonServiceClient : IDisposable
    {
        public void Dispose()
        {
            if (State == CommunicationState.Faulted)
            {
                Abort();
            }
            else if (State != CommunicationState.Closed)
            {
                CloseAsync();
            }
        }
    }
}