namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Lazy
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=3 BCL types
            // $priority=01
            // $description=Lazy
            // $header=_Lazy_ dependency helps when a logic needs to inject _Lazy<T>_ to get instance once on demand.
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                .Container;

            // Resolve the instance of Lazy<IService>
            var lazy = container.Resolve<Lazy<IService>>();

            // Get the instance via Lazy
            var instance = lazy.Value;
            // }
            // Check the instance
            instance.ShouldBeOfType<Service>();
        }
    }
}
