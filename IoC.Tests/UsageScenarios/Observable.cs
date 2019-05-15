namespace IoC.Tests.UsageScenarios
{
    using System;
    using Moq;
    using Xunit;

    public class Observable
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=injection
            // $priority=05
            // $description=Resolve all appropriate instances as IObservable source
            // {
            // Create and configure the container
            using var container = Container.Create();
            using (container.Bind<IDependency>().To<Dependency>())
                // Bind to the implementation #1
            using (container.Bind<IService>().Tag(1).To<Service>())
                // Bind to the implementation #2
            using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
                // Bind to the implementation #3
            using (container.Bind<IService>().Tag(3).To<Service>())
            {
                // Resolve the source for all appropriate instances
                var instancesSource = container.Resolve<IObservable<IService>>();
                // }
                // Create mock of observer to check
                var observer = new Mock<IObserver<IService>>();
                using (instancesSource.Subscribe(observer.Object))
                {
                    // Check the number of resolved instances
                    observer.Verify(o => o.OnNext(It.IsAny<IService>()), Times.Exactly(3));

                    // Check that the finishing event was received
                    observer.Verify(o => o.OnCompleted(), Times.Once);
                }
                // {
            }

            // }
        }
    }
}
