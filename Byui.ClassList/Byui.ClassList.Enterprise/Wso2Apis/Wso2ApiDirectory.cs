using System;
using System.Collections.Generic;

namespace Byui.Byui.ClassList.Enterprise.Wso2Apis
{
    /// <summary>
    /// This class links the Refit Wso2Api interface with the API URL.
    /// Workflow:
    ///     1. Create the interface for your Wso2 Service
    ///     2. Update the Wso2Directory constructor to link your interface to the api url
    /// </summary>
    public static class Wso2ApiDirectory
    {
        /// <summary>
        /// Dictionary linking Refit interface types to their Wso2 urls
        /// </summary>
        public static Dictionary<Type, string> Directory { get; set; }

        /// <summary>
        /// Static constructor. Update this with your Wso2 API interface and associated url
        /// </summary>
        static Wso2ApiDirectory()
        {
            Directory = new Dictionary<Type, string>();
            Directory.Add(typeof(IExampleApiService), "https://apitemp.byui.edu/example-service/v2");
        }
    }
}
