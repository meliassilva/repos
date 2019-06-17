using System;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using Byui.repos.Enterprise.Interfaces;

namespace Byui.repos.Enterprise.Repositories
{
    public abstract class SoaAbstractRepository
    {
        protected readonly IServiceConfiguration _configuration;

        protected SoaAbstractRepository(IServiceConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        /// <summary>
        /// Gets the NetworkCredential to use with the endpoints.
        /// </summary>
        /// <returns></returns>
        protected NetworkCredential GetCredential()
        {
            return new NetworkCredential(_configuration.ServiceUser, _configuration.ServicePassword, _configuration.ServiceDomain);
        }

        /// <summary>
        /// Gets a binding for use with the web services.
        /// </summary>
        /// <returns></returns>
        protected NetTcpBinding GetBinding()
        {
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport)
            {
                CloseTimeout = TimeSpan.FromSeconds(_configuration.TimeoutInSeconds),
                OpenTimeout = TimeSpan.FromSeconds(_configuration.TimeoutInSeconds),
                SendTimeout = TimeSpan.FromSeconds(_configuration.TimeoutInSeconds),
                ReceiveTimeout = TimeSpan.FromSeconds(_configuration.TimeoutInSeconds)
            };

            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.MaxBufferSize = int.MaxValue;

            return binding;
        }

        /// <summary>
        /// Process when a service channel faults, this way it can still be disposed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InnerChannel_Faulted(object sender, EventArgs e)
        {
            // make sure the channel is aborted
            (sender as ICommunicationObject).Abort();
        }

        /// <summary>
        /// Increases the object graph size per operation and timeout per operation.  This can be called in order to allow for some SOA methods that
        /// take longer to run.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="channel"></param>
        /// <param name="timeoutInMinutes"></param>
        protected static void IncreaseObjectGraphSizeAndOperationTimeout(ServiceEndpoint endpoint, IClientChannel channel, int timeoutInMinutes = 2)
        {
            channel.OperationTimeout = TimeSpan.FromMinutes(timeoutInMinutes);

            foreach (OperationDescription operation in endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior behavior = operation.OperationBehaviors.FirstOrDefault(o=>o is DataContractSerializerOperationBehavior) as DataContractSerializerOperationBehavior;
                if (behavior != null)
                {
                    behavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
        }
    }
}