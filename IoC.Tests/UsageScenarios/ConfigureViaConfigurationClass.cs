namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class ConfigureViaConfigurationClass
    {
        [Fact]
        // $visible=true
        // $group=02
        // $priority=00
        // $description=Configure Via Configuration class
        // {
        public void Run()
        {
            // Create the container and configure it
            using (var container = Container.Create().Using<Glue>())
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
        }

        public class Glue : IConfiguration
        {
            public IEnumerable<IDisposable> Apply(IContainer container)
            {
                yield return container.Bind<IDependency>().To<Dependency>();
                yield return container.Bind<IService>().To<Service>();
            }
        }
        // }
    }
}
