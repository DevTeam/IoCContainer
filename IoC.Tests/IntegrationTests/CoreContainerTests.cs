namespace IoC.Tests.IntegrationTests
{
    using Moq;
    using Shouldly;
    using Xunit;

    public class CoreContainerTests
    {
        [Fact]
        public void ContainerShouldResolveFromInstanceWhenPureContainer()
        {
            // Given
            using var container = Container.Create(Features.CoreFeature.Set);
            var expectedInstance = Mock.Of<IMyService>();

            // When
            using (container.Bind<IMyService>().As(Lifetime.Transient).To(ctx => expectedInstance))
            {
                // Then
                var actualInstance = container.Resolve<IMyService>();
                actualInstance.ShouldBe(expectedInstance);
            }
        }

        [Fact]
        public void ContainerShouldResolveAutowiringWhenPureContainer()
        {
            // Given
            using var container = Container.Create(Features.CoreFeature.Set);

            // When
            using (container.Bind<MySimpleClass>().To())
            {
                // Then
                var actualInstance = container.Resolve<MySimpleClass>();
                actualInstance.ShouldBeOfType<MySimpleClass>();
            }
        }

        [Fact]
        public void ContainerShouldResolveAutowiringSingletonWhenPureContainer()
        {
            // Given
            using var container = Container.Create(Features.CoreFeature.Set);

            // When
            using (container.Bind<MySimpleClass>().As(Lifetime.Singleton).To())
            {
                // Then
                var actualInstance1 = container.Resolve<MySimpleClass>();
                var actualInstance2 = container.Resolve<MySimpleClass>();

                actualInstance1.ShouldBeOfType<MySimpleClass>();
                actualInstance1.ShouldBe(actualInstance2);
            }
        }
    }
}