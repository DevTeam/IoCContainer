namespace IoC.Tests.IntegrationTests
{
    using System;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ScopeLifetimeTests
    {
        [Fact]
        public void ContainerShouldResolveWhenScopeLifetime()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService1> func = Mock.Of<IMyService1>;
                // When
                using (container.Bind<IMyService>().To<MyService>(
                    ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>()),
                    ctx => ctx.Container.Inject(ctx.It.SomeRef2, ctx.Container.Inject<IMyService1>()),
                    ctx => ctx.Container.Inject(ctx.It.SomeRef3, ctx.Container.Inject<IMyService1>())))
                using (container.Bind<IMyService1>().As(Lifetime.ScopeSingleton).To(ctx => func()))
                {
                    // Default resolving scope
                    var instance1 = (MyService) container.Resolve<IMyService>("abc");

                    // Resolving scope 2
                    MyService instance2;
                    using (new Scope(2))
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
        }
    }
}