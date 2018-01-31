namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class FuncWithContext
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=04
            // $description=Func with context
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IService>().ToFunc(ctx => new Service(new Dependency())))
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
