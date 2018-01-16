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
            // $group=01
            // $priority=05
            // $description=Method Injection
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<INamedService>().To<InitializingNamedService>(Has.Method("Initialize", Has.Argument<string>(0).For("name"))))
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
