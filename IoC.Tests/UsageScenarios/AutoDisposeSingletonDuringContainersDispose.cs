namespace IoC.Tests.UsageScenarios
{
    using Moq;
    using Shouldly;
    using Xunit;

    public class AutoDisposeSingletonDuringContainersDispose
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=06
            // $description=Auto dispose singleton during container's dispose
            // {
            var disposableService = new Mock<IDisposableService>();

            // Create a container
            using (var container = Container.Create())
            {
                // Configure the container
                container.Bind<IService>().As(Lifetime.Singleton).To<IDisposableService>(ctx => disposableService.Object).ToSelf();

                // Resolve instances
                var instance1 = container.Get<IService>();
                var instance2 = container.Get<IService>();

                instance1.ShouldBe(instance2);
            }

            disposableService.Verify(i => i.Dispose(), Times.Once);
            // }
        }
    }
}
