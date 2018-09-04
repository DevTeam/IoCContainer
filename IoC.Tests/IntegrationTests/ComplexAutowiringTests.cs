namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    [SuppressMessage("ReSharper", "RedundantAssignment")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    public class ComplexAutowiringTests
    {
        [Fact]
        public void ContainerShouldResolveWhenMethodCallForInjectedInstance()
        {
            // Given
            using (var container = Container.Create())
            using (container.Bind<MyClass3>().To())
            {
                // When
                using (container.Bind<MyClass>().As(Lifetime.Transient).To(ctx => new MyClass(new MyClass2(ctx.Container.Inject<MyClass3>().ToString()))))
                {
                    // Then
                    var actualInstance = container.Resolve<MyClass>();
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenArrayForInjectedInstance()
        {
            // Given
            using (var container = Container.Create())
            using (container.Bind<MyClass3>().To())
            {
                // When
                using (container.Bind<MyClass>().As(Lifetime.Transient).To(ctx => new MyClass(new MyClass2(new []{ ctx.Container.Inject<MyClass3>(), ctx.Container.Inject<MyClass3>() }))))
                {
                    // Then
                    var actualInstance = container.Resolve<MyClass>();
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenArrayOfFuncForInjectedInstance()
        {
            // Given
            using (var container = Container.Create())
            using (container.Bind<MyClass3>().To())
            {
                // When
                using (container.Bind<MyClass>().As(Lifetime.Transient).To(ctx => new MyClass(new MyClass2(new Func<MyClass3>[] { () => ctx.Container.Inject<MyClass3>(), () => ctx.Container.Inject<MyClass3>() }))))
                {
                    // Then
                    var actualInstance = container.Resolve<MyClass>();
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenArrayOfTaskForInjectedInstance()
        {
            // Given
            using (var container = Container.Create())
            using (container.Bind<MyClass3>().To())
            {
                // When
                using (container.Bind<MyClass>().As(Lifetime.Transient).To(ctx => new MyClass(new MyClass2(new[] { ctx.Container.Inject<Task<MyClass3>>(), ctx.Container.Inject<Task<MyClass3>>() }))))
                {
                    // Then
                    var actualInstance = container.Resolve<MyClass>();
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveFromParent()
        {
            // Given
            var expectedRef = Mock.Of<IMyService1>();
            Func<IMyService1> func = () => expectedRef;

            using (var container = Container.Create())
            using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => func()))
            {
                var childRef = Mock.Of<IMyService>();
                Func<IMyService1> childFunc = () => childRef;

                // When
                using (var childContainer = container.CreateChild())
                using (childContainer.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => childFunc()))
                using (childContainer.Bind<IMyService>().As(Lifetime.Transient).To(
                    ctx => new MyService((string)ctx.Args[0], ctx.Container.Parent.Inject<IMyService1>())))
                {
                    // Then
                    var actualInstance = childContainer.Resolve<IMyService>("abc");
                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService)actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveFromParentOfParent()
        {
            // Given
            var expectedRef = Mock.Of<IMyService1>();
            Func<IMyService1> func = () => expectedRef;

            using (var container = Container.Create())
            using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => func()))
            {
                var childRef = Mock.Of<IMyService>();
                Func<IMyService1> childFunc = () => childRef;

                // When
                using (var childContainer1 = container.CreateChild())
                using (var childContainer2 = childContainer1.CreateChild())
                using (childContainer2.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => childFunc()))
                using (childContainer2.Bind<IMyService>().As(Lifetime.Transient).To(
                    ctx => new MyService((string)ctx.Args[0], ctx.Container.Parent.Parent.Inject<IMyService1>())))
                {
                    // Then
                    var actualInstance = childContainer2.Resolve<IMyService>("abc");
                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService)actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveFromParentOfParent_MT()
        {
            // Given
            var expectedRef = Mock.Of<IMyService1>();
            Func<IMyService1> func = () => expectedRef;

            using (var container = Container.Create())
            using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => func()))
            {
                var childRef = Mock.Of<IMyService>();
                Func<IMyService1> childFunc = () => childRef;

                TestsExtensions.Parallelize(() =>
                {
                    // When
                    using (var childContainer1 = container.CreateChild())
                    using (var childContainer2 = childContainer1.CreateChild())
                    using (childContainer2.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => childFunc()))
                    using (childContainer2.Bind<IMyService>().As(Lifetime.Transient).To(
                        ctx => new MyService((string) ctx.Args[0], ctx.Container.Parent.Parent.Inject<IMyService1>())))
                    {
                        // Then
                        var actualInstance = childContainer2.Resolve<IMyService>("abc");
                        actualInstance.ShouldBeOfType<MyService>();
                        ((MyService) actualInstance).SomeRef.ShouldBe(expectedRef);
                    }
                });
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenGenericInitMethodCall()
        {
            // Given
            using (var container = Container.Create())
            using (container.Bind<MyClass3>().To())
            {
                // When
                using (container.Bind<MyClass>().As(Lifetime.Transient).To(
                    ctx => new MyClass(new MyClass2(ctx.Container.Inject<MyClass3>().ToString())),
                    ctx => ctx.It.Initialize("aaa")))
                {
                    // Then
                    var actualInstance = container.Resolve<MyClass>();
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenGenericInitMethodCallWithRef()
        {
            // Given
            var val = "abc";
            using (var container = Container.Create())
            using (container.Bind<MyClass3>().To())
            {
                // When
                using (container.Bind<MyClass>().As(Lifetime.Transient).To(
                    ctx => new MyClass(new MyClass2(ctx.Container.Inject<MyClass3>().ToString())),
                    ctx => ctx.It.Initialize2(ref val)))
                {
                    // Then
                    var actualInstance = container.Resolve<MyClass>();
                    val.ShouldBe(default(string));
                }
            }
        }

        public class MyClass
        {
            public MyClass(MyClass2 myClass2)
            {
            }

            public T Initialize<T>(T value)
            {
                return value;
            }

            public void Initialize2<T>(ref T value)
            {
                value = default(T);
            }
        }

        public class MyClass2
        {
            public MyClass2(string str)
            {
            }

            public MyClass2(MyClass3[] arr)
            {
            }

            public MyClass2(Func<MyClass3>[] arr)
            {
                foreach (var func in arr)
                {
                    func();
                }
            }

            public MyClass2(Task<MyClass3>[] arr)
            {
            }
        }

        public class MyClass3
        {
        }
    }
}
