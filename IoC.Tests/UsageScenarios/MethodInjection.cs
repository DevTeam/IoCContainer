namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class MethodInjection
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=1 Basics
            // $priority=03
            // $description=Method injection
            // $header=:warning: Please use the constructor injection instead. The method injection is not recommended because it is a cause of hidden dependencies.
            // $footer=It is possible to use DI aspects (Attributes) to use full autowring instead.
            // {
            // Create and configure a container using full autowiring
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
                .Bind<INamedService>().To<InitializingNamedService>(
                    // Select the constructor and inject a dependency into it
                    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
                    // Configure the initializing method to invoke after the instance creation and inject the dependencies
                    // The first one is the value from context arguments at index 0
                    // The second one - is just dependency injection of type IDependency
                    ctx => ctx.It.Initialize((string) ctx.Args[0], ctx.Container.Inject<IDependency>()))
                .Container;

            // Resolve the instance using the argument "alpha"
            var instance = container.Resolve<INamedService>("alpha");

            // Check the instance type
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
