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
            // $group=01
            // $priority=04
            // $description=Constructor Auto-wiring
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            // Use full auto-wiring
            using (container.Bind<IDependency>().To<Dependency>())
            // Configure auto-wiring
            using (container.Bind<IService>().To<Service>(
                // Configure the constructor to use
                ctx => new Service(ctx.Container.Inject<IDependency>(), "some state")))
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
                instance.State.ShouldBe("some state");
            }
            // }
        }
    }
}
