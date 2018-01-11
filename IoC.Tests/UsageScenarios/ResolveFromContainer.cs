namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ResolveFromContainer
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=00
            // $description=Resolve from Container
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
