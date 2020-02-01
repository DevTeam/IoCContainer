namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ManualAutoWiring
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=04
            // $description=Manual Wiring
            // $header=In the case when the full control of creating an instance is required it is possible to do it in simple way without any performance impact.
            // {
            // Create and configure the container using manual wiring
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
                .Bind<INamedService>().To<InitializingNamedService>(
                    // Select the constructor and inject the dependency into it
                    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
                    // Configure the initializing method to invoke for the every created instance
                    ctx => ctx.It.Initialize("some name", ctx.Container.Inject<IDependency>()))
                .Container;

            // Resolve an instance
            var instance = container.Resolve<INamedService>();

            // Check the instance's type
            instance.ShouldBeOfType<InitializingNamedService>();

            // Check the injected dependency
            instance.Name.ShouldBe("some name");

            // }
        }
    }
}
