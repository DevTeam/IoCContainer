namespace IoC.Benchmark.Containers
{
    using System;
    using Castle.Core.Internal;
    using global::LightInject;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class LightInject: IAbstractContainer<ServiceContainer>
    {
        public ServiceContainer ActualContainer { get; } = new ServiceContainer();

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    if (name.IsNullOrEmpty())
                    {
                        ActualContainer.Register(contractType, implementationType);
                    }
                    else
                    {
                        ActualContainer.Register(contractType, implementationType, name);
                    }

                    break;

                case AbstractLifetime.Singleton:
                    if (name.IsNullOrEmpty())
                    {
                        ActualContainer.Register(contractType, implementationType, new PerContainerLifetime());
                    }
                    else
                    {
                        ActualContainer.Register(contractType, implementationType, name, new PerContainerLifetime());
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        public void Dispose() => ActualContainer.Dispose();
    }
}