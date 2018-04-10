namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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
                    var actualInstance = container.Resolve<IMyService>(10.AsTag());
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
                    var actualInstance = container.Resolve<IMyService>("abc");

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
                    var actualInstance = container.Resolve<Func<string, IMyService>>()("abc");

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
                    var actualInstance = container.Resolve<Func<string, IMyService>>(33.AsTag())("abc");

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
                    var actualInstance = container.Resolve<IMyGenericService<int>>(99, "abc");

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
                    var actualInstance = container.Resolve<IMyService>("abc");

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
                    var actualInstance = childContainer.Resolve<IMyWrapper>();
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
                    var actualInstance = container.Resolve<IMyGenericService<int, string>>();
                    actualInstance.ShouldBeOfType<MyGenericService<int, string>>();
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenGenericWithConstraintAutowring()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Bind(typeof(IMyGenericServiceWithConstraint<,>)).To(typeof(MyGenericServiceWithConstraint<,>)))
                {
                    // Then
                    var actualInstance = container.Resolve<IMyGenericServiceWithConstraint<IEnumerable<string>, int>>();
                    actualInstance.ShouldBeOfType<MyGenericServiceWithConstraint<int, IEnumerable<string>>>();
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenAutowiringWithInitMethod()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Bind<MySimpleClass>().To<MySimpleClass>())
                using (container.Bind(typeof(IMyGenericService<,>)).As(Lifetime.Transient).To(typeof(MyGenericService<,>), new AutowiringStrategy()))
                {
                    // Then
                    var actualInstance = container.Resolve<IMyGenericService<int, string>>();
                    actualInstance.ShouldBeOfType<MyGenericService<int, string>>();
                }
            }
        }

        private class AutowiringStrategy: IAutowiringStrategy
        {
            public bool TryResolveType(Type registeredType, Type resolvingType, out Type instanceType)
            {
                instanceType = default(Type);
                return false;
            }

            public bool TryResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
            {
                constructor = constructors.Single(i => i.Info.GetParameters().Length == 0);
                return true;
            }

            public bool TryResolveInitializers(IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
            {
                initializers =
                    from method in methods
                    where method.Info.Name == nameof(MyGenericService<object, object>.Init)
                    select method;

                return true;
            }
        }

        public interface IMyGenericServiceWithConstraint<TA1, TA2>
            where TA1: IEnumerable<string>
        {
        }

        public class MyGenericServiceWithConstraint<T1, T2>: IMyGenericServiceWithConstraint<T2, T1>
            where T2: IEnumerable<string>
        {
        }
    }
}