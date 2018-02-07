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
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<INamedService>().To<InitializingNamedService>(
                // Configure the constructor to use
                ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
                // Configure the property to initialize
                ctx => ctx.Container.Inject(ctx.It.Name, (string)ctx.Args[0])))
            {
                // Resolve the instance "alpha"
                var instance = container.Get<INamedService>("alpha");

                instance.ShouldBeOfType<InitializingNamedService>();
                instance.Name.ShouldBe("alpha");

                // Resolve the instance "beta"
                var func = container.Get<Func<string, INamedService>>();
                var otherInstance = func("beta");
                otherInstance.Name.ShouldBe("beta");
            }
            // }
        }
    }
}
