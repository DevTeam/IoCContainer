﻿namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ConstructorAutowiring
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=injection
            // $priority=04
            // $description=Constructor Autowiring
            // {
            // Create and configure the container, using full autowiring
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Configure via manual injection
                .Bind<IService>().To<Service>(
                    // Select the constructor and inject arguments
                    ctx => new Service(ctx.Container.Inject<IDependency>(), "some state"))
                .Container;
            // Resolve an instance
            var instance = container.Resolve<IService>();
            // }
            // Check the instance's type
            instance.ShouldBeOfType<Service>();
            // {

            // Check the injected constant
            instance.State.ShouldBe("some state");
            // }
        }
    }
}
