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
