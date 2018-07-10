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
            // $tag=injection
            // $priority=05
            // $description=Resolve Using Arguments
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<INamedService>().To<NamedService>(
                // Select the constructor and inject its parameters
                ctx => new NamedService(ctx.Container.Inject<IDependency>(), (string)ctx.Args[0])))
            {
                // Resolve the instance "alpha"
                var instance = container.Resolve<INamedService>("alpha");

                instance.ShouldBeOfType<NamedService>();
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
