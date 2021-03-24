namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class Configurations
    {
        [Fact]
        // $visible=true
        // $tag=1 Basics
        // $priority=02
        // $description=Configurations
        // $header=Configurations are used to dedicate a logic responsible for configuring containers.
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
                yield return container
                    .Bind<IDependency>().To<Dependency>()
                    .Bind<IService>().To<Service>();
            }
        }
        // }
    }
}
