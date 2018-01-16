namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ResolveViaFactoryMethod
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
            using (container.Bind<IService>().ToFunc(() => new Service(new Dependency())))
            // Each dependency cound be resolve also using a resolving context
            // using (container.Bind<IService>().To(ctx => new Service(ctx.ResolvingContainer.Get<IDependency>())))
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
