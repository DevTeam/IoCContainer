namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class AutowiringHints
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=02
            // $description=Auto-wiring hints
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>(Has.Constructor(Has.Value("some state").For("state"))))
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
                instance.State.ShouldBe("some state");
            }
            // }
        }
    }
}
