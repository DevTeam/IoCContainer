namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ConstructorChoice
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=advanced
            // $priority=04
            // $description=Constructor choice
            // $header
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>(
                    // Select the constructor and inject required dependencies
                    ctx => new Service(ctx.Container.Inject<IDependency>(), "some state"))
                .Container;

            var instance = container.Resolve<IService>();
            // }
            // Check the type
            instance.ShouldBeOfType<Service>();
            // {

            // Check the injected constant
            instance.State.ShouldBe("some state");
            // }
        }
    }
}
