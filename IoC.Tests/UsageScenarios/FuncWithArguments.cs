namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class FuncWithArguments
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=05
            // $description=Func With Arguments
            // {
            // Create the container
            Func<IDependency, string, INamedService> func = 
                (dependency, name) => new NamedService(dependency, name);

            using (var container = Container.Create())
            // Configure the container
            // Use full auto-wiring
            using (container.Bind<IDependency>().To<Dependency>())
            // Configure auto-wiring for constructor and use element from index as a second argument
            using (container.Bind<INamedService>().To(
                ctx => func(ctx.Container.Inject<IDependency>(), (string)ctx.Args[0])))
            {
                // Resolve the instance "alpha"
                var instance = container.Get<INamedService>("alpha");

                instance.ShouldBeOfType<NamedService>();
                instance.Name.ShouldBe("alpha");

                // Resolve the instance "beta"
                var getterFunc = container.Get<Func<string, INamedService>>();
                var otherInstance = getterFunc("beta");
                otherInstance.Name.ShouldBe("beta");
            }
            // }
        }
    }
}
