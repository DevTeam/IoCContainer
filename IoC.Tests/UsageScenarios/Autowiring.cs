namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class Autowiring
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=00
            // $description=Auto-wiring
            // {
            // Create the container and configure it, using full auto-wiring
            using (var container = Container.Create())
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            {
                // Resolve an instance of interface `IService`
                var instance = container.Resolve<IService>();

                // Check the instance's type
                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
