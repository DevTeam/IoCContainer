namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class ResolveWithArgs
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=1 Basics
            // $priority=06
            // $description=Resolve an instance using arguments
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
                .Bind<INamedService>().To<NamedService>(
                    // Select the constructor and inject the value from arguments at index 0
                    ctx => new NamedService(ctx.Container.Inject<IDependency>(), (string) ctx.Args[0]))
                .Container;

            // Resolve the instance using the argument "alpha"
            var instance = container.Resolve<INamedService>("alpha");

            // Check the instance type
            instance.ShouldBeOfType<NamedService>();

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
