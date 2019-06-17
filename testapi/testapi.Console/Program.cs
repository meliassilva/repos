using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Byui.testapi.Business.Business;

namespace Byui.testapi.Console
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
            var exampleBusinss = startup.ServiceProvider.GetService<TestBusiness>();
            var values = exampleBusinss.GetValues();
            foreach (var value in values)
            {
                System.Console.WriteLine(value);
            }
        }

    }
}
