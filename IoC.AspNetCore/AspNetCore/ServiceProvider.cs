namespace IoC.Features.AspNetCore
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal sealed class ServiceProvider : IServiceProvider
    {
        private readonly IContainer _container;

        public ServiceProvider([NotNull] IContainer container) => 
            _container = container ?? throw new ArgumentNullException(nameof(container));

        public object GetService(Type serviceType) => 
            _container.GetResolver<object>(serviceType)(_container);
    }
}
