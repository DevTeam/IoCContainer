// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class Factory
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=1 Basics
            // $priority=01
            // $description=Factory
            // $header=Use Func<.., T> with arguments as a factory passing a state.
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<INamedService>().To<NamedService>()
                .Container;

            // Resolve a factory
            var factory = container.Resolve<Func<string, INamedService>>();

            // Run factory passing the string "beta" as argument
            var instance = factory("alpha");

            // Check that argument "beta" was used during constructing an instance
            instance.Name.ShouldBe("alpha");
            // }

            // Resolve the instance passing the string "alpha" into the array of arguments
            var otherInstance = container.Resolve<INamedService>("beta");

            // Check that argument "alpha" was used during the construction of an instance
            otherInstance.Name.ShouldBe("beta");
        }
    }
}
