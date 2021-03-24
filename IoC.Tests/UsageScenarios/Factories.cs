// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class Factories
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=1 Basics
            // $priority=01
            // $description=Factories
            // $header=Use _Func<..., T>_ with arguments as a factory passing a state.
            // $footer=It is better to pass a state using a special type (but not via any base type like in the sample above) because in this case, it will be possible to create a complex object graph with a special state for every object within this graph.
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
