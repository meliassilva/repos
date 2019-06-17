using System;
using System.Collections.Generic;
using System.Text;
using Byui.repos.Enterprise.Interfaces;
using Byui.repos.Enterprise.Wso2Apis;
using Refit;

namespace Byui.repos.Enterprise.Helpers.Wso2
{
    /// <summary>
    /// Generic creation point for WSO2 services.
    /// This class looks up the address of the service from the static Wso2Directory, thus you should never need to modify this class.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class Wso2Api<T>
    {
        public static T Create(IWso2ApiClient client, int timeoutInSeconds = 120)
        {
            if (Wso2ApiDirectory.Directory.ContainsKey(typeof(T)))
            {
                var address = Wso2ApiDirectory.Directory[typeof(T)];
                T result = RestService.For<T>(client.GetClient(address, timeoutInSeconds));
                return result;
            }
            else
            {
                throw new ArgumentException("ApiClient Type not found in Wso2ApiDictionary");
            }
        }
    }
}
