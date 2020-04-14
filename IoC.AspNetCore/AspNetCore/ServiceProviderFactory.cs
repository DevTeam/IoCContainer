namespace IoC.Features.AspNetCore
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Creates a builder and an <see cref="T:System.IServiceProvider" />.
    /// </summary>
    public sealed class ServiceProviderFactory : IServiceProviderFactory<IContainer>, IDisposable
    {
        private readonly IMutableContainer _container;

        /// <summary>
        /// Creates a new instance of <c>IServiceProviderFactory</c>.
        /// </summary>
        /// <param name="container"></param>
        public ServiceProviderFactory(IContainer container) => _container = container.Create();

        /// <inheritdoc />
        public IContainer CreateBuilder(IServiceCollection services) => _container.Using(new AspNetCoreFeature(services));

        /// <inheritdoc />
        public IServiceProvider CreateServiceProvider(IContainer containerBuilder) => containerBuilder.Resolve<IServiceProvider>();

        /// <inheritdoc />
        public void Dispose() => _container?.Dispose();
    }
}
