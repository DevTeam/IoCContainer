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
            // $tag=binding
            // $priority=06
            // $description=Auto dispose singleton during container's dispose
            // {
            var disposableService = new Mock<IDisposableService>();

            // Create and configure the container
            using (var container = Container.Create()
                .Bind<IService>().As(Lifetime.Singleton).To<IDisposableService>(ctx => disposableService.Object).ToSelf())
            {
                // Resolve singleton instance twice
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IService>();

                // Check that instances are equal
                instance1.ShouldBe(instance2);
            }

            // Check the singleton was disposed after the container was disposed
            disposableService.Verify(i => i.Dispose(), Times.Once);
            // }
        }
    }
}
