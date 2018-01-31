namespace IoC.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Core;
    using Core.Emitters;
    using Dependencies;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class ValueEmitterTests
    {
        [Fact]
        public void ShouldEmitInt()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<int>(Key.Create<int>(), Mock.Of<IContainer>(), Has.Value(10)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(10);
            }
        }

        [Fact]
        public void ShouldEmitLong()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<long>(Key.Create<long>(), Mock.Of<IContainer>(), Has.Value(10L)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(10L);
            }
        }

        [Fact]
        public void ShouldEmitFloat()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<float>(Key.Create<float>(), Mock.Of<IContainer>(), Has.Value(10.1f)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(10.1f);
            }
        }

        [Fact]
        public void ShouldEmitDouble()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<double>(Key.Create<double>(), Mock.Of<IContainer>(), Has.Value(10.1d)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(10.1d);
            }
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("")]
        [InlineData("Abc10")]
        public void ShouldEmitString(string val)
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<string>(Key.Create<string>(), Mock.Of<IContainer>(), Has.Value(val)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(val);
            }
        }

        [Fact]
        public void ShouldEmitNullString()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<object>(Key.Create<string>(), Mock.Of<IContainer>(), Has.Value<string>(null)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(null);
            }
        }

        [Fact]
        public void ShouldEmitRef()
        {
            // Given
            var resolverGenerator = CreateInstance();
            var val = new object();
            using (var resolver = resolverGenerator.Generate<object>(Key.Create<object>(), Mock.Of<IContainer>(), Has.Value(val)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(val);
            }
        }

        [Fact]
        public void ShouldEmitStruct()
        {
            // Given
            var resolverGenerator = CreateInstance();
            var val = new MyStruct {Value = 10};
            using (var resolver = resolverGenerator.Generate<MyStruct>(Key.Create<MyStruct>(), Mock.Of<IContainer>(), Has.Value(val)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(val);
            }
        }

        [Fact]
        public void ShouldEmitBaseRef()
        {
            // Given
            var resolverGenerator = CreateInstance();
            var val = new MyClass();
            using (var resolver = resolverGenerator.Generate<MyBaseClass>(Key.Create<MyClass>(), Mock.Of<IContainer>(), Has.Value(val)))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBe(val);
            }
        }

        private struct MyStruct
        {
#pragma warning disable 414
            public int Value;
#pragma warning restore 414
        }

        private class MyBaseClass
        {
        }

        private class MyClass: MyBaseClass
        {
        }

        private static ResolverGenerator CreateInstance()
        {
            return new ResolverGenerator(
                new DependencyEmitter(
                    new ValueEmitter(),
                    Mock.Of<IDependencyEmitter<Argument>>(),
                    Mock.Of<IDependencyEmitter<FactoryMethod>>(),
                    Mock.Of<IDependencyEmitter<StaticMethod>>(),
                    Mock.Of<IDependencyEmitter<Autowiring>>()),
                Mock.Of<ILifetimeEmitter>());
        }
    }
}
