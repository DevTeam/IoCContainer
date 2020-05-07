namespace IoC.Benchmark.Containers
{
    using System;
    using global::Autofac;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Autofac: IAbstractContainer<IContainer>
    {
        private readonly ContainerBuilder _builder = new ContainerBuilder();
        private readonly Lazy<IContainer> _container;

        public Autofac() => _container = new Lazy<IContainer>(() => _builder.Build());

        public IContainer ActualContainer => _container.Value;

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            var registration = _builder.RegisterType(implementationType).As(contractType);
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    break;

                case AbstractLifetime.Singleton:
                    registration.SingleInstance();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }

            if (name != null)
            {
                registration.Keyed<object>(name);
            }
        }

        public void Dispose() => _container.Value.Dispose();
    }
}