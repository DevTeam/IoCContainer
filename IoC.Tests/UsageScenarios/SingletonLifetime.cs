namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class SingletonLifetime
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=03
            // $description=Singleton lifetime
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().As(Lifetime.Singleton).To<Service>())
            {
                // Resolve one instance twice
                var instance1 = container.Get<IService>();
                var instance2 = container.Get<IService>();

                instance1.ShouldBe(instance2);
            }

            // Other lifetimes:
            // Transient - A new instance each time (default)
            // ContainerSingleton - Singleton per container
            // ScopeSingleton - Singleton per scope
            // ThreadSingleton - Singleton per thread for NET 4.0+, .NET Core 1.0+, .NET Standard 2.0+
            // }
        }
    }
}
