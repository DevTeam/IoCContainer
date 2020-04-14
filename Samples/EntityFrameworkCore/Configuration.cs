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
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            // Create ASP .NET core feature
            var aspNetCoreFeature = new AspNetCoreFeature();

            // Configure services
            aspNetCoreFeature
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<Db>(options => options.UseInMemoryDatabase("Sample DB"), ServiceLifetime.Singleton);

            // Id generator
            var id = new Id();
            var generateId = new Func<Id>(() => id = new Id(id));

            yield return container
                // Apply ASP.NET core feature
                .Apply(aspNetCoreFeature)
                // Add required bindings
                .Bind<Id>().To(ctx => generateId())
                .Bind<Person>().To<Person>();
        }
    }
}
