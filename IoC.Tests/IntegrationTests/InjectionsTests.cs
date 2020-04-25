namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "RedundantTypeArgumentsOfMethod")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class InjectionsTests
    {
        [Fact]
        public void InjectWhenRef()
        {
            // Given
            using var container = Container
                .Create() 
            // When
                .Bind<MyClassRef>().To(ctx => new MyClassRef(ctx.Container.Inject<string>()))
                .Bind<string>().To(ctx => "abc")
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassRef>();

                instance.Val.ShouldBe("abc");
            }
        }

        [Fact]
        public void TryInjectWhenRef()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassRef>().To(ctx => new MyClassRef(ctx.Container.TryInject<string>()))
                .Bind<string>().To(ctx => "abc")
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassRef>();

                instance.Val.ShouldBe("abc");
            }
        }

        [Fact]
        public void TryInjectWhenRefAndHasNoDependency()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassRef>().To(ctx => new MyClassRef(ctx.Container.TryInject<string>()))
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassRef>();

                instance.Val.ShouldBe(null);
            }
        }

        [Fact]
        public void TryInjectWhenRefAndHasNoDependencyAndHasDefaultValue()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassRefDef>().To(ctx => new MyClassRefDef(ctx.Container.TryInject<string>()))
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassRefDef>();

                instance.Val.ShouldBe(null);
            }
        }

        [Fact]
        public void InjectWhenVal()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassVal>().To(ctx => new MyClassVal(ctx.Container.Inject<int>()))
                .Bind<int>().To(ctx => 99)
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassVal>();

                instance.Val.ShouldBe(99);
            }
        }

        [Fact]
        public void TryInjectWhenVal()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassVal>().To(ctx => new MyClassVal(ctx.Container.TryInject<int>()))
                .Bind<int>().To(ctx => 99)
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassVal>();

                instance.Val.ShouldBe(99);
            }
        }

        [Fact]
        public void TryInjectWhenValAndNoDependency()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassVal>().To(ctx => new MyClassVal(ctx.Container.TryInject<int>()))
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassVal>();

                instance.Val.ShouldBe(0);
            }
        }

        [Fact]
        public void TryInjectValueWhensNullableVal()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassNullableVal>().To(ctx => new MyClassNullableVal(ctx.Container.TryInjectValue<int>()))
                .Bind<int>().To(ctx => 99)
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassNullableVal>();

                instance.Val.ShouldBe(99);
            }
        }

        [Fact]
        public void TryInjectValueWhensNullableValAndNoDependency()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassNullableVal>().To(ctx => new MyClassNullableVal(ctx.Container.TryInjectValue<int>()))
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassNullableVal>();

                instance.Val.HasValue.ShouldBeFalse();
            }
        }

        [Fact]
        public void TryInjectWhensNullableVal()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassNullableVal>().To(ctx => new MyClassNullableVal(ctx.Container.TryInject<int>()))
                .Bind<int>().To(ctx => 99)
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassNullableVal>();

                instance.Val.ShouldBe(99);
            }
        }

        [Fact]
        public void TryInjectWhensNullableValAndNoDependency()
        {
            // Given
            using var container = Container
                .Create()
                // When
                .Bind<MyClassNullableVal>().To(ctx => new MyClassNullableVal(ctx.Container.TryInject<int>()))
                .Container;

            using (container)
            {
                // Then
                var instance = container.Resolve<MyClassNullableVal>();

                instance.Val.ShouldBe(0);
            }
        }

        public class MyClassRef
        {
            public string Val { get; }

            public MyClassRef(string val) => Val = val;
        }

        public class MyClassRefDef
        {
            public string Val { get; }

            public MyClassRefDef(string val = "abc") => Val = val;
        }

        public class MyClassVal
        {
            public int Val { get; }

            public MyClassVal(int val) => Val = val;
        }

        public class MyClassNullableVal
        {
            public int? Val { get; }

            public MyClassNullableVal(int? val) => Val = val;
        }

        [Fact]
        public void ContainerShouldResolveWhenHasInitializerMethod()
        {
            // Given
            using var container = Container.Create();
            var expectedRef = Mock.Of<IMyService1>();
            Func<IMyService1> func = () => expectedRef;

            // When
            using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => func()))
            using (container.Bind<IMyService>().As(Lifetime.Transient).To(
                ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>()),
                ctx => ctx.It.Init(ctx.Container.Inject<IMyService1>(), ctx.Container.Inject<IMyService1>(), (string) ctx.Args[1])))
            {
                // Then
                var actualInstance = container.Resolve<IMyService>("abc", "xyz");

                actualInstance.ShouldBeOfType<MyService>();
                ((MyService) actualInstance).Name.ShouldBe("xyz");
                ((MyService) actualInstance).SomeRef.ShouldBe(expectedRef);
            }
        }

        [Fact]
        public void ContainerShouldInjectWhenHasInitializerMethod()
        {
            // Given
            using var container = Container.Create();
            var expectedRef = Mock.Of<IMyService1>();
            Func<IMyService1> func = () => expectedRef;

            // When
            using (container.Bind<MyHolder>().To<MyHolder>())
            using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => func()))
            using (container.Bind<IMyService>().As(Lifetime.Transient).To(
                ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>()),
                ctx => ctx.It.Init(ctx.Container.Inject<IMyService1>(), ctx.Container.Inject<IMyService1>(), (string) ctx.Args[1])))
            {
                // Then
                var actualInstance = container.Resolve<MyHolder>("abc", "xyz");

                actualInstance.MyService.ShouldBeOfType<MyService>();
                ((MyService) actualInstance.MyService).Name.ShouldBe("xyz");
                ((MyService) actualInstance.MyService).SomeRef.ShouldBe(expectedRef);
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenHasInitializerSetter()
        {
            // Given
            using var container = Container.Create();
            var expectedRef = Mock.Of<IMyService1>();
            Func<IMyService1> func = () => expectedRef;

            // When
            using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => func()))
            using (container.Bind<IMyService>().As(Lifetime.Transient).To(
                ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>()),
                ctx => ctx.Container.Inject(ctx.It.Name, (string) ctx.Args[1])))
            {
                // Then
                var actualInstance = container.Resolve<IMyService>("abc", "xyz");

                actualInstance.ShouldBeOfType<MyService>();
                ((MyService) actualInstance).Name.ShouldBe("xyz");
                ((MyService) actualInstance).SomeRef.ShouldBe(expectedRef);
            }
        }

        [Fact]
        public void ContainerShouldGetResolver()
        {
            // Given
            using var container = Container.Create(Features.CoreFeature.Set);
            var expectedRef = Mock.Of<IMyService1>();
            Func<IMyService1> func = () => expectedRef;

            // When
            using (container.Bind<IMyService1>().To(ctx => func()))
            {
                // Then
                var resolver = container.Resolve<Resolver<IMyService1>>();
                resolver.ShouldBeOfType<Resolver<IMyService1>>();
                var actualInstance = resolver(container);
                actualInstance.ShouldBe(expectedRef);
            }
        }

        [Fact]
        public void ContainerShouldInjectResolver()
        {
            // Given
            using var container = Container.Create();
            var expectedRef = Mock.Of<IMyService1>();
            Func<IMyService1> func = () => expectedRef;

            // When
            using (container.Bind<IMyService1>().To(ctx => func()))
            using (container.Bind<Func<IMyService1>>().To(ctx => (() => ctx.Container.Inject<Resolver<IMyService1>>()(ctx.Container))))
            {
                // Then
                var funcInstance = container.Resolve<Func<IMyService1>>();
                funcInstance.ShouldBeOfType<Func<IMyService1>>();
                var actualInstance = funcInstance();
                actualInstance.ShouldBe(expectedRef);
            }
        }

        public class MyHolder
        {
            public MyHolder(IMyService myService)
            {
                MyService = myService;
            }

            public IMyService MyService { get; }
        }
    }
}