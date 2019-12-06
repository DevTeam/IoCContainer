namespace WebApplication3
{
    using System;
    using IoC;
    using IoC.Features;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using ShroedingersCat;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new Aaa())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class Aaa : IServiceProviderFactory<IContainer>
    {
        public IContainer CreateBuilder(IServiceCollection services)
        {
            //IOptions<MvcOptions> mvcOptions
            return Container
                // Creates an Inversion of Control container
                .Create()
                // using .NET ASP Feature
                .Using(new AspNetCoreFeature(services))
                // using Glue
                .Using<Glue>()
                .Bind<IHttpContextAccessor>().To<HttpContextAccessor>().Container;
        }

        public IServiceProvider CreateServiceProvider(IContainer containerBuilder)
        {
            using(var scope = containerBuilder.Resolve<IScope>())
            using (scope.Activate())
            {
                var mvcOption = containerBuilder.Resolve<IOptions<MvcOptions>>();
            }

            return containerBuilder.Resolve<IServiceProvider>();
        }
    }

}
