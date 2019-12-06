namespace IoC.Tests
{
    using System;
    using Core;
    using Lifetimes;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ScopeSingletonLifetimeTests
    {
        [Fact]
        public void ShouldCreateSingleInstance()
        {
            // Given
            var lifetime = new ScopeSingletonLifetime();
            var resolver = lifetime.Compile(() => new object());
            
            // When
            object instance11;
            object instance12;
            object instance21;
            object instance22;
            using (var scope1 = new Scope(1, new LockObject()))
            using (var scope2 = new Scope(2, new LockObject()))
            {
                using (scope1.Activate())
                {
                    instance11 = resolver(Mock.Of<IContainer>());
                    using (scope2.Activate())
                    {
                        instance21 = resolver(Mock.Of<IContainer>());
                    }

                    instance12 = resolver(Mock.Of<IContainer>());
                }

                using (scope2.Activate())
                {
                    instance22 = resolver(Mock.Of<IContainer>());
                }
            }

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
            var lifetime = new ScopeSingletonLifetime();
            var resolver = lifetime.Compile(() => expectedInstance.Object);
            resolver(Mock.Of<IContainer>());

            // When
            lifetime.Dispose();

            // Then
            expectedInstance.Verify(i => i.Dispose(), Times.Once);
        }
    }
}
