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
            // $priority=03
            // $description=Auto-wiring
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            // Use full auto-wiring
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            {
                // Resolve an instance
                var instance = container.Resolve<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
