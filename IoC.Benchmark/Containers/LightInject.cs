namespace IoC.Benchmark.Containers
{
    using System;
    using Castle.Core.Internal;
    using global::LightInject;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class LightInject: IAbstractContainer<ServiceContainer>
    {
        private readonly ServiceContainer _container = new ServiceContainer();

        public ServiceContainer CreateContainer() => _container;

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
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

        public void Dispose() => _container.Dispose();
    }
}