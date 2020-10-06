namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    [SuppressMessage("ReSharper", "RedundantTypeArgumentsOfMethod")]
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public class AutowiringTests
    {
        [Fact]
        public void AutowiringWhenHasDefaultRefValue()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassRefDef>().To()
                .Bind<string>().To(ctx => "xyz")
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassRefDef>();

                instance.Val.ShouldBe("xyz");
            }
        }

        [Fact]
        public void AutowiringWhenHasDefaultRefValueAndHasNoDependency()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassRefDef>().To()
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassRefDef>();

                instance.Val.ShouldBe("abc");
            }
        }

        [Fact]
        public void AutowiringWhenHasDefaultValue()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassValDef>().To()
                .Bind<int>().To(ctx => 33)
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassValDef>();

                instance.Val.ShouldBe(33);
            }
        }

        [Fact]
        public void AutowiringWhenHasDefaultValueAndHasNoDependency()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassValDef>().To()
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassValDef>();

                instance.Val.ShouldBe(99);
            }
        }

        [Fact]
        public void AutowiringWhenNullableValue()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassNullableValDef>().To()
                .Bind<int>().To(ctx => 33)
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassNullableValDef>();

                instance.Val.ShouldBe(33);
            }
        }

        [Fact]
        public void AutowiringWhenNullableValueAndHasNoDependency()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassNullableValDef>().To()
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassNullableValDef>();

                instance.Val.HasValue.ShouldBeFalse();
            }
        }

        public class MyClassRefDef
        {
            public string Val { get; }

            public MyClassRefDef(string val = "abc") => Val = val;
        }

        public class MyClassValDef
        {
            public int Val { get; }

            public MyClassValDef(int val = 99) => Val = val;
        }

        public class MyClassNullableValDef
        {
            public int? Val { get; }

            public MyClassNullableValDef(int? val) => Val = val;
        }

        [Fact]
        public void ContainerShouldResolveWhenHasTag()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWhenHasState()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWhenHasStateUsingFunc()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWhenHasStateUsingFuncWithTag()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWhenHasStateInDependency()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWhenHasStateAndRefTag()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldSupportExplicitWrappingUsingParentContainer()
        {
            // Given
            using var container = Container.Create();
            using (container.Bind<IMyWrapper>().To<Wrappering>())
            {
                // When
                using var childContainer = container.Create();
                using (childContainer.Bind<IMyWrapper>().To<Wrapper>(ctx => new Wrapper(ctx.Container.Parent.Inject<IMyWrapper>(), ctx.Container.Parent.Inject<Func<string, IMyWrapper>>(), ctx.Container.Parent.Inject<Tuple<IMyWrapper>>())))
                {
                    // Then
                    var actualInstance = childContainer.Resolve<IMyWrapper>();
                    actualInstance.ShouldBeOfType<Wrapper>();
                    actualInstance.Wrapped.ShouldBeOfType<Wrappering>();
                }
            }
        }

        [Fact]
        public void ContainerShouldSupportImplicitWrappingUsingCurrentContainer()
        {
            // Given
            using var container = Container.Create();
            using (container.Bind<IMyWrapper>().To<Wrappering>())
            {
                // When
                using var childContainer = container.Create();
                using (childContainer.Bind<IMyWrapper>().To<Wrapper>(ctx => new Wrapper(ctx.Container.Inject<IMyWrapper>(), ctx.Container.Inject<Func<string, IMyWrapper>>(), ctx.Container.Inject<Tuple<IMyWrapper>>())))
                {
                    // Then
                    var actualInstance = childContainer.Resolve<IMyWrapper>();
                    actualInstance.ShouldBeOfType<Wrapper>();
                    actualInstance.Wrapped.ShouldBeOfType<Wrappering>();
                }
            }
        }

        [Fact]
        public void ContainerShouldSupportImplicitWrappingUsingCurrentContainerWhenAutowiring()
        {
            // Given
            using var container = Container.Create();
            using (container.Bind<IMyWrapper>().To<Wrappering>())
            {
                // When
                using var childContainer = container.Create();
                using (childContainer.Bind<IMyWrapper>().To<Wrapper>())
                {
                    // Then
                    var actualInstance = childContainer.Resolve<IMyWrapper>();
                    actualInstance.ShouldBeOfType<Wrapper>();
                    actualInstance.Wrapped.ShouldBeOfType<Wrappering>();
                }
            }
        }

        [Fact]
        public void ContainerShouldSupportImplicitWrappingUsingCurrentContainerWhenAutowiringWhenChainOfContainers()
        {
            // Given
            using var container = Container.Create();
            using (container.Bind<IMyWrapper>().To<Wrappering>())
            {
                // When
                using var childContainer3 = container.Create();
                using var childContainer2 = childContainer3.Create();
                using var childContainer1 = childContainer2.Create();
                using var childContainer = childContainer1.Create();
                using (childContainer.Bind<IMyWrapper>().To<Wrapper>())
                {
                    // Then
                    var actualInstance = childContainer.Resolve<IMyWrapper>();
                    actualInstance.ShouldBeOfType<Wrapper>();
                    actualInstance.Wrapped.ShouldBeOfType<Wrappering>();
                }
            }
        }


        [Fact]
        public void ContainerShouldSupportImplicitWrappingUsingCurrentContainerWhenAutowiringWhenSingleton()
        {
            // Given
            using var container = Container.Create();
            using (container.Bind<IMyWrapper>().As(Lifetime.Singleton).To<Wrappering>())
            {
                // When
                using var childContainer = container.Create();
                using (childContainer.Bind<IMyWrapper>().To<Wrapper>())
                {
                    // Then
                    var actualInstance1 = childContainer.Resolve<IMyWrapper>();
                    var actualInstance2 = childContainer.Resolve<IMyWrapper>();
                    actualInstance1.Wrapped.ShouldBe(actualInstance2.Wrapped);
                }
            }
        }

        [Fact]
        public void ContainerShouldSupportWrapping_MT()
        {
            // Given
            using var container = Container.Create();
            using (container.Bind<IMyWrapper>().To<Wrappering>())
            {
                TestsExtensions.Parallelize(() =>
                {
                    // When
                    using var childContainer = container.Create();
                    using (childContainer.Bind<IMyWrapper>().To<Wrapper>(ctx => new Wrapper(ctx.Container.Parent.Inject<IMyWrapper>(), ctx.Container.Parent.Inject<Func<string, IMyWrapper>>(), ctx.Container.Parent.Inject<Tuple<IMyWrapper>>())))
                    {
                        // Then
                        var actualInstance = childContainer.Resolve<IMyWrapper>();
                        actualInstance.ShouldBeOfType<Wrapper>();
                        actualInstance.Wrapped.ShouldBeOfType<Wrappering>();
                    }
                });
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenGenericAutowiring()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind(typeof(IMyGenericService<,>)).As(Lifetime.Transient).To(typeof(MyGenericService<,>)))
            {
                // Then
                var actualInstance = container.Resolve<IMyGenericService<int, string>>();
                actualInstance.ShouldBeOfType<MyGenericService<int, string>>();
            }
        }

        [Fact]
        public void ContainerShouldThrowInvalidOperationExceptionWhenCannotResolveGenericTypeArgument()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind(typeof(IMyGenericService1<>), typeof(IMyGenericService<,>)).To(typeof(MyGenericService<,>)))
            {
                // Then
                Should.Throw<InvalidOperationException>(() => container.Resolve<IMyGenericService1<int>>());
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenGenericAutowiringWithRefTypeConstraint()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind(typeof(RefTypeHolder<>)).To(typeof(RefTypeHolder<>)))
            {
                // Then
                var actualInstance = container.Resolve<RefTypeHolder<Content>>();
                actualInstance.ShouldBeOfType<RefTypeHolder<Content>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenGenericAutowiringWithDefaultCtorConstraint()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind(typeof(DefaultCtorHolder<>)).To(typeof(DefaultCtorHolder<>)))
            {
                // Then
                var actualInstance = container.Resolve<DefaultCtorHolder<Content>>();
                actualInstance.ShouldBeOfType<DefaultCtorHolder<Content>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenGenericAutowiringWithCovariantConstraint()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind(typeof(ICovariantHolder<>)).To(typeof(CovariantHolder<>)))
            {
                // Then
                var actualInstance = container.Resolve<ICovariantHolder<Content>>();
                actualInstance.ShouldBeOfType<CovariantHolder<Content>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenCovariantConstraint()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind(typeof(ICovariantHolder<>)).To(typeof(CovariantHolder<Content>)))
            {
                // Then
                var actualInstance = container.Resolve<ICovariantHolder<object>>();
                actualInstance.ShouldBeOfType<CovariantHolder<Content>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenInvariantConstraint()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind(typeof(IInvariantHolder<>)).To(typeof(InvariantHolder<object>)))
            {
                // Then
                var actualInstance = container.Resolve<IInvariantHolder<Content>>();
                actualInstance.ShouldBeOfType<InvariantHolder<object>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenGenericAutowiring_MT()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind(typeof(IMyGenericService<,>)).As(Lifetime.Transient).To(typeof(MyGenericService<,>)))
            {
                TestsExtensions.Parallelize(() =>
                {
                    // Then
                    var actualInstance = container.Resolve<IMyGenericService<int, string>>();
                    actualInstance.ShouldBeOfType<MyGenericService<int, string>>();
                });
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenGenericWithConstraintAutowiring()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind(typeof(IMyGenericServiceWithConstraint<,>)).To(typeof(MyGenericServiceWithConstraint<,>)))
            {
                // Then
                var actualInstance = container.Resolve<IMyGenericServiceWithConstraint<IEnumerable<string>, int>>();
                actualInstance.ShouldBeOfType<MyGenericServiceWithConstraint<int, IEnumerable<string>>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenAutowiringWithInitMethod()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<MySimpleClass>().To<MySimpleClass>())
            using (container.Bind(typeof(IMyGenericService<,>)).As(Lifetime.Transient).Autowiring(new AutowiringStrategy()).To(typeof(MyGenericService<,>)))
            {
                // Then
                var actualInstance = container.Resolve<IMyGenericService<int, string>>();
                actualInstance.ShouldBeOfType<MyGenericService<int, string>>();
            }
        }

        private class AutowiringStrategy: IAutowiringStrategy
        {
            public bool TryResolveType(IContainer container, Type registeredType, Type resolvingType, out Type instanceType)
            {
                instanceType = default(Type);
                return false;
            }

            public bool TryResolveConstructor(IContainer container, IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
            {
                constructor = constructors.Single(i => i.Info.GetParameters().Length == 0);
                return true;
            }

            public bool TryResolveInitializers(IContainer container, IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
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

        public class RefTypeHolder<T> where T : class { }

        public class DefaultCtorHolder<T> where T: new() { }

        public interface ICovariantHolder<out T> { }

        public class CovariantHolder<T> : ICovariantHolder<T> { }

        public interface IInvariantHolder<in T> { }

        public class InvariantHolder<T> : IInvariantHolder<T> { }

        public class Content { }
    }
}