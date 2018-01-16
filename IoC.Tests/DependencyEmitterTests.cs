namespace IoC.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Core;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class DependencyEmitterTests
    {
        [Fact]
        public void ShouldEmitArgumentForValueTypeIndex0()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<int>(Key.Create<int>(), Mock.Of<IContainer>(), Has.Argument<int>(0)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>(), 10);

                // Then
                instance.ShouldBe(10);
            }
        }

        [Fact]
        public void ShouldEmitContextDependency()
        {
            // Given
            var resolverGenerator = CreateInstance();
            //var generationContext = CreateContext<Task<MyClass>>();
            //var resolvingContext = CreateContext<Task<MyClass>>();
            var resolvingKey = Key.Create<MyTask<MyClass>>();
            var resolvingContainer = Mock.Of<IContainer>();
            using (var resolver = resolverGenerator.Generate<Task<MyClass>>(Key.Create<MyTask<MyClass>>(), Mock.Of<IContainer>(), Has.Autowiring(typeof(MyTask<>), Has.Constructor(Has.Dependency<Context>().At(0)))))
            {
                // When
                var instance = resolver.Resolve(resolvingContainer);

                // Then
                instance.ShouldBeOfType<MyTask<MyClass>>();
                ((MyTask<MyClass>)instance).Context.Key.ShouldBe(resolvingKey);
                ((MyTask<MyClass>)instance).Context.Container.ShouldBe(resolvingContainer);
            }
        }

        private static IResolverGenerator CreateInstance()
        {
            return ResolverGenerator.Shared;
        }

        public class MyClass
        {
        }

        // ReSharper disable once UnusedTypeParameter
        public class Task<T>
        {
        }

        public class MyTask<T>: Task<T>
        {
            public Context Context { get; }

            public MyTask(Context context)
            {
                Context = context;
            }
        }
    }
}
