namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    public class SingletonLifetimeTests
    {
        [Fact]
        public void ContainerShouldResolveWhenSingletonLifetime()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService>().As(Lifetime.Singleton).To(ctx => func()))
                {
                    // Then
                    var instance1 = container.Resolve<IMyService>();
                    var instance2 = container.Resolve<IMyService>();
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var instance3 = childContainer.Resolve<IMyService>();
                        instance1.ShouldNotBeNull();
                        instance1.ShouldBe(instance2);
                        instance1.ShouldBe(instance3);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenSingletonLifetime_MT()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService>().As(Lifetime.Singleton).To(ctx => func()))
                {
                    TestsExtensions.Parallelize(() =>
                    {
                        // Then
                        var instance1 = container.Resolve<IMyService>();
                        var instance2 = container.Resolve<IMyService>();
                        using (var childContainer = container.CreateChild())
                        {
                            // Then
                            var instance3 = childContainer.Resolve<IMyService>();
                            instance1.ShouldNotBeNull();
                            instance1.ShouldBe(instance2);
                            instance1.ShouldBe(instance3);
                        }
                    });
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenSingletonLifetimeAndSeveralContracts()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService, IMyService1>().As(Lifetime.Singleton).To(ctx => func()))
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
                        instance1.ShouldBe(instance3);
                        instance1.ShouldBe(instance4);
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
                using (container.Bind<IMyDisposableService>().As(Lifetime.Singleton).To(ctx => mock.Object))
                {
                    var instance1 = container.Resolve<IMyDisposableService>();
                }

                // Then
                mock.Verify(i => i.Dispose(), Times.Once);
            }
        }

        [Fact]
        public void ContainerShouldDiposeWhenContainerDisposed()
        {
            // Given
            var mock = new Mock<IMyDisposableService>();
            using (var container = Container.Create())
            {
                container.Bind<IMyDisposableService>().As(Lifetime.Singleton).To(ctx => mock.Object);

                // When
                var instance1 = container.Resolve<IMyDisposableService>();
            }

            // Then
            mock.Verify(i => i.Dispose(), Times.Once);
        }

        [Fact]
        public void ContainerShouldResolveWhenSingletonLifetimeWithGenerics()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Bind<IMyGenericService<TT1, TT2>>().As(Lifetime.Singleton).To(ctx => new MyGenericService<TT1, TT2>()))
                {
                    // Then
                    var instance1 = container.Resolve<IMyGenericService<int, double>>();
                    var instance2 = container.Resolve<IMyGenericService<string, object>>();
                    var instance3 = container.Resolve<IMyGenericService<int, double>>();
                    var instance4 = container.Resolve<IMyGenericService<string, object>>();
                    instance1.ShouldBe(instance3);
                    instance2.ShouldBe(instance4);
                }
            }
        }
    }
}