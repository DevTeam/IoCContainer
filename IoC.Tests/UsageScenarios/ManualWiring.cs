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
            // $tag=basic
            // $priority=04
            // $description=Manual wiring
            // $header=In the case when the full control of creating an instance is required it is possible to do it in simple way without any performance impact.
            // $footer=It's important to note that injection is possible by several ways in the sample above. **The first one** is an expressions like `ctx.Container.Inject<IDependency>()`. It uses the injection context `ctx` to access to the current (or other parents) container and method `Inject` to inject a dependency. But actually this method has no implementation, it ust a marker and it every such method wil be replaced by expression which creates dependency in place without any additional invocations. **Another one way** is to use an expressions like `ctx.Resolve<IDependency>()`. It will access a container each time to resolve a dependency. That is, each time it will look for the necessary binding in the container and call the method to create an instance of the dependency type. **In summary: wherever possible, use the first approach like `ctx.Container.Inject<IDependency>()`.**
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

            // Check the instance
            instance.ShouldBeOfType<InitializingNamedService>();

            // Check the injected dependency
            instance.Name.ShouldBe("some name");
            // }
        }
    }
}
