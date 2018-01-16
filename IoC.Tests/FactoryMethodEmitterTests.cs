namespace IoC.Tests
{
    using Core;
    using Core.Emiters;
    using Dependencies;
    using Moq;
    using Shouldly;
    using Xunit;

    public class FactoryMethodEmitterTests
    {
        [Fact]
        public void ShouldEmitFunctionCallForValueType()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<int>(Key.Create<int>(), Mock.Of<IContainer>(), Has.Factory<object>((key, container, args) => 10)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(10);
            }
        }

        [Fact]
        public void ShouldEmitGenericFunctionCallForValue()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<object>(Key.Create<int>(), Mock.Of<IContainer>(), Has.Factory((key, container, args) => 10)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(10);
            }
        }

        [Fact]
        public void ShouldEmitFunctionCallForRefType()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<string>(Key.Create<string>(), Mock.Of<IContainer>(), Has.Factory<object>((key, container, args) => "abc")))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe("abc");
            }
        }

        [Fact]
        public void ShouldEmitGenericFunctionCallForRefType()
        {
            // Given
            var resolverGenerator = CreateInstance();
            var resolvingKey = Key.Create<string>();
            var resolvingContainer = Mock.Of<IContainer>();
            using (var resolver = resolverGenerator.Generate<object>(Key.Create<string>(), Mock.Of<IContainer>(), Has.Factory<object>((key, container, args) =>
            {
                key.ShouldBe(resolvingKey);
                container.ShouldBe(resolvingContainer);
                return "abc";
            })))
            {
                // When
                var instance = resolver.Resolve(resolvingContainer);

                // Then
                instance.ShouldBe("abc");
            }
        }

        [Fact]
        public void ShouldEmitGenericFunctionCallForBaseRef()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<IMyClass>(Key.Create<MyClass>(), Mock.Of<IContainer>(), Has.Factory<object>((key, container, args) => new MyClass())))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBeOfType<MyClass>();
            }
        }

        public interface IMyClass
        {
        }

        public class MyClass: IMyClass
        {
        }

        private static ResolverGenerator CreateInstance()
        {
            return new ResolverGenerator(
                new DependencyEmitter(
                    Mock.Of<IEmitter<Value>>(),
                    Mock.Of<IEmitter<Argument>>(),
                    new FactoryMethodEmitter(),
                    Mock.Of<IEmitter<StaticMethod>>(),
                    Mock.Of<IEmitter<Autowiring>>()));
        }
    }
}
