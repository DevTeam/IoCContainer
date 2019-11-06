namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class AutoWiring
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=00
            // $description=Autowiring
            // $header=Auto-writing is most natural way to use containers. At first step we should create a container. At the second step we bind interfaces to their implementations. After that the container is ready to resolve dependencies.
            // {
            // Create the container and configure it, using full autowiring
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                .Container;

            // Resolve an instance of interface `IService`
            var instance = container.Resolve<IService>();
            // }
            // Check the instance's type
            instance.ShouldBeOfType<Service>();
        }
    }
}
