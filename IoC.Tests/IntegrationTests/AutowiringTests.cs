﻿namespace IoC.Tests.IntegrationTests
{
    using System;
    using Moq;
    using Shouldly;
    using Xunit;

    public class AutowiringTests
    {
        [Fact]
        public void ContainerShouldResolveWhenHasTag()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();
                Func<IMyService> func = () => expectedInstance;
                // When
                using (container.Bind<IMyService>().As(Lifetime.Transient).Tag("abc").Tag(10).To(ctx => func()))
                {
                    // Then
                    var actualInstance = container.Tag(10).Get<IMyService>();
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenHasState()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService>();
                Func<IMyService> func = () => expectedRef;

                // When
                using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => func()))
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(
                    ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>())))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>("abc");

                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService) actualInstance).Name.ShouldBe("abc");
                    ((MyService) actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenHasStateUsingFunc()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService>();
                Func<IMyService> func = () => expectedRef;

                // When
                using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => func()))
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(
                    ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>())))
                {
                    // Then
                    var actualInstance = container.Get<Func<string, IMyService>>()("abc");

                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService) actualInstance).Name.ShouldBe("abc");
                    ((MyService) actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenHasStateUsingFuncWithTag()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService>();
                Func<IMyService> func = () => expectedRef;

                // When
                using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => func()))
                using (container.Bind<IMyService>().As(Lifetime.Transient).Tag(33).To(
                    ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>())))
                {
                    // Then
                    var actualInstance = container.Tag(33).Get<Func<string, IMyService>>()("abc");

                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService) actualInstance).Name.ShouldBe("abc");
                    ((MyService) actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenHasStateInDependency()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService>();
                Func<IMyService> func = () => expectedRef;

                // When
                using (container.Bind<IMyService1>().To(ctx => func()))
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(
                    ctx => new MyService((string) ctx.Args[1], ctx.Container.Inject<IMyService1>())))
                using (container.Bind(typeof(IMyGenericService<TT>)).To(
                    ctx => new MyGenericService<TT>((TT) ctx.Args[0], ctx.Container.Inject<IMyService>())))
                {
                    // Then
                    var actualInstance = container.Get<IMyGenericService<int>>(99, "abc");

                    actualInstance.ShouldBeOfType<MyGenericService<int>>();
                    actualInstance.Value.ShouldBe(99);
                    actualInstance.Service.ShouldBeOfType<MyService>();
                    ((MyService) actualInstance.Service).Name.ShouldBe("abc");
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenHasStateAndRefTag()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService>();
                Func<IMyService> func = () => expectedRef;

                // When
                using (container.Bind<IMyService1>().Tag(33).As(Lifetime.Transient).To(ctx => func()))
                using (container.Bind<IMyService>().To(
                    ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>(33))))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>("abc");

                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService) actualInstance).Name.ShouldBe("abc");
                    ((MyService) actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }

        [Fact]
        public void ContainerShouldSupportWrapping()
        {
            // Given
            using (var container = Container.Create())
            using (container.Bind<IMyWrapper>().To<Wrappered>())
            {
                // When
                using (var childContainer = container.CreateChild())
                using (childContainer.Bind<IMyWrapper>().To<Wrapper>(ctx => new Wrapper(ctx.Container.Parent.Inject<IMyWrapper>())))
                {
                    // Then
                    var actualInstance = childContainer.Get<IMyWrapper>();
                    actualInstance.ShouldBeOfType<Wrapper>();
                    actualInstance.Wrapped.ShouldBeOfType<Wrappered>();
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenGenericAutowring()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Bind(typeof(IMyGenericService<,>)).As(Lifetime.Transient).To(typeof(MyGenericService<,>)))
                {
                    // Then
                    var actualInstance = container.Get<IMyGenericService<int, string>>();
                    actualInstance.ShouldBeOfType<MyGenericService<int, string>>();
                }
            }
        }
    }
}