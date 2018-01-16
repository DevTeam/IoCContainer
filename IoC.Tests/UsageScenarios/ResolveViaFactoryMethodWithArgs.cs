namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class ResolveViaFactoryMethodWithArgs
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=05
            // $description=Factory Method With Arguments
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<INamedService>().ToFunc(ctx => new NamedService(ctx.Container.Get<IDependency>(), (string)ctx.Args[0])))
            {
                // Resolve the instance "alpha"
                var instance = container.Get<INamedService>("alpha");

                instance.ShouldBeOfType<NamedService>();
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
