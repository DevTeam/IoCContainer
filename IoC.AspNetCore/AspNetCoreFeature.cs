namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AspNetCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Allows to use ASP.NET and other related frameworks.
    /// </summary>
    [PublicAPI]
    public sealed class AspNetCoreFeature : Collection<ServiceDescriptor>, IServiceCollection, IConfiguration
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public AspNetCoreFeature() { }

        /// <summary>
        /// Creates an instance of feature based on a list of <c>ServiceDescriptor</c>.
        /// </summary>
        /// <param name="services"></param>
        public AspNetCoreFeature([NotNull] IList<ServiceDescriptor> services) : base(services) { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var singletonLifetimeResolver = container.GetResolver<ILifetime>(Lifetime.Singleton.AsTag());
            var containerSingletonLifetimeResolver = container.GetResolver<ILifetime>(Lifetime.ContainerSingleton.AsTag());
            var disposingLifetimeResolver = container.GetResolver<ILifetime>(Lifetime.Disposing.AsTag());
            foreach (var serviceGroup in this.Reverse().Select((service, index) => new { index, item = service }).GroupBy(i => i.item.ServiceType))
            {
                var isFirst = true;
                foreach (var description in serviceGroup)
                {
                    var service = description.item;
                    var binding = container.Bind(service.ServiceType);
                    if (!isFirst)
                    {
                        binding = binding.Tag(description.index);
                    }
                    else
                    {
                        isFirst = false;
                    }

                    switch (service.Lifetime)
                    {
                        case ServiceLifetime.Transient:
                            binding = binding.Lifetime(disposingLifetimeResolver(container));
                            break;

                        case ServiceLifetime.Singleton:
                            binding = binding.Lifetime(singletonLifetimeResolver(container));
                            break;

                        case ServiceLifetime.Scoped:
                            binding = binding.Lifetime(containerSingletonLifetimeResolver(container));
                            break;

                        default:
                            throw new NotSupportedException($"Unknown lifetime {service.Lifetime}.");
                    }

                    if (service.ImplementationType != null)
                    {
                        yield return binding.To(service.ImplementationType);
                        continue;
                    }

                    if (service.ImplementationFactory != null)
                    {
                        yield return binding.To(ctx => service.ImplementationFactory(ctx.Container.Inject<IServiceProvider>()));
                        continue;
                    }

                    if (service.ImplementationInstance != null)
                    {
                        yield return binding.To(ctx => service.ImplementationInstance);
                        continue;
                    }

                    throw new NotSupportedException($"The service descriptor {service} is not supported.");
                }
            }

            yield return container
                .Bind<IServiceProvider>().Bind<ISupportRequiredService>().Lifetime(singletonLifetimeResolver(container)).To<ServiceProvider>()
                .Bind<IServiceScopeFactory>().Lifetime(singletonLifetimeResolver(container)).To<ServiceScopeFactory>()
                .Bind<IServiceScope>().To<ServiceScope>()
                // IEnumerable should preserve an order
                .Bind<IEnumerable<TT>>().To(ctx => ctx.Container.Inject<IEnumerable<TT>>(TagKeyComparer.Shared, ctx.Args));
        }
    }
}
