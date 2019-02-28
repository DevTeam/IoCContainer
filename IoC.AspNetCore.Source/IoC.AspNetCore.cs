
/* 
IoC Container APS.NET Core feature

https://github.com/DevTeam/IoCContainer

Important note: do not use any internal classes, structures, enums, interfaces, methods, fields or properties
because it may be changed even in minor updates of package.

MIT License

Copyright (c) 2018 Nikolay Pianikov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

#pragma warning disable CS1658 // Warning is overriding an error
#pragma warning disable nullable
#pragma warning restore CS1658 // Warning is overriding an error

// ReSharper disable All

#region AspNetCore

#region ServiceProvider

namespace IoC.Features.AspNetCore
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class ServiceProvider: IServiceProvider
    {
        private readonly IContainer _container;

        public ServiceProvider([NotNull] IContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public object GetService(Type serviceType)
        {
            return _container.GetResolver<object>(serviceType)(_container);
        }
    }
}


#endregion
#region ServiceScope

namespace IoC.Features.AspNetCore
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    internal class ServiceScope : IServiceScope, IServiceProvider
    {
        [NotNull] private readonly Scope _scope;
        [NotNull] private readonly IContainer _container;

        public ServiceScope([NotNull] Scope scope, [NotNull] IContainer container)
        {
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public IServiceProvider ServiceProvider => this;

        public object GetService(Type serviceType)
        {
            using (_scope.Begin())
            {
                return _container.Resolve<object>(serviceType);
            }
        }

        public void Dispose() => _scope.Dispose();
    }
}


#endregion
#region ServiceScopeFactory

namespace IoC.Features.AspNetCore
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class ServiceScopeFactory : IServiceScopeFactory
    {
        [NotNull] private readonly Func<IServiceScope> _serviceScopeFactory;

        public ServiceScopeFactory([NotNull] Func<IServiceScope> serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        public IServiceScope CreateScope() => _serviceScopeFactory();
    }
}


#endregion

#endregion

#region IoC.AspNetCore

#region AspNetCoreFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AspNetCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <inheritdoc cref="IConfiguration" />
    [PublicAPI]
    public class AspNetCoreFeature: Collection<ServiceDescriptor>, IServiceCollection, IConfiguration
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public AspNetCoreFeature()
        {
        }

        /// <summary>
        /// Creates an instance of feature based on a list of ServiceDescriptor.
        /// </summary>
        /// <param name="list"></param>
        public AspNetCoreFeature([NotNull] IList<ServiceDescriptor> list) : base(list)
        {
        }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var singletonLifetimeResolver = container.GetResolver<ILifetime>(Lifetime.Singleton.AsTag());
            var scopeSingletonLifetimeResolver = container.GetResolver<ILifetime>(Lifetime.ScopeSingleton.AsTag());
            foreach (var serviceGroup in this.GroupBy(i => i.ServiceType))
            {
                var tag = 0;
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

                    if (tag > 0)
                    {
                        binding = binding.Tag(tag);
                    }

                    tag++;

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

            yield return container.Bind<IServiceProvider>().Lifetime(singletonLifetimeResolver(container)).To<ServiceProvider>();
            yield return container.Bind<IServiceScopeFactory>().Lifetime(singletonLifetimeResolver(container)).To<ServiceScopeFactory>();
            yield return container.Bind<IServiceScope>().To<ServiceScope>(ctx => new ServiceScope(ctx.Container.Inject<Scope>(), ctx.Container.Inject<IContainer>()));
        }
    }
}


#endregion

#endregion

#pragma warning disable CS1658 // Warning is overriding an error
#pragma warning restore nullable
#pragma warning restore CS1658 // Warning is overriding an error

// ReSharper restore All