using Byui.WSO2Checker.Enterprise.Interfaces;
using Byui.WSO2Checker.Enterprise.Models;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Byui.WSO2Checker.Enterprise.Repositories
{
    public class CheckerRepository : ICheckerRepository
    {
        private readonly IStudentListConfiguration _serviceConfiguration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceConfiguration"></param>
        public CheckerRepository(
            IStudentListConfiguration serviceConfiguration)
        {
            _serviceConfiguration = serviceConfiguration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessToken()
        {
            try
            {
                Url url = new Url($"https://apitemp.byui.edu/token?grant_type=client_credentials");
                var response = await url.WithHeader("Authorization", $"Basic {GetBase64UsernamePassword(_serviceConfiguration.WSO2Username, _serviceConfiguration.WSO2Password)}")
                    .SendAsync(HttpMethod.Post);
                string result = await response.Content.ReadAsStringAsync();

                Token token = JsonConvert.DeserializeObject<Token>(result);


                return token.AccessToken;
            }
            catch (Exception ex)
            {
                return $"There was a problem getting the auth token {ex}";

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string GetBase64UsernamePassword(string username, string password)
        {
            var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");
            var convertedString = Convert.ToBase64String(bytes);

            return convertedString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetStudentList()
        {
            try
            {
                //pass the url
                Url url = new Url($"https://apitemp.byui.edu/domain/studentlist/v1/student/Campus.2019.Spring.CS 313.1?returnformat=json");
                string accessToken = await GetAccessToken();

                //pass what I need for authorization
                var response = await url.WithHeader("Authorization", $"Bearer {accessToken}")
                    .SendAsync(HttpMethod.Get);

                //my result 
                string result = await response.Content.ReadAsStringAsync();


                return result;
            }
            catch (Exception ex)
            {
                return $"There was a problem getting the list{ex}";

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> Verification()
        {
            string verified = await GetStudentList();
            if (verified != null && verified != "")
            {
                return "Sucess";
            }

            return "Failed";

        }
    }
}
