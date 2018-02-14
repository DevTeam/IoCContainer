namespace IoC.Tests.IntegrationTests
{
    using Moq;
    using Shouldly;
    using Xunit;

    public class PureContainerTests
    {
        [Fact]
        public void ContainerShouldResolveFromInstanceWhenPureContainer()
        {
            // Given
            using (var container = Container.CreatePure())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>();
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveAutowiringWhenPureContainer()
        {
            // Given
            using (var container = Container.CreatePure())
            {
                // When
                using (container.Bind<MySimpleClass>().To())
                {
                    // Then
                    var actualInstance = container.Get<MySimpleClass>();
                    actualInstance.ShouldBeOfType<MySimpleClass>();
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveAutowiringSingletonWhenPureContainer()
        {
            // Given
            using (var container = Container.CreatePure())
            {
                // When
                using (container.Bind<MySimpleClass>().As(Lifetime.Singleton).To())
                {
                    // Then
                    var actualInstance1 = container.Get<MySimpleClass>();
                    var actualInstance2 = container.Get<MySimpleClass>();

                    actualInstance1.ShouldBeOfType<MySimpleClass>();
                    actualInstance1.ShouldBe(actualInstance2);
                }
            }
        }
    }
}