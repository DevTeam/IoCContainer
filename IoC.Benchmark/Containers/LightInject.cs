namespace IoC.Benchmark.Containers
{
    using System;
    using Castle.Core.Internal;
    using global::LightInject;
    using global::LightInject.Microsoft.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class LightInject: BaseAbstractContainer<ServiceContainer>
    {
        private readonly ServiceContainer _container = new ServiceContainer();

        public override ServiceContainer CreateContainer() => _container;

        public override void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    if (name.IsNullOrEmpty())
                    {
                        _container.Register(contractType, implementationType);
                    }
                    else
                    {
                        _container.Register(contractType, implementationType, name);
                    }

                    break;

                case AbstractLifetime.Singleton:
                    if (name.IsNullOrEmpty())
                    {
                        _container.Register(contractType, implementationType, new PerContainerLifetime());
                    }
                    else
                    {
                        _container.Register(contractType, implementationType, name, new PerContainerLifetime());
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        public override void Register(IServiceCollection services) => _container.CreateServiceProvider(services);

        public override T Resolve<T>() where T : class => _container.GetInstance<T>();

        public override void Dispose() => _container.Dispose();
    }
}