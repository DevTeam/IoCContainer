namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ChildContainer
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=1 Basics
            // $priority=03
            // $description=Child container
            // $header=Child containers allow to override or just to add bindings without any influence on parent containers. This is useful when few components have their own child containers with additional bindings based on a common parent container.
            // {
            using var parentContainer = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind IService to Service
                .Bind<IService>().To<Service>()
                .Container;

            using var childContainer = parentContainer
                .Create()
                // Override binding of IService to Service<int>
                .Bind<IService>().To<Service<int>>()
                .Container;

            var instance1 = parentContainer.Resolve<IService>();
            var instance2 = childContainer.Resolve<IService>();

            childContainer.Parent.ShouldBe(parentContainer);
            instance1.ShouldBeOfType<Service>();
            instance2.ShouldBeOfType<Service<int>>();
            // }
        }
    }
}
