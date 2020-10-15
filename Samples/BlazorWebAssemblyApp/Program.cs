namespace BlazorWebAssemblyApp
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Clock;
    using IoC;
    using IoC.Features.AspNetCore;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var container = Container
                // Creates an Inversion of Control container
                .Create()
                .Using<ClockConfiguration>();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Adds a service provider for the Inversion of Control container
            builder.ConfigureContainer(new ServiceProviderFactory(container));

            await builder.Build().RunAsync();
        }
    }
}
