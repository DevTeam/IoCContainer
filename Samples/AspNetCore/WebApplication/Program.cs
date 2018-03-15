namespace WebApplication
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).Run();

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
