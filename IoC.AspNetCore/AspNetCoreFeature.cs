namespace IoC.Features
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
    public class AspNetCoreFeature : Collection<ServiceDescriptor>, IServiceCollection, IConfiguration, ICannotRegister
    {
        private object _lockObject = new object();
        private long _tag;
        private ServiceDescriptor _service;
        private IBinding<object> _binding;

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

            using (container.Bind<ICannotRegister>().To(ctx => this))
            {
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

                        if (TryCreateToken(service, binding, out var token))
                        {
                            yield return token;
                            continue;
                        }

                        throw new NotSupportedException($"The service descriptor {service} is not supported.");
                    }
                }
            }

            yield return container
                .Bind<IServiceProvider>().Lifetime(singletonLifetimeResolver(container)).To<ServiceProvider>()
                .Bind<IServiceScopeFactory>().Lifetime(singletonLifetimeResolver(container)).To<ServiceScopeFactory>()
                .Bind<IServiceScope>().To<ServiceScope>();
        }

        IToken ICannotRegister.Resolve(IContainer container, Key[] keys)
        {
            lock (_lockObject)
            {
                if (TryCreateToken(_service, _binding, out var token))
                {
                    return token;
                }
            }

            throw new InvalidOperationException("Cannot create a binding token.");
        }

        private bool TryCreateToken(ServiceDescriptor service, IBinding<object> binding, out IToken token)
        {
            lock (_lockObject)
            {
                _service = service;
                _binding = binding.Tag(_tag++);

                if (service.ImplementationType != null)
                {
                    token = binding.To(service.ImplementationType);
                    return true;
                }

                if (service.ImplementationFactory != null)
                {
                    token = binding.To(ctx => service.ImplementationFactory(ctx.Container.Inject<IServiceProvider>()));
                    return true;
                }

                if (service.ImplementationInstance != null)
                {
                    token = binding.To(ctx => service.ImplementationInstance);
                    return true;
                }

                token = default(IToken);
                return false;
            }
        }
    }
}
