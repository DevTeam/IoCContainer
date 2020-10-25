namespace BlazorWebAssemblyApp
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // Adds a service provider for the Inversion of Control container
            builder.ConfigureContainer(new ServiceProviderFactory(container));

            await builder.Build().RunAsync();
        }
    }
}
