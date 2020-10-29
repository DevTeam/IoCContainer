namespace IoC.Benchmark.Containers
{
    using System;
    using global::DryIoc;

    internal class DryIoc: IAbstractContainer<Container>
    {
        private readonly static Container _container = new Container();

        public Container CreateActualContainer() => _container;

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

            _container.Register(new ReflectionFactory(implementationType, reuse), contractType, name, null, true);
        }

        public void Dispose() => _container.Dispose();
    }
}