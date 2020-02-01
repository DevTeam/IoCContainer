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
            // $header=It is easy to use Func<> with arguments and to pass these arguments to the created instances.
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

            // Resolve a function to create instance
            var getterFunc = container.Resolve<Func<string, INamedService>>();

            // Run this function and pass the string "beta" as argument
            var otherInstance = getterFunc("beta");

            // Check that argument "beta" was used during constructing an instance
            otherInstance.Name.ShouldBe("beta");
            // }
        }
    }
}
