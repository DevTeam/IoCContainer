namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ResolveEnumerablesTests
    {
        [Fact]
        public void ContainerShouldResolveEnumerable()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                Func<IMyService1> func1 = Mock.Of<IMyService1>;
                // When
                using (container.Bind<IMyService, IMyService1>().As(Lifetime.Singleton).Tag(1).To(ctx => func()))
                using (container.Bind<IMyService1>().Tag("abc").Tag(99).To(ctx => func1()))
                using (container.Bind<IMyService, IMyService1>().As(Lifetime.Transient).Tag("xyz").To(ctx => func()))
                {
                    // Then
                    var actualInstances = container.Resolve<IEnumerable<IMyService1>>().ToList();
                    actualInstances.Count.ShouldBe(3);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveGenericEnumerable()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService>().As(Lifetime.Singleton).To(ctx => func()))
                using (container.Bind<IMyGenericService<TT>>().To(ctx => new MyGenericService<TT>(default(TT), ctx.Container.Inject<IMyService>())))
                using (container.Bind<IMyGenericService<TT>>().Tag(1).To(ctx => new MyGenericService<TT>(default(TT), ctx.Container.Inject<IMyService>())))
                using (container.Bind<IMyGenericService<TT>>().Tag("abc").To(ctx => new MyGenericService<TT>(default(TT), ctx.Container.Inject<IMyService>())))
                {
                    // Then
                    var actualInstances = container.Resolve<IEnumerable<IMyGenericService<string>>>().ToList();
                    actualInstances.Count.ShouldBe(3);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveEnumerableAfterContainerChanged()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                Func<IMyService1> func1 = Mock.Of<IMyService1>;
                using (container.Bind<IMyService, IMyService1>().As(Lifetime.Singleton).Tag(1).To(ctx => func()))
                using (container.Bind<IMyService1>().Tag("abc").To(ctx => func1()))
                using (container.Bind<IMyService, IMyService1>().As(Lifetime.Transient).Tag("xyz").To(ctx => func()))
                {
                    var actualInstances = container.Resolve<IEnumerable<IMyService1>>().ToList();
                    actualInstances.Count.ShouldBe(3);
                    // When
                    using (container.Bind<IMyService1>().Tag(123).To(ctx => func1()))
                    {
                        // Then
                        var actualInstances2 = container.Resolve<IEnumerable<IMyService1>>().ToList();
                        actualInstances2.Count.ShouldBe(4);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveArray()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                Func<IMyService1> func1 = Mock.Of<IMyService1>;
                // When
                using (container.Bind<IMyService, IMyService1>().As(Lifetime.Singleton).Tag(1).To(ctx => func()))
                using (container.Bind<IMyService1>().Tag("abc").Tag(99).To(ctx => func1()))
                using (container.Bind<IMyService, IMyService1>().As(Lifetime.Transient).Tag("xyz").To(ctx => func()))
                {
                    // Then
                    var actualInstances = container.Resolve<IMyService1[]>();
                    actualInstances.Length.ShouldBe(3);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveArrayWhenGeneric()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                Func<IMyService1> func1 = Mock.Of<IMyService1>;
                // When
                using (container.Bind<IBox<TT>>().To<Box<TT>>())
                using (container.Bind<IMyService, IMyService1>().As(Lifetime.Singleton).Tag(1).To(ctx => func()))
                using (container.Bind<IMyService1>().Tag("abc").Tag(99).To(ctx => func1()))
                using (container.Bind<IMyService, IMyService1>().As(Lifetime.Transient).Tag("xyz").To(ctx => func()))
                {
                    // Then
                    var box = container.Resolve<IBox<IMyService1[]>>();
                    box.Content.Length.ShouldBe(3);
                }
            }
        }

        interface IBox<out T> { T Content { get; } }

        class Box<T> : IBox<T>
        {
            public Box(T content) => Content = content;

            public T Content { get; }

            public override string ToString() => Content.ToString();
        }
    }
}