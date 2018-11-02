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
            // $priority=02
            // $description=Resolve Func
            // {
            // Create and configure the container
            using (var container = Container.Create())
            // Bind some dependency
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            {
                // Resolve function to get instances
                var func = container.Resolve<Func<IService>>();
                
                // Get an instance
                var instance = func();
                // }
                // Check the instance's type
                instance.ShouldBeOfType<Service>();
                // {
            }
            // }
        }
    }
}
