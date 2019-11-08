namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ResolveFunc
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=injection
            // $priority=01
            // $description=Resolve Func
            // $header=_Func_ dependency helps when a logic requires to inject some number of type's instances on demand.
            // {
            // Create and configure the container
            // Create and configure the container
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                .Container;

            // Resolve function to create instances
            var factory = container.Resolve<Func<IService>>();

            // Resolve instances
            var instance1 = factory();
            var instance2 = factory();
            // }
            // Check the instance's type
            instance1.ShouldBeOfType<Service>();
            instance2.ShouldBeOfType<Service>();
        }
    }
}
