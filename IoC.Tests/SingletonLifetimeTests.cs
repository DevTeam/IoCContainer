namespace IoC.Tests
{
    using System;
    using Lifetimes;
    using Moq;
    using Shouldly;
    using Xunit;

    public class SingletonLifetimeTests
    {
        [Fact]
        public void ShouldCreateSingleInstance()
        {
            // Given
            var lifetime = new SingletonLifetime();
            var resolver = lifetime.Compile(() => new object());

            // When
            var instance1 = resolver(Mock.Of<IContainer>());
            var instance2 = resolver(Mock.Of<IContainer>());
            var instance3 = resolver(Mock.Of<IContainer>());

            // Then
            instance2.ShouldBe(instance1);
            instance3.ShouldBe(instance1);
        }

        [Fact]
        public void ShouldCreateSingleInstanceWhenStruct()
        {
            // Given
            var lifetime = new SingletonLifetime();
            var resolver = lifetime.Compile(() => 10);

            // When
            var instance1 = resolver(Mock.Of<IContainer>());
            var instance2 = resolver(Mock.Of<IContainer>());
            var instance3 = resolver(Mock.Of<IContainer>());

            // Then
            instance2.ShouldBe(instance1);
            instance3.ShouldBe(instance1);
        }

        [Fact]
        public void ShouldDisposeInstanceWhenDispose()
        {
            // Given
            var expectedInstance = new Mock<IDisposable>();
            var lifetime = new SingletonLifetime();
            var resolver = lifetime.Compile(() => expectedInstance.Object);
            resolver(Mock.Of<IContainer>());

            // When
            lifetime.Dispose();

            // Then
            expectedInstance.Verify(i => i.Dispose(), Times.Once);
        }
    }
}
