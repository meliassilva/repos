using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Byui.WSO2Checker.Enterprise.Interfaces
{
    /// <summary>
    /// Used with Refit to authenticate against the Wso2 API servers. Returns an HttpClient that handles the
    /// authentication, through REFIT, with the Wso2 Server
    /// </summary>
    public interface IWso2ApiClient
    {
        Task<string> GetAuthToken();
        HttpClient GetClient(string url);
        HttpClient GetClient(string url, int seconds);
    }
}
