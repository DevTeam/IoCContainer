namespace IoC.Tests.IntegrationTests
{
    using Moq;
    using Shouldly;
    using Xunit;

    public class ContainerReferenceTests
    {
        [Fact]
        public void ContainerShouldResolveChildContainer()
        {
            // Given
            using var container = Container.Create();
            var expectedInstance = Mock.Of<IMyService>();

            // When
            using (container.Bind<IMyService>().As(Lifetime.Transient).To(ctx => expectedInstance))
            {
                using var childContainer = container.Resolve<IMutableContainer>();

                // Then
                var actualInstance = childContainer.Resolve<IMyService>();
                actualInstance.ShouldBe(expectedInstance);
            }
        }

        [Fact]
        public void ContainerShouldResolveCurrentContainer()
        {
            // Given
            using var container = Container.Create();

            // When
            var curContainer = container.Resolve<IContainer>();

            // Then
            curContainer.ShouldBe(container);
        }
    }
}