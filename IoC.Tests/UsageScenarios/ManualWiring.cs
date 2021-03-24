namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ManualWiring
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=1 Basics
            // $priority=04
            // $description=Manual wiring
            // $header=In the case when the full control of creating an instance is required it is possible to do it in a simple way without any performance impact.
            // $footer=It's important to note that injection is possible in several ways in the sample above. **The first one** is an expressions like `ctx.Container.Inject<IDependency>()`. It uses the injection context `ctx` to access the current (or other parents) container and method `Inject` to inject a dependency. But actually, this method has no implementation. It just a marker and every such method will be replaced by an expression that creates dependency in place without any additional invocations. **Another way** is to use an expression like `ctx.Resolve<IDependency>()`. It will access a container each time to resolve a dependency. Each time, it will look for the necessary binding in the container and call the method to create an instance of the dependency type. **We recommend: wherever possible, use the first approach like `ctx.Container.Inject<IDependency>()`.**
            // {
            // Create and configure a container using manual wiring
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
                .Bind<INamedService>().To<InitializingNamedService>(
                    // Select the constructor and inject a dependency into it
                    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
                    // Configure the initializing method to invoke for every created instance with all appropriate dependencies
                    // We used _Resolve_ instead _Inject_ just for example
                    ctx => ctx.It.Initialize("some name", ctx.Container.Resolve<IDependency>()))
                .Container;

            // Resolve an instance
            var instance = container.Resolve<INamedService>();

            // Check the instance
            instance.ShouldBeOfType<InitializingNamedService>();

            // Check the injected dependency
            instance.Name.ShouldBe("some name");
            // }
        }
    }
}
