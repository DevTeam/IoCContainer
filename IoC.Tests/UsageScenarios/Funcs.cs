namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Funcs
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=3 BCL types
            // $priority=01
            // $description=Funcs
            // $header=_Func<>_ helps when a logic needs to inject some type of instances on-demand or solve circular dependency issues.
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                .Container;

            // Resolve function to create instances
            var factory = container.Resolve<Func<IService>>();

            // Resolve few instances
            var instance1 = factory();
            var instance2 = factory();
            // }
            // Check each instance
            instance1.ShouldBeOfType<Service>();
            instance2.ShouldBeOfType<Service>();
        }
    }
}
