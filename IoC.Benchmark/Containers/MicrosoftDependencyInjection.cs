namespace IoC.Benchmark.Containers
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class MicrosoftDependencyInjection: BaseAbstractContainer<ServiceProvider>
    {
        private IServiceCollection _serviceCollection = new ServiceCollection();
        private readonly Lazy<ServiceProvider> _container;

        public MicrosoftDependencyInjection() =>
            _container = new Lazy<ServiceProvider>(() => _serviceCollection.BuildServiceProvider());

        public override ServiceProvider CreateContainer() => _container.Value;

        public override void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    _serviceCollection.AddTransient(contractType, implementationType);
                    break;

                case AbstractLifetime.Singleton:
                    _serviceCollection.AddSingleton(contractType, implementationType);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        public override T Resolve<T>() where T : class => _container.Value.GetService<T>();

        public override void Register(IServiceCollection services)
        {
            _serviceCollection = services;
        }

        public override void Dispose() { }
    }
}
