namespace IoC.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Core;
    using Core.Emitters;
    using Dependencies;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class StaticMethodEmitterTests
    {
        [Fact]
        public void ShouldEmitStaticCallForValueType()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<int>(Key.Create<int>(), Mock.Of<IContainer>(), Has.StaticMethod<int, MyClass>("CreateInt")))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(10);
            }
        }

        [Fact]
        public void ShouldEmitStaticCallForRefType()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<object>(Key.Create<string>(), Mock.Of<IContainer>(), Has.StaticMethod<string, MyClass>("CreateString")))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe("abc");
            }
        }

        [Fact]
        public void ShouldEmitStaticCallWithDependencies()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<string>(Key.Create<string>(), Mock.Of<IContainer>(), Has.StaticMethod<string, MyClass>("CreateStringWithDep", Has.Value(10).For("val"))))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe("abc: 10");
            }
        }

        private static ResolverGenerator CreateInstance()
        {
            return new ResolverGenerator(
                new DependencyEmitter(
                    new ValueEmitter(),
                    Mock.Of<IDependencyEmitter<Argument>>(),
                    Mock.Of<IDependencyEmitter<FactoryMethod>>(),
                    new StaticMethodEmitter(),
                    new AutowiringEmitter()),
                Mock.Of<ILifetimeEmitter>());
        }

        public class MyClass
        {
            public static int CreateInt()
            {
                return 10;
            }

            public static string CreateString()
            {
                return "abc";
            }

            public static string CreateStringWithDep(int val)
            {
                return "abc: " + val;
            }
        }
    }
}
