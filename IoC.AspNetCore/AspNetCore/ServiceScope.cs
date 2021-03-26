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
