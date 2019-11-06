﻿namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class ConfigurationClass
    {
        [Fact]
        // $visible=true
        // $tag=design
        // $priority=00
        // $description=Configuration class
        // {
        public void Run()
        {
            // Create and configure the container
            using var container = Container.Create().Using<Glue>();
            // Resolve an instance
            var instance = container.Resolve<IService>();
            // }
            // Check the instance's type
            instance.ShouldBeOfType<Service>();
            // {
        }

        public class Glue : IConfiguration
        {
            public IEnumerable<IToken> Apply(IContainer container)
            {
                // Bind using full autowiring
                yield return container
                    .Bind<IDependency>().To<Dependency>()
                    .Bind<IService>().To<Service>();
            }
        }
        // }
    }
}
