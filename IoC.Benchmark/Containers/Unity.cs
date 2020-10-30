namespace IoC.Benchmark.Containers
{
    using System;
    using global::Unity;
    using global::Unity.Lifetime;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Unity: IAbstractContainer<UnityContainer>
    {
        private readonly UnityContainer _container = new UnityContainer();

        public UnityContainer CreateContainer() => _container;

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            ITypeLifetimeManager lifetimeManager = null;
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    break;

                case AbstractLifetime.Singleton:
                    lifetimeManager = new ContainerControlledLifetimeManager();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }

           ((IUnityContainer)_container).RegisterType(contractType, implementationType, name, lifetimeManager);
        }

        public void Dispose() => _container.Dispose();
    }
}