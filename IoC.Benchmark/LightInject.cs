namespace IoC.Benchmark
{
    using global::LightInject;
    using Model;

    public static class LightInject
    {
        public static ServiceContainer Singleton()
        {
            var container = new ServiceContainer();
            container.Register<IService1, Service1>();
            container.Register<IService2, Service2>(new PerContainerLifetime());
            container.Register<IService3, Service3>();
            container.Register<IService4, Service4>();
            return container;
        }

        public static ServiceContainer Transient()
        {
            var container = new ServiceContainer();
            container.Register<IService1, Service1>();
            container.Register<IService2, Service2>();
            container.Register<IService3, Service3>();
            container.Register<IService4, Service4>();
            return container;
        }

        public static ServiceContainer Complex()
        {
            var container = new ServiceContainer();
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                container.Register(type);
            }

            return container;
        }
    }
}