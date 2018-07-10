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
            // $tag=binding
            // $priority=05
            // $description=Func With Arguments
            // {
            // Create the container
            Func<IDependency, string, INamedService> func = 
                (dependency, name) => new NamedService(dependency, name);

            using (var container = Container.Create())
            // Configure a container, using full auto-wiring
            using (container.Bind<IDependency>().To<Dependency>())
            // Select the constructor and inject argument[0] as the second parameter of type 'string'
            using (container.Bind<INamedService>().To(
                ctx => func(ctx.Container.Inject<IDependency>(), (string)ctx.Args[0])))
            {
                // Resolve the instance "alpha" passing the array of arguments
                var instance = container.Resolve<INamedService>("alpha");

                instance.ShouldBeOfType<NamedService>();
                instance.Name.ShouldBe("alpha");

                // Resolve the instance "beta"
                var getterFunc = container.Resolve<Func<string, INamedService>>();
                var otherInstance = getterFunc("beta");
                otherInstance.Name.ShouldBe("beta");
            }
            // }
        }
    }
}
