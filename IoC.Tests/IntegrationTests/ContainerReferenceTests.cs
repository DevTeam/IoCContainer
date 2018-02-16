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
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(ctx => expectedInstance))
                using (var childContainer = container.Tag(WellknownContainer.Child).Get<IContainer>())
                {
                    // Then
                    var actualInstance = childContainer.Get<IMyService>();
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

#if !NET40
        [Theory]
        [InlineData(null)]
        [InlineData(WellknownContainer.Current)]
        public void ContainerShouldResolveCurrentContainer(WellknownContainer? wellknownContainer)
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (var curContainer = wellknownContainer.HasValue ? container.Tag(wellknownContainer.Value).Get<IContainer>() : container.Get<IContainer>())
                {
                    // Then
                    curContainer.ShouldBe(container);
                }
            }
        }
#endif

        [Fact]
        public void ContainerShouldResolveParentContainer()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                var curContainer = container.Tag(WellknownContainer.Parent).Get<IContainer>();

                // Then
                curContainer.ShouldBe(container.Parent);
            }
        }
    }
}