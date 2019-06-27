using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace Byui.WSO2Checker.Enterprise.Wso2Apis
{
    /// <summary>
    /// Refit Example Interface.
    /// Headers is required to authenticate against the WSO2 server
    /// </summary>
    [Headers("Authorization: Bearer")]
    public interface IExampleApiService
    {
        /// <summary>
        /// Api Endpoint configuration. See Refit Documentation
        /// </summary>
        /// <returns></returns>
        [Get("/term")]
        Task<string> GetCurrentTerm();
    }
}
