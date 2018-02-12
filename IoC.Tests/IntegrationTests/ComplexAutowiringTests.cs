namespace IoC.Tests.IntegrationTests
{
    using System;
    using Moq;
    using Shouldly;
    using Xunit;

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
                using (container.Bind<MyClass>().Lifetime(Lifetime.Transient).To(ctx => new MyClass(new MyClass2(ctx.Container.Inject<MyClass3>().ToString()))))
                {
                    // Then
                    var actualInstance = container.Get<MyClass>();
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
            using (container.Bind<IMyService1>().Lifetime(Lifetime.Transient).To(ctx => func()))
            {
                var childRef = Mock.Of<IMyService>();
                Func<IMyService1> childFunc = () => childRef;

                // When
                using (var childContainer = container.CreateChild())
                using (childContainer.Bind<IMyService1>().Lifetime(Lifetime.Transient).To(ctx => childFunc()))
                using (childContainer.Bind<IMyService>().Lifetime(Lifetime.Transient).To(
                    ctx => new MyService((string)ctx.Args[0], ctx.Container.Parent.Inject<IMyService1>())))
                {
                    // Then
                    var actualInstance = childContainer.Get<IMyService>("abc");
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
            using (container.Bind<IMyService1>().Lifetime(Lifetime.Transient).To(ctx => func()))
            {
                var childRef = Mock.Of<IMyService>();
                Func<IMyService1> childFunc = () => childRef;

                // When
                using (var childContainer1 = container.CreateChild())
                using (var childContainer2 = childContainer1.CreateChild())
                using (childContainer2.Bind<IMyService1>().Lifetime(Lifetime.Transient).To(ctx => childFunc()))
                using (childContainer2.Bind<IMyService>().Lifetime(Lifetime.Transient).To(
                    ctx => new MyService((string)ctx.Args[0], ctx.Container.Parent.Parent.Inject<IMyService1>())))
                {
                    // Then
                    var actualInstance = childContainer2.Get<IMyService>("abc");
                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService)actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }

        public class MyClass
        {
            public MyClass(MyClass2 myClass2)
            {
            }
        }

        public class MyClass2
        {
            public MyClass2(string myClass3Name)
            {
            }
        }

        public class MyClass3
        {
        }
    }
}
