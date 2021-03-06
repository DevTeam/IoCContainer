﻿// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    // ReSharper disable once RedundantUsingDirective
    using System.Threading.Tasks;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    public class ResolveTests
    {
        [Fact]
        public void ContainerShouldThrowInvalidOperationExceptionWhenResolveFromNullContainer()
        {
            // Given
            using var container = Container.Create();

            // When
            using (container.Bind<IFactory<TT>>().To<MyFactory<TT>>(ctx => new MyFactory<TT>(ctx.Container.Parent.Parent.Inject<Func<TT>>())))
            {
                // Then
                Should.Throw<InvalidOperationException>(() => container.Resolve<IFactory<int>>());
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenRef()
        {
            // Given
            using var container = Container.Create();

            // When
            using (container.Bind<MySimpleClass>().To(ctx => new MySimpleClass()))
            {
                var instance = container.Resolve<object>(typeof(MySimpleClass));
                var instance1 = container.Resolve<MySimpleClass>();
                var instance2 = container.Resolve<MySimpleClass>();

                // Then
                instance.ShouldNotBe(instance1);
                instance.ShouldNotBe(instance2);
                instance1.ShouldNotBe(instance2);
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenRef_MT()
        {
            // Given
            using var container = Container.Create();

            // When
            using (container.Bind<MySimpleClass>().To(ctx => new MySimpleClass()))
            {
                TestsExtensions.Parallelize(() =>
                {
                    var instance = container.Resolve<object>(typeof(MySimpleClass));
                    var instance1 = container.Resolve<MySimpleClass>();
                    var instance2 = container.Resolve<MySimpleClass>();

                    // Then
                    instance.ShouldNotBe(instance1);
                    instance.ShouldNotBe(instance2);
                    instance1.ShouldNotBe(instance2);
                });
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenTransientLifetime()
        {
            // Given
            using var container = Container.Create();

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

        [Fact]
        public void ContainerShouldResolveWhenInherited()
        {
            // Given
            using var container = Container.Create(Features.CoreFeature.Set);

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
            }
        }

        [Fact]
        public void ContainerShouldResolveFunc()
        {
            // Given
            using var container = Container.Create();
            var expectedInstance = Mock.Of<IMyService>();

            // When
            using (container.Bind<IMyService>().Tag(99).To(ctx => expectedInstance))
            {
                // Then
                var actualInstance = container.Resolve<Func<IMyService>>(99.AsTag());
                actualInstance().ShouldBe(expectedInstance);
            }
        }

        [Fact]
        public void ContainerShouldResolveNewFuncWhenContainerChanged()
        {
            // Given
            using var container = Container.Create();
            var firstInstance = Mock.Of<IMyService>();
            var expectedInstance = Mock.Of<IMyService>();

            var token1 = container.Bind<IMyService>().To(ctx => firstInstance);
            var instance1 = container.Resolve<Func<IMyService>>();
            instance1().ShouldBe(firstInstance);

            // When
            token1.Dispose();
            container.Bind<IMyService>().To(ctx => expectedInstance);
            var actualInstance = container.Resolve<Func<IMyService>>();
            // Then

            actualInstance().ShouldBe(expectedInstance);
        }

        [Fact]
        public void ContainerShouldResolveFuncWitgArgs()
        {
            // Given
            using var container = Container.Create();

            // When
            container.Bind<string>().To(ctx => $"{Inc()}abc{ctx.Args[0]}");
            var func = container.Resolve<Func<int, string>>();
            var val = func(99);
            
            // Then
            val.ShouldBe("1abc99");
        }

        private int Inc()
        {
            return 1;
        }

        [Fact]
        public void ContainerShouldResolveNewFuncWhenParentContainerChanged()
        {
            // Given
            using(var parentContainer = Container.Create())
            using (var container = parentContainer.Create())
            {

                var firstInstance = Mock.Of<IMyService>();
                var expectedInstance = Mock.Of<IMyService>();

                var token1 = parentContainer.Bind<IMyService>().To(ctx => firstInstance);
                var instance1 = container.Resolve<Func<IMyService>>();
                instance1().ShouldBe(firstInstance);

                // When
                token1.Dispose();
                using (parentContainer.Bind<IMyService>().To(ctx => expectedInstance))
                {
                    var actualInstance = container.Resolve<Func<IMyService>>();

                    // Then
                    actualInstance().ShouldBe(expectedInstance);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveLazy()
        {
            // Given
            using var container = Container.Create();
            var expectedInstance = Mock.Of<IMyService>();

            // When
            using (container.Bind<IMyService>().Tag(99).To(ctx => expectedInstance))
            {
                // Then
                var actualInstance = container.Resolve<Lazy<IMyService>>(99.AsTag());
                actualInstance.Value.ShouldBe(expectedInstance);
            }
        }

        [Fact]
        public void ContainerShouldResolve()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWithArgs()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWithTag()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWithTagAndArgs()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWithType()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWithTypeAndArgs()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWithTypeAndTag()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWithTypeAndTagAndArgs()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWhenSeveralContracts()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWhenGeneric()
        {
            // Given
            using var container = Container.Create();
            
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().As(Lifetime.Transient).To(ctx => new MyGenericService<TT1, TT2>(), ctx => ctx.It.Do(), ctx => ctx.It.Do()))
            {
                // Then
                var actualInstance = container.Resolve<IMyGenericService<int, string>>();
                actualInstance.ShouldBeOfType<MyGenericService<int, string>>();
                ((MyGenericService<int, string>)actualInstance).Counter.ShouldBe(2);
            }
        }

        [Fact]
        public void ContainerShouldFullAutoWiringResolveWhenGenericAndHasStatements()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().As(Lifetime.Transient).To<MyGenericService<TT1, TT2>>(ctx => ctx.It.Do(), ctx => ctx.It.Do()))
            {
                // Then
                var actualInstance = container.Resolve<IMyGenericService<int, string>>();
                actualInstance.ShouldBeOfType<MyGenericService<int, string>>();
                ((MyGenericService<int, string>)actualInstance).Counter.ShouldBe(2);
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenFunc()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldResolveWhenArray()
        {
            // Given
            using var container = Container.Create();
            
            // When
            using (container.Bind<IMyService>().As(Lifetime.Singleton).To(ctx => Mock.Of<IMyService>()))
            using (container.Bind<IMyService>().Tag(2).To(ctx => Mock.Of<IMyService>()))
            {
                // Then
                var instance = container.Resolve<IMyService[]>();
                var instance2 = container.Resolve<IMyService[]>();
                instance.Length.ShouldBe(2);
                instance2.Length.ShouldBe(2);
                instance.Intersect(instance2).Count().ShouldBe(1);
            }
        }

#if !NET40
        [Fact]
        public async Task ContainerShouldResolveTask()
        {
            // Given
            using var container = Container.Create();
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
#endif

        [Fact]
        public void ContainerShouldResolveWhenFuncWithArg()
        {
            // Given
            using var container = Container.Create();
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

        [Fact]
        public void ContainerShouldThrowDetailedExceptionWhenResolvingWasFailed()
        {
            // Given
            using var container = Container.Create();
            using (container.Bind<IMyService>().To(ctx => new MyService("abc", ctx.Container.Inject<IMyService1>())))
            {
                // When
                try
                {
                    container.Resolve<IMyService>();
                }
                catch (Exception ex)
                {
                    // Then
                    ex.InnerException.ShouldNotBeNull();
                    ex.InnerException.Message.ShouldContain("Cannot find a dependency for");
                    ex.InnerException.Message.ShouldContain("IMyService1");
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveGenericWhenFunc()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                var instance = container.Resolve<Func<IMyGenericService<string, int>>>()();

                // Then
                instance.ShouldBeOfType<MyGenericService<string, int>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveGenericWhenTuple()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                var instance = container.Resolve<Tuple<IMyGenericService<string, int>, IMyGenericService<string, int>>>();

                // Then
                instance.Item1.ShouldBeOfType<MyGenericService<string, int>>();
                instance.Item2.ShouldBeOfType<MyGenericService<string, int>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveGenericWhenLazy()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                var instance = container.Resolve<Lazy<IMyGenericService<string, int>>>();

                // Then
                instance.Value.ShouldBeOfType<MyGenericService<string, int>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveGenericWhenEnumerable()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                var instance = container.Resolve<IEnumerable<IMyGenericService<string, int>>>();

                // Then
                instance.Single().ShouldBeOfType<MyGenericService<string, int>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveGenericWhenList()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                var instance = container.Resolve<IList<IMyGenericService<string, int>>>();

                // Then
                instance[0].ShouldBeOfType<MyGenericService<string, int>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenFactory()
        {
            // Given
            using var container = Container.Create();

            // When
            using (container
                .Bind<IMyClass>().To<MyClass>()
                .Bind<IFactory<TT>>().To<MyFactory<TT>>())
            {
                var instance = container.Resolve<IMyClass>();

                // Then
                instance.New().ShouldBeOfType<MyClass>();
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenHasKeyDependency()
        {
            // Given
            using var container = Container.Create();

            // When
            using (container.Bind<IMyClass>().Tag(1).To<MyClassWithKey>())
            {
                var instance = container.Resolve<IMyClass>(1.AsTag());
                
                // Then
                instance.ShouldBeOfType<MyClassWithKey>();
                ((MyClassWithKey)instance).Key.ShouldBe(new Key(typeof(IMyClass), 1));
                ((MyClassWithKey)instance).Tag.Value.ShouldBe(1);
            }
        }

        public interface IFactory<out T>
        {
            T Create();
        }

        public class MyFactory<T>: IFactory<T>
        {
            private readonly Func<T> _func;

            public MyFactory(Func<T> func) => _func = func;

            public T Create() => _func();
        }

        public interface IMyClass
        {
            IMyClass New();
        }

        public class MyClass: IMyClass
        {
            private readonly IFactory<IMyClass> _factory;

            public MyClass(IFactory<IMyClass> factory) => _factory = factory;

            public IMyClass New() => _factory.Create();
        }

        public class MyClassWithKey: IMyClass
        {
            public Key Key { get; }
            public Tag Tag { get; }

            public MyClassWithKey(Key key, Tag tag)
            {
                Key = key;
                Tag = tag;
            }

            public IMyClass New()
            {
                throw new NotImplementedException();
            }
        }

#if !NET40
        [Fact]
        public void ContainerShouldResolveGenericWhenComplex()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                var instance = container.Resolve<Task<Func<IEnumerable<Lazy<Tuple<Func<IMyGenericService<string, int>>, IMyGenericService<string, int>>>>>>>();

                // Then
                instance.Result().Single().Value.Item1().ShouldBeOfType<MyGenericService<string, int>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveGenericWhenComplex_MT()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                TestsExtensions.Parallelize(() =>
                {
                    var instance = container.Resolve<Task<Func<IEnumerable<Lazy<Tuple<Func<IMyGenericService<string, int>>, IMyGenericService<string, int>>>>>>>();

                    // Then
                    instance.Result().Single().Value.Item1().ShouldBeOfType<MyGenericService<string, int>>();
                });
            }
        }

        [Fact]
        public void ContainerShouldResolveGenericWhenTask()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                var instance = container.Resolve<Task<IMyGenericService<string, int>>>().Result;

                // Then
                instance.ShouldBeOfType<MyGenericService<string, int>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveGenericWhenReactive()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                var source = container.Resolve<IObservable<IMyGenericService<string, int>>>();

                // Then
                using(source.Subscribe(new Observer())) {}
            }
        }

        [Fact]
        public void ContainerShouldThrowInvalidOperationExceptionWhenGenericTypeMarkersWasNotReplaced()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>, IMyGenericService1<TT1>>().To<MyGenericService<TT1, TT2>>())
            {
                // Then
                Should.Throw<InvalidOperationException>(() => container.Resolve<Func<IMyGenericService1<string>>>()());
            }
        }

        private class Observer: IObserver<IMyGenericService<string, int>>
        {
            public void OnNext(IMyGenericService<string, int> value)
            {
                value.ShouldBeOfType<MyGenericService<string, int>>();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnCompleted()
            {
            }
        }
#endif

#if NET5_0_OR_GREATER || NETCOREAPP2_1 || NETCOREAPP3_1
        [Fact]
        public void ContainerShouldResolveGenericWhenValueTuple()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                var instance = container.Resolve<(IMyGenericService<string, int> a, IMyGenericService<string, int> b)>();

                // Then
                instance.a.ShouldBeOfType<MyGenericService<string, int>>();
                instance.b.ShouldBeOfType<MyGenericService<string, int>>();
            }
        }

        [Fact]
        public void ContainerShouldResolveGenericWhenValueTask()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                var instance = container.Resolve<ValueTask<IMyGenericService<string, int>>>().Result;

                // Then
                instance.ShouldBeOfType<MyGenericService<string, int>>();
            }
        }
#endif
    }
}