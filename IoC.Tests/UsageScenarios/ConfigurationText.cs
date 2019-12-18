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
            // $tag=binding
            // $priority=07
            // $description=Configuration via a text metadata
            // {
            // Create and configure the container from a metadata string
            using var container = Container.Create().Apply(
                "ref IoC.Tests;" +
                "using IoC.Tests.UsageScenarios;" +
                "Bind<IDependency>().As(Singleton).To<Dependency>();" +
                "Bind<IService>().To<Service>();")
                .Container;
            // Resolve an instance
            var instance = container.Resolve<IService>();
            // }
            // Check the instance's type
            instance.ShouldBeOfType<Service>();
            // {
            // }
        }
    }
}
