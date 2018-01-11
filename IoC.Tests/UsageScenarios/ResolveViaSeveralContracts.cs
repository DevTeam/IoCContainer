namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ResolveViaSeveralContracts
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=01
            // $description=Several Contracts
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<Service, IService, IAnotherService>().To<Service>())
            {
                // Resolve instances
                var instance1 = container.Get<IService>();
                var instance2 = container.Get<IAnotherService>();

                instance1.ShouldBeOfType<Service>();
                instance2.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
