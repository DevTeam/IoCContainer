﻿namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AspNetCore;
    using Issues;
    using Microsoft.Extensions.DependencyInjection;

    /// <inheritdoc cref="IConfiguration" />
    [PublicAPI]
    public class AspNetCoreFeature : Collection<ServiceDescriptor>, IServiceCollection, IConfiguration
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public AspNetCoreFeature() { }

        /// <summary>
        /// Creates an instance of feature based on a list of ServiceDescriptor.
        /// </summary>
        /// <param name="list"></param>
        public AspNetCoreFeature([NotNull] IList<ServiceDescriptor> list) : base(list) { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var singletonLifetimeResolver = container.GetResolver<ILifetime>(Lifetime.Singleton.AsTag());
            var scopeSingletonLifetimeResolver = container.GetResolver<ILifetime>(Lifetime.ScopeSingleton.AsTag());

            foreach (var serviceGroup in this.GroupBy(i => i.ServiceType))
            {
                foreach (var service in serviceGroup.Reverse())
                {
                    var binding = container.Bind(service.ServiceType);
                    switch (service.Lifetime)
                    {
                        case ServiceLifetime.Transient:
                            break;

                        case ServiceLifetime.Singleton:
                            binding = binding.Lifetime(singletonLifetimeResolver(container));
                            break;

                        case ServiceLifetime.Scoped:
                            binding = binding.Lifetime(scopeSingletonLifetimeResolver(container));
                            break;

                        default:
                            throw new NotSupportedException($"Unknown lifetime {service.Lifetime}.");
                    }

                    var tokenFactory = new TokenFactory(service, binding);
                    using (container.Bind<ICannotRegister>().To(ctx => tokenFactory))
                    {
                        if (tokenFactory.TryCreateToken(out var token))
                        {
                            yield return token;
                            continue;
                        }
                    }

                    throw new NotSupportedException($"The service descriptor {service} is not supported.");
                }
            }


            yield return container
                .Bind<IServiceProvider>().Lifetime(singletonLifetimeResolver(container)).To<ServiceProvider>()
                .Bind<IServiceScopeFactory>().Lifetime(singletonLifetimeResolver(container)).To<ServiceScopeFactory>()
                .Bind<IServiceScope>().To<ServiceScope>();
        }
    }
}
