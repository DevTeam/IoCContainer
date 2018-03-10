namespace IoC.Tests.IntegrationTests
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using System.Threading.Tasks;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ResolveTests
    {
        [Fact]
        public void ContainerShouldResolveWhenTransientLifetime()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(ctx => func()))
                {
                    // Then
                    var instance1 = container.Resolve<IMyService>();
                    var instance2 = container.Resolve<IMyService>();
                    instance1.ShouldNotBe(instance2);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenInherited()
        {
            // Given
            using (var container = Container.CreateBasic())
            {
                // When
                using (container.Bind<MyService, IMyService>().To(ctx => new MyService("abc", null)))
                {
                    // Then
                    var instance1 = container.GetResolver<MyService>(typeof(MyService))(container);
                    var instance2 = container.GetResolver<object>(typeof(MyService))(container);
                    var instance3 = container.GetResolver<IMyService>(typeof(IMyService))(container);

                    instance3.ShouldBeOfType<MyService>();
                    instance1.ShouldBeOfType<MyService>();
                    instance2.ShouldBeOfType<MyService>();
                    container.Validate().IsValid.ShouldBeTrue();
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveFunc()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Resolve<Func<IMyService>>();
                    actualInstance().ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolve()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Resolve<IMyService>();
                    var actualInstance2 = ((IContainer)container).Resolve<IMyService>();
                    actualInstance.ShouldBe(expectedInstance);
                    actualInstance2.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWithArgs()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Resolve<IMyService>("abc");
                    var actualInstance2 = ((IContainer)container).Resolve<IMyService>("abc");
                    actualInstance.ShouldBe(expectedInstance);
                    actualInstance2.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWithTag()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().Tag(10).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Resolve<IMyService>(10.AsTag());
                    var actualInstance2 = ((IContainer)container).Resolve<IMyService>(10.AsTag());
                    actualInstance.ShouldBe(expectedInstance);
                    actualInstance2.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWithTagAndArgs()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().Tag(10).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Resolve<IMyService>(10.AsTag(), "abc");
                    var actualInstance2 = ((IContainer)container).Resolve<IMyService>(10.AsTag(), "abc");
                    actualInstance.ShouldBe(expectedInstance);
                    actualInstance2.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWithType()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Resolve<object>(typeof(IMyService));
                    var actualInstance2 = ((IContainer)container).Resolve<object>(typeof(IMyService));
                    actualInstance.ShouldBe(expectedInstance);
                    actualInstance2.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWithTypeAndArgs()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Resolve<object>(typeof(IMyService), "abc");
                    var actualInstance2 = ((IContainer)container).Resolve<object>(typeof(IMyService), "abc");
                    actualInstance.ShouldBe(expectedInstance);
                    actualInstance2.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWithTypeAndTag()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().Tag(9).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Resolve<object>(typeof(IMyService), 9.AsTag());
                    var actualInstance2 = ((IContainer)container).Resolve<object>(typeof(IMyService), 9.AsTag());
                    actualInstance.ShouldBe(expectedInstance);
                    actualInstance2.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWithTypeAndTagAndArgs()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().Tag(9).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Resolve<object>(typeof(IMyService), 9.AsTag(), "abc");
                    var actualInstance2 = ((IContainer)container).Resolve<object>(typeof(IMyService), 9.AsTag(), "abc");
                    actualInstance.ShouldBe(expectedInstance);
                    actualInstance2.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenSeveralContracts()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Resolve<IMyService>();
                    var actualInstance1 = container.Resolve<IMyService>();
                    actualInstance.ShouldBe(expectedInstance);
                    actualInstance1.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenGeneric()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyGenericService<int, string>>();

                // When
                using (container.Bind(typeof(IMyGenericService<,>)).As(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Resolve<IMyGenericService<int, string>>();
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenFunc()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();
                Func<IMyService> func = () => expectedInstance;

                // When
                using (container.Bind<IMyService>().To(ctx => func()))
                {
                    // Then
                    var getter = container.Resolve<Func<IMyService>>();
                    var actualInstance = getter();
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

#if !NET40
        [Fact]
        public async Task ContainerShouldResolveTask()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();
                Func<IMyService> func = () => expectedInstance;

                // When
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(ctx => func()))
                {
                    // Then
                    var actualInstance = await container.Resolve<Task<IMyService>>();
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }
#endif

        [Fact]
        public void ContainerShouldResolveWhenFuncWithArg()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => expectedRef))
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(ctx => new MyService((string)ctx.Args[0], ctx.Container.Inject<IMyService1>())))
                {
                    // Then
                    var func = container.Resolve<Func<string, IMyService>>();
                    var actualInstance = func("abc");
                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService)actualInstance).Name.ShouldBe("abc");
                    ((MyService)actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }
    }
}