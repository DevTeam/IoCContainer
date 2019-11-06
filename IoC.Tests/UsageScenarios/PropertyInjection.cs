namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class PropertyInjection
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=injection
            // $priority=03
            // $description=Property Injection
            // {
            // Create and configure the container
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
                .Bind<INamedService>().To<InitializingNamedService>(
                    // Select the constructor and inject the dependency
                    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
                    // Select the property to inject after the instance creation and inject the value from arguments at index 0
                    ctx => ctx.Container.Inject(ctx.It.Name, (string) ctx.Args[0]))
                .Container;

            // Resolve the instance using the argument "alpha"
            var instance = container.Resolve<INamedService>("alpha");

            // Check the instance's type
            instance.ShouldBeOfType<InitializingNamedService>();

            // Check the injected dependency
            instance.Name.ShouldBe("alpha");

            // Resolve a function to create an instance
            var func = container.Resolve<Func<string, INamedService>>();

            // Create an instance with the argument "beta"
            var otherInstance = func("beta");

            // Check the injected dependency
            otherInstance.Name.ShouldBe("beta");

            // }
        }
    }
}
