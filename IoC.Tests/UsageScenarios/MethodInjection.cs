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
            // $tag=injection
            // $priority=03
            // $description=Method Injection
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            // Use full auto-wiring
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<INamedService>().To<InitializingNamedService>(
                // Select the constructor
                ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
                // Select the method and inject its parameters
                // First as arguments[0]
                // Second as just dependency of type IDependency
                ctx => ctx.It.Initialize((string)ctx.Args[0], ctx.Container.Inject<IDependency>())))
            {
                // Resolve the instance "alpha"
                var instance = container.Resolve<INamedService>("alpha");

                instance.ShouldBeOfType<InitializingNamedService>();
                instance.Name.ShouldBe("alpha");

                // Resolve the instance "beta"
                var func = container.Resolve<Func<string, INamedService>>();
                var otherInstance = func("beta");
                otherInstance.Name.ShouldBe("beta");
            }
            // }
        }
    }
}
