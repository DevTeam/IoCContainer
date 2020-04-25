namespace IoC.Benchmark
{
    using global::Ninject;
    using Model;

    public static class Ninject
    {
        public static StandardKernel Singleton()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IService1>().To<Service1>();
            kernel.Bind<IService2>().To<Service2>().InSingletonScope();
            kernel.Bind<IService3>().To<Service3>();
            kernel.Bind<IService4>().To<Service4>();
            return kernel;
        }

        public static StandardKernel Transient()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IService1>().To<Service1>();
            kernel.Bind<IService2>().To<Service2>();
            kernel.Bind<IService3>().To<Service3>();
            kernel.Bind<IService4>().To<Service4>();
            return kernel;
        }

        public static StandardKernel Complex()
        {
            var kernel = new StandardKernel();
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                kernel.Bind(type).To(type);
            }

            return kernel;
        }
    }
}