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
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
                using (var childContainer = container.Tag(ContainerReference.Child).Get<IContainer>())
                {
                    // Then
                    var actualInstance = childContainer.Get<IMyService>();
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData(ContainerReference.Current)]
        public void ContainerShouldResolveCurrentContainer(ContainerReference? containerReference)
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (var curContainer = containerReference.HasValue ? container.Tag(containerReference.Value).Get<IContainer>() : container.Get<IContainer>())
                {
                    // Then
                    curContainer.ShouldBe(container);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveParentContainer()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                var curContainer = container.Tag(ContainerReference.Parent).Get<IContainer>();

                // Then
                curContainer.ShouldBe(container.Parent);
            }
        }
    }
}