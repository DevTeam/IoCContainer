namespace IoC.Benchmark
{
    using global::Autofac;
    using Model;

    public static class Autofac
    {
        public static IContainer Singleton()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Service1>().As<IService1>();
            builder.RegisterType<Service2>().As<IService2>().SingleInstance();
            builder.RegisterType<Service3>().As<IService3>();
            builder.RegisterType<Service4>().As<IService4>();
            return builder.Build();
        }

        public static IContainer Transient()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Service1>().As<IService1>();
            builder.RegisterType<Service2>().As<IService2>();
            builder.RegisterType<Service3>().As<IService3>();
            builder.RegisterType<Service4>().As<IService4>();
            return builder.Build();
        }

        public static IContainer Complex()
        {
            var builder = new ContainerBuilder();
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                builder.RegisterType(type).As(type);
            }

            return builder.Build();
        }
    }
}