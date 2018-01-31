namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class FactoryMethod
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=04
            // $description=Factory Method
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IService>().ToFactory((key, curContainer, args) => new Service(new Dependency())))
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
