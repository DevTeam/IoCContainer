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
            // $header=Child containers allow to override or just to add bindings of a parent containers without any influence on a parent containers. This is most useful when there are some base parent container(s). And these containers are shared between several components which have its own child containers based on common parent container(s) and have some additional bindings.
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
