namespace IoC.Benchmark.Containers
{
    using System;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class IoCContainer: IAbstractContainer<Container>
    {
        public IoCContainer() => ActualContainer = Container.Create();

        public Container ActualContainer { get; }

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        { 
            var bind = ActualContainer.Bind(contractType);
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    break;

                case AbstractLifetime.Singleton:
                    bind = bind.As(Lifetime.Singleton);
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

        public void Dispose() => ActualContainer.Dispose();
    }
}