namespace IoC.Benchmark.Containers
{
    using System;
    using Features;
    using Lifetimes;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class IoCContainer: IAbstractContainer<Container>
    {
        private readonly Container _container = Container.Create(LightFeature.Set);

        public Container CreateActualContainer() => _container;

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        { 
            var bind = _container.Bind(contractType);
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    break;

                case AbstractLifetime.Singleton:
                    bind = bind.Lifetime(new SingletonLifetime(false));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }

            if (name != null)
            {
                bind = bind.Tag(name);
            }

            bind.To(implementationType);
        }

        public void Dispose() => _container.Dispose();
    }
}