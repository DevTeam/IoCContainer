
/* 
IoC Container APS.NET Core feature

https://github.com/DevTeam/IoCContainer

Important note: do not use any internal classes, structures, enums, interfaces, methods, fields or properties
because it may be changed even in minor updates of package.

MIT License

Copyright (c) 2018-2021 Nikolay Pianikov

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
    using Microsoft.Extensions.DependencyInjection;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal sealed class ServiceProvider : IServiceProvider, ISupportRequiredService
    {
        private static readonly object[] EmptyArgs = new object[0];
        private readonly IContainer _container;

        public ServiceProvider([NotNull] IContainer container) =>
            _container = container ?? throw new ArgumentNullException(nameof(container));

        public object GetService(Type serviceType) => 
            _container.TryGetResolver<object>(serviceType, null, out var resolver, out var error) 
                ? resolver(_container, EmptyArgs)
                : null;


        public object GetRequiredService(Type serviceType) =>
            _container.GetResolver<object>(serviceType)(_container, EmptyArgs);
    }
}


#endregion
#region ServiceProviderFactory

// ReSharper disable UnusedType.Global
namespace IoC.Features.AspNetCore
{
    using System;
    using System.Threading;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Creates a builder and an <see cref="T:System.IServiceProvider" />.
    /// </summary>
    public sealed class ServiceProviderFactory : IServiceProviderFactory<IContainer>, IDisposable
    {
        private static long _counter;
        private readonly IMutableContainer _container;

        /// <summary>
        /// Creates a new instance of <c>IServiceProviderFactory</c>.
        /// </summary>
        /// <param name="container"></param>
        public ServiceProviderFactory(IContainer container) =>
            _container = container.Create("ASP.NET");

        /// <inheritdoc />
        public IContainer CreateBuilder(IServiceCollection services) => 
            _container
                .Create($"builder_{Interlocked.Increment(ref _counter)}")
                .Using(new AspNetCoreFeature(services));

        /// <inheritdoc />
        public IServiceProvider CreateServiceProvider(IContainer containerBuilder) =>
            containerBuilder.Resolve<IServiceProvider>();

        /// <inheritdoc />
        public void Dispose() => _container?.Dispose();
    }
}


#endregion
#region ServiceScope

// ReSharper disable ClassNeverInstantiated.Global
namespace IoC.Features.AspNetCore
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    internal sealed class ServiceScope : IServiceScope, IServiceProvider, ISupportRequiredService
    {
        private static readonly object[] EmptyArgs = new object[0];
        [NotNull] private readonly IScope _scope;
        
        public ServiceScope(
            [NotNull] IScope scope) =>
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));

        public IServiceProvider ServiceProvider => this;

        public object GetService(Type serviceType)
        {
            using (_scope.Activate())
            {
                var container = _scope.Container;
                return container.TryGetResolver<object>(serviceType, null, out var resolver, out var error)
                    ? resolver(container, EmptyArgs)
                    : null;
            }
        }

        public object GetRequiredService(Type serviceType)
        {
            using (_scope.Activate())
            {
                var container = _scope.Container;
                return container.GetResolver<object>(serviceType)(container, EmptyArgs);
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
    internal sealed class ServiceScopeFactory : IServiceScopeFactory
    {
        [NotNull] private readonly Func<IServiceScope> _serviceScopeFactory;

        public ServiceScopeFactory([NotNull] Func<IServiceScope> serviceScopeFactory) => 
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));

        public IServiceScope CreateScope() => _serviceScopeFactory();
    }
}


#endregion
#region TagKeyComparer

namespace IoC.Features.AspNetCore
{
    using System.Collections.Generic;

    internal sealed class TagKeyComparer : IComparer<Key>
    {
        public static readonly IComparer<Key> Shared = new TagKeyComparer();

        private TagKeyComparer() { }

        public int Compare(Key x, Key y) =>
            -Comparer<object>.Default.Compare(x.Tag, y.Tag);
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


#endregion

#endregion

#pragma warning disable CS1658 // Warning is overriding an error
#pragma warning restore nullable
#pragma warning restore CS1658 // Warning is overriding an error

// ReSharper restore All