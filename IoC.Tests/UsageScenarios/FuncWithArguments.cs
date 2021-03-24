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
            // $tag=3 BCL types
            // $priority=03
            // $description=Func with arguments
            // $header=It is easy to use _Func<..., T>_ with arguments and to pass these arguments to the created instances manually via context arguments.
            // $footer=Besides that, you can rely on full autowring, when it is not needed to specify constructor arguments at all. In this case, all appropriate arguments are matching with context arguments automatically by type.
            // {
            Func<IDependency, string, INamedService> func = 
                (dependency, name) => new NamedService(dependency, name);

            // Create and configure the container, using full autowiring
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind the constructor and inject argument[0] as the second parameter of type 'string'
                .Bind<INamedService>().To(ctx => func(ctx.Container.Inject<IDependency>(), (string)ctx.Args[0]))
                .Container;

            // Resolve the instance passing the string "alpha" into the array of arguments
            var instance = container.Resolve<INamedService>("alpha");

            // Check the instance's type
            instance.ShouldBeOfType<NamedService>();

            // Check that argument "alpha" was used during the construction of an instance
            instance.Name.ShouldBe("alpha");

            // Resolve a factory
            var factory = container.Resolve<Func<string, INamedService>>();

            // Run this function and pass the string "beta" as argument
            var otherInstance = factory("beta");

            // Check that argument "beta" was used during constructing an instance
            otherInstance.Name.ShouldBe("beta");
            // }
        }
    }
}
