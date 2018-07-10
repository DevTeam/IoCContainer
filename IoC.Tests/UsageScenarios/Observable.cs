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
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().Tag(1).To<Service>())
            using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
            using (container.Bind<IService>().Tag(3).To<Service>())
            {
                // Resolve source for all appropriate instances
                var instancesSource = container.Resolve<IObservable<IService>>();

                var observer = new Mock<IObserver<IService>>();
                using (instancesSource.Subscribe(observer.Object))
                {
                    observer.Verify(o => o.OnNext(It.IsAny<IService>()), Times.Exactly(3));
                    observer.Verify(o => o.OnCompleted(), Times.Once);
                }
            }
            // }
        }
    }
}
