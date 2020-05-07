namespace IoC.Benchmark.Containers
{
    using System;
    using global::DryIoc;

    internal class DryIoc: IAbstractContainer<Container>
    {
        public Container ActualContainer { get; } = new Container();

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            IReuse reuse;
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    reuse = Reuse.Transient;
                    break;

                case AbstractLifetime.Singleton:
                    reuse = Reuse.Singleton;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }

            ActualContainer.Register(new ReflectionFactory(implementationType, reuse), contractType, name, null, true);
        }

        public void Dispose() => ActualContainer.Dispose();
    }
}