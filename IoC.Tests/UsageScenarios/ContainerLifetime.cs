namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;
    using static Lifetime;

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
            // Create and configure the container
            using (var container = Container.Create())
            // Bind some dependency
            using (container.Bind<IDependency>().To<Dependency>())
            // Use the Container Singleton lifetime
            using (container.Bind<IService>().As(ContainerSingleton).To<Service>())
            {
                // Resolve the container singleton twice
                var parentInstance1 = container.Resolve<IService>();
                var parentInstance2 = container.Resolve<IService>();

                // Check that instances from the parent container are equal
                parentInstance1.ShouldBe(parentInstance2);

                // Create a child container
                using (var childContainer = container.CreateChild())
                {
                    // Resolve the container singleton twice
                    var childInstance1 = childContainer.Resolve<IService>();
                    var childInstance2 = childContainer.Resolve<IService>();

                    // Check that instances from the child container are equal
                    childInstance1.ShouldBe(childInstance2);

                    // Check that instances from different containers are not equal
                    parentInstance1.ShouldNotBe(childInstance1);
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
