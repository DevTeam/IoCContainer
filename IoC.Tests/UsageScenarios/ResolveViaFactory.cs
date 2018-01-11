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
            using (container.Bind<IService>().To(new Factory()))
            {
                // Resolve the instance
                var instance = container.Get<IService>();
                instance.ShouldBeOfType<Service>();
            }
        }

        public class Factory: IFactory
        {
            public object Create(ResolvingContext context)
            {
                return new Service(new Dependency());
            }
        }
        // }
    }
}
