namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Core;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class ScopeSingletonLifetimeTests
    {
        [Fact]
        public void ContainerShouldResolveWhenScopeLifetime()
        {
            // Given
            using var container = Container.Create();
            Func<IMyService1> func = Mock.Of<IMyService1>;
            // When
            using (container.Bind<IMyService>().To<MyService>(
                ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>()),
                ctx => ctx.Container.Assign(ctx.It.SomeRef2, ctx.Container.Inject<IMyService1>()),
                ctx => ctx.Container.Assign(ctx.It.SomeRef3, ctx.Container.Inject<IMyService1>())))
            using (container.Bind<IMyService1>().As(Lifetime.ScopeSingleton).To(ctx => func()))
            {
                // Default resolving scope
                var instance1 = (MyService) container.Resolve<IMyService>("abc");

                // Resolve scope
                MyService instance2;
                using (var scope = container.Resolve<IScope>())
                using (scope.Activate())
                {
                    instance2 = (MyService) container.Resolve<IMyService>("xyz");
                }

                // Then
                instance1.SomeRef.ShouldBe(instance1.SomeRef2);
                instance1.SomeRef.ShouldBe(instance1.SomeRef3);

                instance2.SomeRef.ShouldBe(instance2.SomeRef2);
                instance2.SomeRef.ShouldBe(instance2.SomeRef3);

                instance1.SomeRef.ShouldNotBe(instance2.SomeRef);
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public void ContainerShouldResolveWhenScopeLifetime_MT()
        {
            // Given
            using var container = Container.Create();
            Func<IMyService1> func = Mock.Of<IMyService1>;
            // When
            using (container.Bind<IMyService>().To<MyService>(
                ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>()),
                ctx => ctx.Container.Assign(ctx.It.SomeRef2, ctx.Container.Inject<IMyService1>()),
                ctx => ctx.Container.Assign(ctx.It.SomeRef3, ctx.Container.Inject<IMyService1>())))
            using (container.Bind<IMyService1>().As(Lifetime.ScopeSingleton).To(ctx => func()))
            {
                TestsExtensions.Parallelize(() =>
                {
                    // Default resolving scope
                    var instance1 = (MyService) container.Resolve<IMyService>("abc");

                    // Resolving scope 2
                    MyService instance2;
                    using (var scope = container.Resolve<IScope>())
                    using (scope.Activate())
                    {
                        instance2 = (MyService) container.Resolve<IMyService>("xyz");
                    }

                    // Then
                    instance1.SomeRef.ShouldBe(instance1.SomeRef2);
                    instance1.SomeRef.ShouldBe(instance1.SomeRef3);

                    instance2.SomeRef.ShouldBe(instance2.SomeRef2);
                    instance2.SomeRef.ShouldBe(instance2.SomeRef3);

                    instance1.SomeRef.ShouldNotBe(instance2.SomeRef);
                });
            }
        }

        [Fact]
        public void ContainerShouldDisposeWhenScopeDisposed()
        {
            // Given
            var mock = new Mock<IMyDisposableService>();
            using var container = Container.Create();
            using (container.Bind<IMyDisposableService>().As(Lifetime.ScopeSingleton).To(ctx => mock.Object))
            {
                var scope = (Scope)container.Resolve<IScope>();
                using (scope.Activate())
                {
                    var instance = container.Resolve<IMyDisposableService>();
                }

                // When
                scope.Dispose();

                // Then
                scope.ResourceCount.ShouldBe(0);

                mock.Verify(i => i.Dispose(), Times.Once);
#if NETCOREAPP5_0 || NETCOREAPP3_1
                mock.Verify(i => i.DisposeAsync(), Times.Once);
#endif
            }
        }

        [Fact]
        public void ContainerShouldDisposeWhenSubscriptionDisposed()
        {
            // Given
            var mock = new Mock<IMyDisposableService>();
            using var container = Container.Create();
            var subscriptionToken = container.Bind<IMyDisposableService>().As(Lifetime.ScopeSingleton).To(ctx => mock.Object);
            var scope = (Scope)container.Resolve<IScope>();
            using (scope.Activate())
            {
                var instance = container.Resolve<IMyDisposableService>();
            }

            // When
            subscriptionToken.Dispose();

            // Then
            scope.ResourceCount.ShouldBe(0);

            mock.Verify(i => i.Dispose(), Times.Once);
#if NETCOREAPP5_0 || NETCOREAPP3_1
            mock.Verify(i => i.DisposeAsync(), Times.Once);
#endif                
            scope.Dispose();
        }

        [Fact]
        public void ContainerShouldResolveDependencyFromRegistrationContainerWhenScopeSingletonLifetime()
        {
            // Given
            var myService1 = new Mock<IMyService1>();
            var myService12 = new Mock<IMyService1>();
            using var container = Container.Create();
            // When
            using (container.Bind<IMyService>().As(Lifetime.ScopeSingleton).To<MyService>())
            using (container.Bind<IMyService1>().To(ctx => myService1.Object))
            using (container.Bind<string>().To(ctx => "abc"))
            {
                // Then
                using var childContainer = container.Create();
                using (childContainer.Bind<IMyService1>().To(ctx => myService12.Object))
                using (childContainer.Bind<string>().To(ctx => "xyz"))
                {
                    using var scope = container.Resolve<IScope>();

                    // Then
                    var instance = childContainer.Resolve<IMyService>();
                    instance.ShouldNotBeNull();
                    instance.Name.ShouldBe("xyz");
                    instance.SomeRef.ShouldBe(myService12.Object);
                }
            }
        }
    }
}