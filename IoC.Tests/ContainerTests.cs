﻿namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public class ContainerTests
    {
        [Fact]
        public void ContainerShouldResolve()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Map<IMyService>().Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>();
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveChildContainer()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Map<IMyService>().Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
                using (var childContainer = container.Get<IContainer>())
                {
                    // Then
                    var actualInstance = childContainer.Get<IMyService>();
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ShouldRegisterCustomChildContainer()
        {
            // Given
            var childContainer = new Mock<IContainer>();
            using (var container = Container.Create("my"))
            {
                // When
                IContainer actualChildContainer;
                using (container.Map<IContainer>().To(ctx => childContainer.Object))
                {
                    actualChildContainer = container.Get<IContainer>();
                }

                // Then
                actualChildContainer.ShouldBe(childContainer.Object);
            }
        }

        [Fact]
        public void ParentContainerShouldDisposeChildContainerWhenDispose()
        {
            // Given
            var expectedInstance = new Mock<IDisposable>();
            using (var container = Container.Create("root"))
            {
                var childContainer = container.CreateChild("child");
                IDisposable actualInstance;
                using (childContainer.Map<IDisposable>().Lifetime(Lifetime.Container).To(ctx => expectedInstance.Object))
                {
                    // When
                    actualInstance = childContainer.Get<IDisposable>();
                }

                // Then
                actualInstance.ShouldBe(expectedInstance.Object);
            }

            expectedInstance.Verify(i => i.Dispose(), Times.Once);
        }

        [Fact]
        public void ContainerShouldResolveWhenSeveralContracts()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Map<IMyService>().Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
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
        public void ContainerShouldResolveWhenTransientLifetime()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Map<IMyService>().Lifetime(Lifetime.Transient).To(ctx => Mock.Of<IMyService>()))
                {
                    // Then
                    var instance1 = container.Get<IMyService>();
                    var instance2 = container.Get<IMyService>();
                    instance1.ShouldNotBe(instance2);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenSingletonLifetime()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Map<IMyService>().Lifetime(Lifetime.Singletone).To(ctx => Mock.Of<IMyService>()))
                {
                    // Then
                    var instance1 = container.Get<IMyService>();
                    var instance2 = container.Get<IMyService>();
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var instance3 = childContainer.Get<IMyService>();
                        instance1.ShouldBe(instance2);
                        instance1.ShouldBe(instance3);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenSingletonLifetimeAndSeveralContracts()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Map<IMyService, IMyService1>().Lifetime(Lifetime.Singletone).To(ctx => Mock.Of<IMyService>()))
                {
                    // Then
                    var instance1 = container.Get<IMyService>();
                    var instance2 = container.Get<IMyService1>();
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var instance3 = childContainer.Get<IMyService>();
                        var instance4 = childContainer.Get<IMyService1>();
                        instance1.ShouldBe(instance2);
                        instance1.ShouldBe(instance3);
                        instance1.ShouldBe(instance4);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldDiposeWhenSingletonLifetime()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Map<IMyService>().Lifetime(Lifetime.Singletone).To(ctx => Mock.Of<IMyService>()))
                {
                    // Then
                    var instance1 = container.Get<IMyService>();
                    var instance2 = container.Get<IMyService>();
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var instance3 = childContainer.Get<IMyService>();
                        instance1.ShouldBe(instance2);
                        instance1.ShouldBe(instance3);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenContainerLifetime()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Map<IMyService>().Lifetime(Lifetime.Container).To(ctx => Mock.Of<IMyService>()))
                {
                    // Then
                    var instance1 = container.Get<IMyService>();
                    var instance2 = container.Get<IMyService>();
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var instance3 = childContainer.Get<IMyService>();
                        instance1.ShouldBe(instance2);
                        instance1.ShouldNotBe(instance3);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenContainerLifetimeAndSeveralContracts()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Map<IMyService, IMyService1>().Lifetime(Lifetime.Container).To(ctx => Mock.Of<IMyService>()))
                {
                    // Then
                    var instance1 = container.Get<IMyService>();
                    var instance2 = container.Get<IMyService1>();
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var instance3 = childContainer.Get<IMyService>();
                        var instance4 = childContainer.Get<IMyService1>();
                        instance1.ShouldBe(instance2);
                        instance1.ShouldNotBe(instance3);
                        instance3.ShouldBe(instance4);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveFromChild()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Map<IMyService>().Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var actualInstance = childContainer.Get<IMyService>();
                        actualInstance.ShouldBe(expectedInstance);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenHasTag()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedInstance = Mock.Of<IMyService>();

                // When
                using (container.Map<IMyService>().Lifetime(Lifetime.Transient).Tag("abc").Tag(10).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Tag(10).Get<IMyService>();
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

                // When
                using (container.Map<IMyService1>().Lifetime(Lifetime.Transient).To(ctx => expectedRef))
                using (container.Map<IMyService>().Lifetime(Lifetime.Transient).To(typeof(MyService), Dependency.Arg<string>(0, 0, "name")))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>("abc");

                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService)actualInstance).Name.ShouldBe("abc");
                    ((MyService)actualInstance).SomeRef.ShouldBe(expectedRef);
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

                // When
                using (container.Map<IMyService1>().Tag(33).Lifetime(Lifetime.Transient).To(ctx => expectedRef))
                using (container.Map<IMyService>().Lifetime(Lifetime.Transient).To(typeof(MyService), Dependency.Arg<string>(0, 0, "name"), Dependency.Ref<IMyService1>(33, 1, "someRef")))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>("abc");

                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService)actualInstance).Name.ShouldBe("abc");
                    ((MyService)actualInstance).SomeRef.ShouldBe(expectedRef);
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
                using (container.Map(typeof(IMyGenericService<,>)).Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var actualInstance = container.Get<IMyGenericService<int, string>>();
                    actualInstance.ShouldBe(expectedInstance);
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
                using (container.Map(typeof(IMyGenericService<,>)).Lifetime(Lifetime.Transient).To(typeof(MyGenericService<,>)))
                {
                    // Then
                    var actualInstance = container.Get<IMyGenericService<int, string>>();
                    actualInstance.ShouldBeOfType<MyGenericService<int, string>>();
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveEnumerable()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Map<IMyService, IMyService1>().Lifetime(Lifetime.Transient).Tag(1).To(ctx => Mock.Of<IMyService>()))
                using (container.Map<IMyService1>().Lifetime(Lifetime.Transient).Tag("abc").To(ctx => Mock.Of<IMyService1>()))
                using (container.Map<IMyService, IMyService1>().Lifetime(Lifetime.Transient).Tag("xyz").To(ctx => Mock.Of<IMyService>()))
                {
                    // Then
                    var actualInstances = container.Get<IEnumerable<IMyService1>>().ToList();
                    actualInstances.Count.ShouldNotBe(3);
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

                // When
                using (container.Map<IMyService>().Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var func = container.Get<Func<IMyService>>();
                    var actualInstance = func();
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
                using (container.Map<IMyService1>().Lifetime(Lifetime.Transient).To(ctx => expectedRef))
                using (container.Map<IMyService>().Lifetime(Lifetime.Transient).To(typeof(MyService), Dependency.Arg<string>(0, 0, "name")))
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

        [Fact]
        public void ContainerShouldResolveWhenSingletonLifetimeWithGenerics()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Map(typeof(IMyGenericService1<>)).Lifetime(Lifetime.Singletone).To(ctx =>
                {
                    if(ctx.ContractType.GenericTypeArguments[0] == typeof(string))
                    {
                        return Mock.Of<IMyGenericService1<string>>();
                    }

                    if (ctx.ContractType.GenericTypeArguments[0] == typeof(int))
                    {
                        return Mock.Of<IMyGenericService1<int>>();
                    }

                    return null;
                }))
                {
                    // Then
                    var instance1 = container.Get<IMyGenericService1<int>>();
                    var instance2 = container.Get<IMyGenericService1<string>>();
                    var instance3 = container.Get<IMyGenericService1<int>>();
                    var instance4 = container.Get<IMyGenericService1<string>>();
                    instance1.ShouldBe(instance3);
                    instance2.ShouldBe(instance4);
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

                // When
                using (container.Map<IMyService>().Lifetime(Lifetime.Transient).To(ctx => expectedInstance))
                {
                    // Then
                    var task = container.Get<Task<IMyService>>();
                    task.Start();
                    var actualInstance = task.Result;
                    actualInstance.ShouldBe(expectedInstance);
                }
            }
        }

        public interface IMyService: IMyService1
        {
        }

        public interface IMyService1
        {
        }

        public class MyService: IMyService
        {
            public MyService(string name, IMyService1 someRef)
            {
                Name = name;
                SomeRef = someRef;
            }

            public string Name { get; }
            public IMyService1 SomeRef { get; }
        }

        public class MyGenericService<T1, T2> : IMyGenericService<T1, T2>
        {
        }

        public interface IMyGenericService<T1, T2> : IMyGenericService1<T1>
        {
        }

        public interface IMyGenericService1<T1>
        {
        }
    }
}
