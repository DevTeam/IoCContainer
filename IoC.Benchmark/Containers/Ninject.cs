namespace IoC.Benchmark.Containers
{
    using System;
    using global::Ninject;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Ninject: IAbstractContainer<StandardKernel>
    {
        public StandardKernel ActualContainer { get; } = new StandardKernel();

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            var bind = ActualContainer.Bind(contractType).To(implementationType);
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

        public void Dispose() => ActualContainer.Dispose();
    }
}