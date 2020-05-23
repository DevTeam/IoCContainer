namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Func
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=basic
            // $priority=04
            // $description=Func
            // $header=_Func_ dependency helps when a logic needs to inject some number of type instances on demand.
            // {
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
            // Check the instances
            instance1.ShouldBeOfType<Service>();
            instance2.ShouldBeOfType<Service>();
        }
    }
}
