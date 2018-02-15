namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ConfigurationText
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=02
            // $priority=00
            // $description=Configuration via a text metadata
            // {
            // Create a container and configure it from the metadata string
            using (var container = Container.Create().Using(
                "ref IoC.Tests;" +
                "using IoC.Tests.UsageScenarios;" +
                "Bind<IDependency>().To<Dependency>();" +
                "Bind<IService>().To<Service>();"))
            {
                // Resolve an instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
