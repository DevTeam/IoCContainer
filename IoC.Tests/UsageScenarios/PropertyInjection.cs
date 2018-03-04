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
            // $group=01
            // $priority=03
            // $description=Property Injection
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<INamedService>().To<InitializingNamedService>(
                // Select the constructor to use
                ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
                // Select the property to inject
                // And inject arguments[0]
                ctx => ctx.Container.Inject(ctx.It.Name, (string)ctx.Args[0])))
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
