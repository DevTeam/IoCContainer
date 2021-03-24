namespace IoC.Tests.UsageScenarios
{
    using System;
    using Moq;
    using Xunit;

    public class Observables
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=3 BCL types
            // $priority=01
            // $description=Observables
            // $header=To resolve all possible instances of any tags of the specific type as an _IObservable<>_ instance just use the injection _IObservable<T>_
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind to the implementation #1
                .Bind<IService>().Tag(1).To<Service>()
                // Bind to the implementation #2
                .Bind<IService>().Tag(2).Tag("abc").To<Service>()
                // Bind to the implementation #3
                .Bind<IService>().Tag(3).To<Service>()
                .Container;

            // Resolve the source for all appropriate instances
            var instancesSource = container.Resolve<IObservable<IService>>();
            // }
            // Create mock of the observer to check
            var observer = new Mock<IObserver<IService>>();
            using (instancesSource.Subscribe(observer.Object))
            {
                // Check the number of resolved instances
                observer.Verify(o => o.OnNext(It.IsAny<IService>()), Times.Exactly(3));

                // Check that the finishing event was received
                observer.Verify(o => o.OnCompleted(), Times.Once);
            }
        }
    }
}
