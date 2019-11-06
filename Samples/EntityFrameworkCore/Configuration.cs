// ReSharper disable RedundantTypeArgumentsOfMethod
namespace EntityFrameworkCore
{
    using System.Collections.Generic;
    using IoC;
    using IoC.Features;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using static IoC.Lifetime;

    internal class Configuration: IConfiguration
    {
        public IEnumerable<IToken> Apply(IContainer container)
        {
            // Create ASP .NET core feature
            var aspNetCoreFeature = new AspNetCoreFeature();
            aspNetCoreFeature.AddDbContext<PeopleDbContext>(
                        options => options.UseInMemoryDatabase("People"),
                        ServiceLifetime.Transient);

            // Apply ASP.NET core feature
            yield return container.Apply(aspNetCoreFeature)
                .Bind<IIdGenerator>().As(Singleton).To<IdGenerator>()
                .Bind<Person>().To<Person>(ctx => new Person(ctx.Container.Resolve<IIdGenerator>().Generate()));
        }
    }
}
