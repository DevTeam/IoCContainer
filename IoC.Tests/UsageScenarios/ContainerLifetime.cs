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
            // $tag=2 Lifetimes
            // $priority=01
            // $description=Container Singleton lifetime
            // $header=Each container may have its own [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instance for specific binding.
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Use the Container Singleton lifetime
                .Bind<IService>().As(ContainerSingleton).To<Service>()
                .Container;

            // Resolve the container singleton twice
            var parentInstance1 = container.Resolve<IService>();
            var parentInstance2 = container.Resolve<IService>();

            // Check that instances from the parent container are equal
            parentInstance1.ShouldBe(parentInstance2);

            // Create a child container
            using var childContainer = container.Create();
            // Resolve the container singleton twice
            var childInstance1 = childContainer.Resolve<IService>();
            var childInstance2 = childContainer.Resolve<IService>();

            // Check that instances from the child container are equal
            childInstance1.ShouldBe(childInstance2);

            // Check that instances from different containers are not equal
            parentInstance1.ShouldNotBe(childInstance1);
            // }
        }
    }
}
