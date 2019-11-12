// ReSharper disable RedundantTypeArgumentsOfMethod
namespace EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;
    using IoC;
    using IoC.Features;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    internal class Configuration: IConfiguration
    {
        public IEnumerable<IToken> Apply(IContainer container)
        {
            // Create ASP .NET core feature
            var aspNetCoreFeature = new AspNetCoreFeature();
            aspNetCoreFeature.AddDbContext<Db>(
                options => options.UseInMemoryDatabase("Sample DB"),
                ServiceLifetime.Singleton);

            // Id generator
            var currentId = new Id();
            var lockObject = new object();
            var id = new Func<Id>(() =>
            {
                lock (lockObject)
                {
                    return currentId = new Id(currentId);
                }
            });

            yield return container
                // Apply ASP.NET core feature
                .Apply(aspNetCoreFeature)
                // Add required bindings
                .Bind<Id>().To(ctx => id())
                .Bind<Person>().To<Person>();
        }
    }
}
