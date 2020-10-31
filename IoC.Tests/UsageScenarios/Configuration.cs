namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class Configuration
    {
        [Fact]
        // $visible=true
        // $tag=1 Basics
        // $priority=02
        // $description=Configuration
        // $header=Configuration classes are used to dedicate a logic responsible for configuring containers.
        // {
        public void Run()
        {
            using var container = Container
                .Create()
                .Using<Glue>();

            var instance = container.Resolve<IService>();
        // }
            // Check the instance
            instance.ShouldBeOfType<Service>();
        // {
        }

        public class Glue : IConfiguration
        {
            public IEnumerable<IToken> Apply(IMutableContainer container)
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
