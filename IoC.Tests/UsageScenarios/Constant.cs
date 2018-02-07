namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class Constant
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=02
            // $description=Constant
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IService>().To(ctx => new Service(new Dependency())))
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
