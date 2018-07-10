namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ConstructorAutowiring
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=injection
            // $priority=04
            // $description=Constructor Auto-wiring
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure a container
            // Use full auto-wiring
            using (container.Bind<IDependency>().To<Dependency>())
            // Configure via auto-wiring
            using (container.Bind<IService>().To<Service>(
                // Select the constructor and specify its arguments
                ctx => new Service(ctx.Container.Inject<IDependency>(), "some state")))
            {
                // Resolve an instance
                var instance = container.Resolve<IService>();

                instance.ShouldBeOfType<Service>();
                instance.State.ShouldBe("some state");
            }
            // }
        }
    }
}
