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
            using var container = Container.Create("Core", Features.Set.Core);
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