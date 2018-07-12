namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ContainerLifetime
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=03
            // $description=Container Singleton lifetime
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().As(Lifetime.ContainerSingleton).To<Service>())
            {
                // Resolve one instance twice
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IService>();

                // Create a child container
                using (var childContainer = container.CreateChild())
                {
                    var instance3 = childContainer.Resolve<IService>();
                    var instance4 = childContainer.Resolve<IService>();

                    instance1.ShouldBe(instance2);
                    instance1.ShouldNotBe(instance3);
                    instance3.ShouldBe(instance4);
                }
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
