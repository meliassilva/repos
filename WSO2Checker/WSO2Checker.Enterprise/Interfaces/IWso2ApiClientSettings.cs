using System;
using System.Collections.Generic;
using System.Text;

namespace Byui.WSO2Checker.Enterprise.Interfaces
{
    /// <summary>
    /// This interface should be applied to your project's AppSettings object so the Wso2ApiClient can be setup correctly
    /// without duplicating code
    /// </summary>
    public interface IWso2ApiClientSettings
    {
        /// <summary>
        /// MetadataAddress
        /// </summary>
        string MetadataAddress { get; set; }

        /// <summary>
        /// Client Id for the API
        /// </summary>
        string ClientId { get; set; }

        /// <summary>
        /// Password for the API
        /// </summary>
        string ClientSecret { get; set; }
    }
}
