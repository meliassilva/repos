using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Byui.Something.ApplicationCore.Examples.Interactors.GetExample;
using MediatR;

namespace Byui.Something.Console
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
            var mediatr = startup.ServiceProvider.GetService<IMediator>();
            var values = await mediatr.Send(new GetExampleRequest());
            foreach (var value in values)
            {
                System.Console.WriteLine(value);
            }
        }

    }
}