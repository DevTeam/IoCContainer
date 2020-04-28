namespace IoC.Benchmark
{
    using Microsoft.Extensions.DependencyInjection;
    using Model;

    public static class MicrosoftDependencyInjection
    {
        public static ServiceProvider Singleton()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IService1, Service1>();
            serviceCollection.AddSingleton<IService2, Service2>();
            serviceCollection.AddTransient<IService3, Service3>();
            serviceCollection.AddTransient<IService4, Service4>();
            return serviceCollection.BuildServiceProvider();
        }

        public static ServiceProvider Transient()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IService1, Service1>();
            serviceCollection.AddTransient<IService2, Service2>();
            serviceCollection.AddTransient<IService3, Service3>();
            serviceCollection.AddTransient<IService4, Service4>();
            return serviceCollection.BuildServiceProvider();
        }

        public static ServiceProvider Complex()
        {
            var serviceCollection = new ServiceCollection();
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                serviceCollection.AddTransient(type);
            }

            return serviceCollection.BuildServiceProvider();
        }
    }
}
