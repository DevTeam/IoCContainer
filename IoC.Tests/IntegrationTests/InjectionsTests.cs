namespace IoC.Tests.IntegrationTests
{
    using System;
    using Moq;
    using Shouldly;
    using Xunit;

    public class InjectionsTests
    {
        [Fact]
        public void ContainerShouldResolveWhenHasInitializerMethod()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService1>();
                Func<IMyService1> func = () => expectedRef;

                // When
                using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => func()))
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(
                    ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>()),
                    ctx => ctx.It.Init(ctx.Container.Inject<IMyService1>(), ctx.Container.Inject<IMyService1>(), (string) ctx.Args[1])))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>("abc", "xyz");

                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService) actualInstance).Name.ShouldBe("xyz");
                    ((MyService) actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenHasInitializerSetter()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService1>();
                Func<IMyService1> func = () => expectedRef;

                // When
                using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => func()))
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(
                    ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>()),
                    ctx => ctx.Container.Inject(ctx.It.Name, (string) ctx.Args[1])))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>("abc", "xyz");

                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService) actualInstance).Name.ShouldBe("xyz");
                    ((MyService) actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }

        [Fact]
        public void ContainerShouldGetResolver()
        {
            // Given
            using (var container = Container.CreatePure())
            {
                var expectedRef = Mock.Of<IMyService1>();
                Func<IMyService1> func = () => expectedRef;

                // When
                using (container.Bind<IMyService1>().To(ctx => func()))
                {
                    // Then
                    var resolver = container.Get<Resolver<IMyService1>>();
                    resolver.ShouldBeOfType<Resolver<IMyService1>>();
                    var actualInstance = resolver(container);
                    actualInstance.ShouldBe(expectedRef);
                }
            }
        }

        [Fact]
        public void ContainerShouldInjectResolver()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService1>();
                Func<IMyService1> func = () => expectedRef;

                // When
                using (container.Bind<IMyService1>().To(ctx => func()))
                using (container.Bind<Func<IMyService1>>().To(ctx => (() => ctx.Container.Inject<Resolver<IMyService1>>()(ctx.Container))))
                {
                    // Then
                    var funcInstance = container.Get<Func<IMyService1>>();
                    funcInstance.ShouldBeOfType<Func<IMyService1>>();
                    var actualInstance = funcInstance();
                    actualInstance.ShouldBe(expectedRef);
                }
            }
        }
    }
}