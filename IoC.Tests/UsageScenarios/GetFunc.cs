namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class GetFunc
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=02
            // $description=Get Func
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            {
                // Resolve Func
                var func = container.Get<Func<IService>>();
                // Get the instance via Func
                var instance = func();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
