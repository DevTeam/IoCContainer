// ReSharper disable ClassNeverInstantiated.Global
namespace WebApplication3
{
    using Clock;
    using IoC;
    using IoC.Features;
    using IoC.Features.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using static System.Console;

    public class Program
    {
        public static void Main(string[] args)
        {
            using var container = Container
                // Creates an Inversion of Control container
                .Create()
                //.Using(new ResolveUnboundFeature())
                .Using<ClockConfiguration>();

            // Creates a host
            using var host = Host
                .CreateDefaultBuilder(args)
                // Adds a service provider for the Inversion of Control container
                .UseServiceProviderFactory(new ServiceProviderFactory(container))
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseKestrel().UseStartup<Startup>(); })
                .Build();

            host.Run();
        }
    }
}
