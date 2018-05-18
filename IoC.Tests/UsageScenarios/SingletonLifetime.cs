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
            // $tag=binding
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
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IService>();

                // Create a child container
                using (var childContainer = container.CreateChild())
                {
                    var instance3 = childContainer.Resolve<IService>();

                    instance1.ShouldBe(instance2);
                    instance1.ShouldBe(instance3);
                }
            }

            // Lifetimes:
            // Transient - A new instance each time (default)
            // Singleton - Single instance per dependency
            // ContainerSingleton - Singleton per container
            // ScopeSingleton - Singleton per scope
            // }
        }
    }
}
