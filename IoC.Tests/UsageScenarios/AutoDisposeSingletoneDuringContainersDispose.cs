namespace IoC.Tests.UsageScenarios
{
    using Moq;
    using Shouldly;
    using Xunit;

    public class AutoDisposeSingletoneDuringContainersDispose
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=06
            // $description=Auto dispose singletone during container's dispose
            // {
            var disposableService = new Mock<IDisposableService>();

            // Create the container
            using (var container = Container.Create())
            {
                // Configure the container
                container.Bind<IService>().Lifetime(Lifetime.Singletone).ToFunc(() => disposableService.Object).ToSelf();

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
