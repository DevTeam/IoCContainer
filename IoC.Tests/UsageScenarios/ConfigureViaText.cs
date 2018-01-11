namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ConfigureViaText
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=02
            // $priority=00
            // $description=Configure Via Text
            // {
            // Create the container and configure from the metadata string
            using (var container = Container.Create().Using(
                "ref IoC.Tests;" +
                "using IoC.Tests.UsageScenarios;" +
                "Bind<IDependency>().To<Dependency>();" +
                "Bind<IService>().To<Service>();"))
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
