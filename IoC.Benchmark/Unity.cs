namespace IoC.Benchmark
{
    using global::Unity;
    using global::Unity.Lifetime;
    using Model;

    public static class Unity
    {
        public static UnityContainer Singleton()
        {
            var container = new UnityContainer();
            container.RegisterType<IService1, Service1>();
            container.RegisterType<IService2, Service2>(new ContainerControlledLifetimeManager());
            container.RegisterType<IService3, Service3>();
            container.RegisterType<IService4, Service4>();
            return container;
        }

        public static UnityContainer Transient()
        {
            var container = new UnityContainer();
            container.RegisterType<IService1, Service1>();
            container.RegisterType<IService2, Service2>(new ContainerControlledLifetimeManager());
            container.RegisterType<IService3, Service3>();
            container.RegisterType<IService4, Service4>();
            return container;
        }

        public static UnityContainer Complex()
        {
            var container = new UnityContainer();
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                container.RegisterType(type, type);
            }

            return container;
        }
    }
}