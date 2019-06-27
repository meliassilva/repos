using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Byui.WSO2Checker.Business.Business;
using Byui.WSO2Checker.Enterprise.Interfaces;

namespace Byui.WSO2Checker.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // initialize dependancy injection and user secrets
            Startup startup = new Startup(args);
            startup.ConfigureServices();
            
            // instantiate and run your business 
            // using await is possible with C# 7.1
            ICheckerRepository checkerRepository = startup.ServiceProvider.GetService<ICheckerRepository>();

            string token = checkerRepository.GetAccessToken().Result;

            //var values = exampleBusinss.GetValues();
            //foreach (var value in values)
            //{
            //    System.Console.WriteLine(value);
            //}
        }

    }
}