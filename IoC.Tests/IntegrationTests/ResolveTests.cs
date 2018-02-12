namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                using (container.Bind<IMyService>().Lifetime(Lifetime.Transient).To(ctx => func()))
                {
                    // Then
                    var instance1 = container.Get<IMyService>();
                    var instance2 = container.Get<IMyService>();
                    instance1.ShouldNotBe(instance2);
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
                using (container.Bind<IMyService>().Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.FuncGet<IMyService>();
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
                using (container.Bind<IMyService>().Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>();
                    actualInstance.ShouldBe(expectedInstance);
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
                using (container.Bind<IMyService>().Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>();
                    var actualInstance1 = container.Get<IMyService>();
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
                using (container.Bind(typeof(IMyGenericService<,>)).Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Get<IMyGenericService<int, string>>();
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveEnumerable()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                Func<IMyService1> func1 = Mock.Of<IMyService1>;
                // When
                using (container.Bind<IMyService, IMyService1>().Lifetime(Lifetime.Singletone).Tag(1).To(ctx => func()))
                using (container.Bind<IMyService1>().Tag("abc").To(ctx => func1()))
                using (container.Bind<IMyService, IMyService1>().Lifetime(Lifetime.Transient).Tag("xyz").To(ctx => func()))
                {
                    // Then
                    var actualInstances = container.Get<IEnumerable<IMyService1>>().ToList();
                    actualInstances.Count.ShouldBe(3);
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
                    var getter = container.Get<Func<IMyService>>();
                    var actualInstance = getter();
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveTask()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();
                Func<IMyService> func = () => expectedInstance;

                // When
                using (container.Bind<IMyService>().Lifetime(Lifetime.Transient).To(ctx => func()))
                {
                    // Then
                    var task = container.Get<Task<IMyService>>();
                    task.Start();
                    var actualInstance = task.Result;
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenFuncWithArg()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService1>().Lifetime(Lifetime.Transient).To(ctx => expectedRef))
                using (container.Bind<IMyService>().Lifetime(Lifetime.Transient).To(ctx => new MyService((string)ctx.Args[0], ctx.Container.Inject<IMyService1>())))
                {
                    // Then
                    var func = container.Get<Func<string, IMyService>>();
                    var actualInstance = func("abc");
                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService)actualInstance).Name.ShouldBe("abc");
                    ((MyService)actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }
    }
}