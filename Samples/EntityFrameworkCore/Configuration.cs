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
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            // Create ASP .NET core feature
            var aspNetCoreFeature = new AspNetCoreFeature();
            aspNetCoreFeature.AddDbContext<PeopleDbContext>(
                        options => options.UseInMemoryDatabase("People"),
                        ServiceLifetime.Transient);

            // Apply ASP.NET core feature
            yield return container.Apply(aspNetCoreFeature);
        }
    }
}
