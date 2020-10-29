namespace IoC.Benchmark.Containers
{
    using System;
    using global::Ninject;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Ninject: IAbstractContainer<StandardKernel>
    {
        private readonly StandardKernel _container = new StandardKernel();

        public StandardKernel CreateActualContainer() => _container;

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            var bind = _container.Bind(contractType).To(implementationType);
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    break;

                case AbstractLifetime.Singleton:
                    bind.InSingletonScope();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }

            if (name != null)
            {
                bind.Named(name);
            }
        }

        public void Dispose() => _container.Dispose();
    }
}