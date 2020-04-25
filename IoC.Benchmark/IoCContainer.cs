namespace IoC.Benchmark
{
    using Features;
    using Model;

    public static class IoCContainer
    {
        public static Container Singleton()
        {
            var container = Container.Create(CoreFeature.Set);
            container
                .Bind<IService1>().To<Service1>()
                .Bind<IService2>().As(Lifetime.Singleton).To<Service2>()
                .Bind<IService3>().To<Service3>()
                .Bind<IService4>().To<Service4>();
            return container;
        }

        public static Container Transient()
        {
            var container = Container.Create(CoreFeature.Set);
            container
                .Bind<IService1>().To<Service1>()
                .Bind<IService2>().To<Service2>()
                .Bind<IService3>().To<Service3>()
                .Bind<IService4>().To<Service4>();
            return container;
        }

        public static Container Complex()
        {
            var container = Container.Create(CoreFeature.Set);
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                container.Bind(type).To(type);
            }

            return container;
        }
    }
}