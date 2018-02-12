namespace IoC.Tests.IntegrationTests
{
    using System;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ChildContainerTests
    {
        [Fact]
        public void ShouldRegisterCustomChildContainer()
        {
            // Given
            var childContainer = new Mock<IContainer>();
            Func<IContainer> containerGetter = () => childContainer.Object;
            using (var container = Container.Create("my"))
            {
                // When
                IContainer actualChildContainer;
                using (container.Bind<IContainer>().To(ctx => containerGetter()))
                {
                    actualChildContainer = container.Get<IContainer>();
                }

                // Then
                actualChildContainer.ShouldBe(childContainer.Object);
            }
        }

        [Fact]
        public void ParentContainerShouldDisposeChildContainerWhenDispose()
        {
            // Given
            var expectedInstance = new Mock<IDisposable>();
            using (var container = Container.Create("root"))
            {
                var childContainer = container.CreateChild("child");
                IDisposable actualInstance;
                using (childContainer.Bind<IDisposable>().Lifetime(Lifetime.Container).To(ctx => expectedInstance.Object))
                {
                    // When
                    actualInstance = childContainer.Get<IDisposable>();
                }

                // Then
                actualInstance.ShouldBe(expectedInstance.Object);
            }

            expectedInstance.Verify(i => i.Dispose(), Times.Once);
        }

        [Fact]
        public void ContainerShouldResolveFromChild()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();
                Func<IMyService> func = () => expectedInstance;
                // When
                using (container.Bind<IMyService>().Lifetime(Lifetime.Transient).To(ctx => func()))
                {
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var actualInstance = childContainer.Get<IMyService>();
                        actualInstance.ShouldBe(expectedInstance);
                    }
                }
            }
        }
    }
}