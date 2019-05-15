namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ResolveLazy
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=injection
            // $priority=02
            // $description=Resolve Lazy
            // {
            // Create and configure the container
            using var container = Container.Create();
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            {
                // Resolve the instance of Lazy<IService>
                var lazy = container.Resolve<Lazy<IService>>();

                // Get the instance via Lazy
                var instance = lazy.Value;
                // }
                // Check the instance's type
                instance.ShouldBeOfType<Service>();
                // {
            }

            // }
        }
    }
}
