namespace IoC.Benchmark
{
    using global::DryIoc;
    using Model;

    public static class DryIoc
    {
        public static Container Singleton()
        {
            var container = new Container();
            container.Register<IService1, Service1>();
            container.Register<IService2, Service2>(Reuse.Singleton);
            container.Register<IService3, Service3>();
            container.Register<IService4, Service4>();
            return container;
        }

        public static Container Transient()
        {
            var container = new Container();
            container.Register<IService1, Service1>();
            container.Register<IService2, Service2>();
            container.Register<IService3, Service3>();
            container.Register<IService4, Service4>();
            return container;
        }

        public static Container Complex()
        {
            var container = new Container();
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                container.Register(type);
            }

            return container;
        }
    }
}