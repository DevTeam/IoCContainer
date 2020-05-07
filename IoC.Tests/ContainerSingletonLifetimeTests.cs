namespace IoC.Tests
{
    using System;
    using Lifetimes;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ContainerSingletonLifetimeTests
    {
        [Fact]
        public void ShouldCreateSingleInstance()
        {
            // Given
            var lifetime = new ContainerSingletonLifetime();
            var resolver = lifetime.Compile(() => new object());
            var container1 = Mock.Of<IContainer>();
            var container2 = Mock.Of<IContainer>();

            // When
            var instance11 = resolver(container1);
            var instance21 = resolver(container2);
            var instance12 = resolver(container1);
            var instance22 = resolver(container2);

            // Then
            instance11.ShouldBe(instance12);
            instance21.ShouldBe(instance22);
            instance11.ShouldNotBe(instance22);
        }

        [Fact]
        public void ShouldCreateSingleInstanceWhenStruct()
        {
            // Given
            var lifetime = new ContainerSingletonLifetime();
            var resolver = lifetime.Compile(() => new object().GetHashCode());
            var container1 = Mock.Of<IContainer>();
            var container2 = Mock.Of<IContainer>();

            // When
            var instance11 = resolver(container1);
            var instance21 = resolver(container2);
            var instance12 = resolver(container1);
            var instance22 = resolver(container2);

            // Then
            instance11.ShouldBe(instance12);
            instance21.ShouldBe(instance22);
            instance11.ShouldNotBe(instance22);
        }

        [Fact]
        public void ShouldDisposeInstanceWhenDispose()
        {
            // Given
            var expectedInstance = new Mock<IDisposable>();
            var lifetime = new ContainerSingletonLifetime();
            var resolver = lifetime.Compile(() => expectedInstance.Object);
            resolver(Mock.Of<IContainer>());

            // When
            lifetime.Dispose();

            // Then
            expectedInstance.Verify(i => i.Dispose(), Times.Once);
        }
    }
}
