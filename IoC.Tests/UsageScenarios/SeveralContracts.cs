﻿namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "RedundantTypeArgumentsOfMethod")]
    public class SeveralContracts
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=01
            // $description=Several Contracts
            // {
            // Create and configure the container, using full auto-wiring
            using (var container = Container.Create())
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<Service, IService, IAnotherService>().To<Service>())
            {
                // Resolve instances
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IAnotherService>();

                // Check the instances' types
                instance1.ShouldBeOfType<Service>();
                instance2.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
