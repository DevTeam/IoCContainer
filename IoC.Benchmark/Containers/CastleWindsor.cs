namespace IoC.Benchmark.Containers
{
    using System;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class CastleWindsor: IAbstractContainer<WindsorContainer>
    {
        public WindsorContainer ActualContainer { get; } = new WindsorContainer();

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            var registration = Component.For(contractType).ImplementedBy(implementationType);
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    registration.LifestyleTransient();
                    break;

                case AbstractLifetime.Singleton:
                    registration.LifestyleSingleton();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }

            if (name != null)
            {
                registration.Named(name);
            }

            ActualContainer.Register(registration);
        }

        public void Dispose() => ActualContainer.Dispose();
    }
}