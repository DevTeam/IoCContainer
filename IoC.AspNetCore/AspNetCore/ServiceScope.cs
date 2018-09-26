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
