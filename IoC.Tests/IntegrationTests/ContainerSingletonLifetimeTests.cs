namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    public class ContainerSingletonLifetimeTests
    {
        [Fact]
        public void ContainerShouldResolveWhenContainerLifetime()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService>().As(Lifetime.ContainerSingleton).To(ctx => func()))
                {
                    // Then
                    var instance1 = container.Resolve<IMyService>();
                    var instance2 = container.Resolve<IMyService>();

                    // Then
                    instance1.ShouldBe(instance2);
                    IMyService instance3;
                    using (var childContainer1 = container.CreateChild())
                    {
                        instance3 = childContainer1.Resolve<IMyService>();
                        var instance4 = childContainer1.Resolve<IMyService>();
                        instance3.ShouldBe(instance4);
                        instance1.ShouldNotBe(instance3);
                    }

                    using (var childContainer2 = container.CreateChild())
                    {
                        // Then
                        var instance5 = childContainer2.Resolve<IMyService>();
                        var instance6 = childContainer2.Resolve<IMyService>();
                        instance5.ShouldBe(instance6);
                        instance1.ShouldNotBe(instance5);
                        instance3.ShouldNotBe(instance5);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenContainerLifetime_MT()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService>().As(Lifetime.ContainerSingleton).To(ctx => func()))
                {
                    TestsExtensions.Parallelize(() =>
                    {
                        // Then
                        var instance1 = container.Resolve<IMyService>();
                        var instance2 = container.Resolve<IMyService>();

                        // Then
                        instance1.ShouldBe(instance2);
                        IMyService instance3;
                        using (var childContainer1 = container.CreateChild())
                        {
                            instance3 = childContainer1.Resolve<IMyService>();
                            var instance4 = childContainer1.Resolve<IMyService>();
                            instance3.ShouldBe(instance4);
                            instance1.ShouldNotBe(instance3);
                        }

                        using (var childContainer2 = container.CreateChild())
                        {
                            // Then
                            var instance5 = childContainer2.Resolve<IMyService>();
                            var instance6 = childContainer2.Resolve<IMyService>();
                            instance5.ShouldBe(instance6);
                            instance1.ShouldNotBe(instance5);
                            instance3.ShouldNotBe(instance5);
                        }
                    });
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenContainerLifetimeAndSeveralContracts()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService, IMyService1>().As(Lifetime.ContainerSingleton).To(ctx => func()))
                {
                    // Then
                    var instance1 = container.Resolve<IMyService>();
                    var instance2 = container.Resolve<IMyService1>();
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var instance3 = childContainer.Resolve<IMyService>();
                        var instance4 = childContainer.Resolve<IMyService1>();
                        instance1.ShouldBe(instance2);
                        instance1.ShouldNotBe(instance3);
                        instance3.ShouldBe(instance4);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldDiposeWhenDependencyTokenDisposed()
        {
            // Given
            using (var container = Container.Create())
            {
                var mock = new Mock<IMyDisposableService>();
                // When
                using (container.Bind<IMyDisposableService>().As(Lifetime.ContainerSingleton).To(ctx => mock.Object))
                {
                    var instance = container.Resolve<IMyDisposableService>();
                }

                // Then
                mock.Verify(i => i.Dispose(), Times.Once);
            }
        }

        [Fact]
        public void ContainerShouldDiposeWhenChildContainerDisposed()
        {
            // Given
            var mock = new Mock<IMyDisposableService>();
            using (var container = Container.Create())
            using (container.Bind<IMyDisposableService>().As(Lifetime.ContainerSingleton).To(ctx => mock.Object))
            {
                // When
                using (var childContainer = container.CreateChild())
                {
                    var instance = childContainer.Resolve<IMyDisposableService>();
                }

                // Then
                mock.Verify(i => i.Dispose(), Times.Once);
            }
        }
    }
}