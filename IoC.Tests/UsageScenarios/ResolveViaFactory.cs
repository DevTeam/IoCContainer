namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ResolveViaFactory
    {
        [Fact]
        // $visible=true
        // $group=01
        // $priority=04
        // $description=Factory
        // {
        public void Run()
        {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IService>().ToFactory((key, cutContainer, args) => new Service(new Dependency())))
            {
                // Resolve the instance
                var instance = container.Get<IService>();
                instance.ShouldBeOfType<Service>();
            }
        }
        // }
    }
}
