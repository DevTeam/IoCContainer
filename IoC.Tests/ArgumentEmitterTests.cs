namespace IoC.Tests
{
    using Core;
    using Core.Emitters;
    using Dependencies;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ArgumentEmitterTests
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
        public void ShouldEmitArgumentForValueTypeIndex1()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<int>(Key.Create<int>(), Mock.Of<IContainer>(), Has.Argument<int>(1)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>(), 10, 20);

                // Then
                instance.ShouldBe(20);
            }
        }

        [Fact]
        public void ShouldEmitArgumentForRefTypeIndex0()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<string>(Key.Create<string>(), Mock.Of<IContainer>(), Has.Argument<string>(0)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>(), "abc");

                // Then
                instance.ShouldBe("abc");
            }
        }

        [Fact]
        public void ShouldEmitArgumentForRefTypeIndex1()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<MyClass>(Key.Create<MyClass>(), Mock.Of<IContainer>(), Has.Argument<MyClass>(1)))
            {
                var value = new MyClass();

                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>(), "abc", value);

                // Then
                instance.ShouldBe(value);
            }
        }

        public class MyClass
        {
        }

        private static ResolverGenerator CreateInstance()
        {
            return new ResolverGenerator(
                new DependencyEmitter(
                    Mock.Of<IDependencyEmitter<Value>>(),
                    new ArgumentEmitter(),
                    Mock.Of<IDependencyEmitter<FactoryMethod>>(),
                    Mock.Of<IDependencyEmitter<StaticMethod>>(),
                    Mock.Of<IDependencyEmitter<Autowiring>>()),
                Mock.Of<ILifetimeEmitter>());
        }
    }
}
