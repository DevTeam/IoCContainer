namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class Func
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=04
            // $description=Func
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IService>().ToFunc(() => new Service(new Dependency())))
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
