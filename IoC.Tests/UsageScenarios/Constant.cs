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
            // $tag=binding
            // $priority=02
            // $description=Constant
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IService>().To(ctx => new Service(new Dependency())))
            {
                // Resolve an instance
                var instance = container.Resolve<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
