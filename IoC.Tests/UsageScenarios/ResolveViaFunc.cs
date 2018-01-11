namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ResolveViaFunc
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=02
            // $description=Func
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            {
                // Resolve Func
                var func = container.Get<Func<IService>>();
                // Get the instance
                var instance = func();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
