namespace IoC.Benchmark
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Model;

    public static class CastleWindsor
    {
        public static WindsorContainer Singleton()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IService1>().ImplementedBy<Service1>().LifestyleTransient());
            container.Register(Component.For<IService2>().ImplementedBy<Service2>().LifestyleSingleton());
            container.Register(Component.For<IService3>().ImplementedBy<Service3>().LifestyleTransient());
            container.Register(Component.For<IService4>().ImplementedBy<Service4>().LifestyleTransient());
            return container;
        }

        public static WindsorContainer Transient()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IService1>().ImplementedBy<Service1>().LifestyleTransient());
            container.Register(Component.For<IService2>().ImplementedBy<Service2>().LifestyleTransient());
            container.Register(Component.For<IService3>().ImplementedBy<Service3>().LifestyleTransient());
            container.Register(Component.For<IService4>().ImplementedBy<Service4>().LifestyleTransient());
            return container;
        }

        public static WindsorContainer Complex()
        {
            var container = new WindsorContainer();
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                container.Register(Component.For(type).ImplementedBy(type).LifestyleTransient());
            }

            return container;
        }
    }
}