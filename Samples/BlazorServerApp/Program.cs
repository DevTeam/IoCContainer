namespace BlazorServerApp
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Clock;
    using IoC;
    using IoC.Features.AspNetCore;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var container = Container
                // Creates an Inversion of Control container
                .Create()
                .Using<ClockConfiguration>();

            var host = Host.CreateDefaultBuilder(args)
                // Adds a service provider for the Inversion of Control container
                .UseServiceProviderFactory(new ServiceProviderFactory(container))
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .Build();

            await host.RunAsync();
        }
    }
}
