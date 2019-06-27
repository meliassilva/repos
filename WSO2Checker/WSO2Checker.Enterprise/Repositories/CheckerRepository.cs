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
        public CheckerRepository(
            IStudentListConfiguration serviceConfiguration)
        {
            _serviceConfiguration = serviceConfiguration;
        }

        public async Task<string> GetAccessToken()
        {
            Url url = new Url($"https://apitemp.byui.edu/token?grant_type=client_credentials");
            var response = await url.WithHeader("Authorization", $"Basic {GetBase64UsernamePassword(_serviceConfiguration.WSO2Username, _serviceConfiguration.WSO2Password)}")
                .SendAsync(HttpMethod.Post);
            string result = await response.Content.ReadAsStringAsync();

            Token token = JsonConvert.DeserializeObject<Token>(result);
                        
            return token.AccessToken;
        }
    
        private string GetBase64UsernamePassword(string username, string password)
        {
            var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");
            var convertedString = Convert.ToBase64String(bytes);

            return convertedString;
        }
          
        public async Task getStudentListAsync(Token)
        {
            Url url = new Url($"https://apitemp.byui.edu/domain/studentlist/v1/student/Campus.2019.Spring.CS313.1?returnformat=json");
            var response = await url.WithHeader("Authorization", $"Bearer" Token)
                .SendAsync(HttpMethod.Get);

            string result = await response.Content.ReadAsStringAsync();

            Student student = JsonConvert.DeserializeObject<Student>(result);


        }

        public void verifiedWSO2()
        {



        }


    }
}
