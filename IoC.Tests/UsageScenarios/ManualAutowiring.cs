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
            // $description=Manual Autowiring
            // {
            // Create and configure the container using full autowiring
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
                .Bind<INamedService>().To<InitializingNamedService>(
                    // Select the constructor and inject the dependency
                    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
                    // Configure the initializing method to invoke after the instance creation and inject the dependencies
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
