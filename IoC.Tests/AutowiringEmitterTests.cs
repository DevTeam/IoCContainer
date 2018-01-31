namespace IoC.Tests
{
    using Core;
    using Core.Emitters;
    using Dependencies;
    using Moq;
    using Shouldly;
    using Xunit;

    public class AutowiringEmitterTests
    {
        [Fact]
        public void ShouldEmitNewObjectCreationWhenHasNoConstructor()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<object>(Key.Create<MyClassWithoutConstructor>(), Mock.Of<IContainer>(), Has.Autowiring<MyClassWithoutConstructor>()))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBeOfType<MyClassWithoutConstructor>();
            }
        }

        [Fact]
        public void ShouldEmitNewObjectCreationWhenConstructorWithoutArguments()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<object>(Key.Create<MyClass>(), Mock.Of<IContainer>(), Has.Autowiring<MyClass>()))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBeOfType<MyClass>();
            }
        }

        [Fact]
        public void ShouldEmitNewObjectCreationWhenConstructorWithValueArgument()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<object>(Key.Create<MyClass>(), Mock.Of<IContainer>(), Has.Autowiring<MyClass>(Has.Constructor(Has.Value(10).For("intValue")))))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBeOfType<MyClass>();
                ((MyClass)instance).IntValue.ShouldBe(10);
            }
        }

        [Fact]
        public void ShouldEmitNewObjectCreationWhenConstructorWithRefArgument()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<object>(Key.Create<MyClass>(), Mock.Of<IContainer>(), Has.Autowiring<MyClass>(Has.Constructor(Has.Value("abc").At(0)))))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBeOfType<MyClass>();
                ((MyClass) instance).StringValue.ShouldBe("abc");
            }
        }

        [Fact]
        public void ShouldEmitNewGenericObjectCreationWhenConstructorWithRefArgument()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<MyGenericClass<string>>(Key.Create<MyGenericClass<string>>(), Mock.Of<IContainer>(), Has.Autowiring<MyGenericClass<string>>(Has.Constructor(Has.Value("abc").At(0)))))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBeOfType<MyGenericClass<string>>();
                instance.Value.ShouldBe("abc");
            }
        }

        [Fact]
        public void ShouldEmitNewObjectCreationWhenHasMethod()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<object>(Key.Create<MyClass>(), Mock.Of<IContainer>(), Has.Autowiring<MyClass>(Has.Constructor(Has.Value("abc").At(0)), Has.Method("StringInitialize", Has.Value("xyz").At(0)))))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBeOfType<MyClass>();
                ((MyClass) instance).StringValue.ShouldBe("abc");
                ((MyClass) instance).StringValueFromInit.ShouldBe("xyz");
            }
        }

        [Fact]
        public void ShouldEmitNewObjectCreationWhenHasMethodReturningValue()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<object>(Key.Create<MyClass>(), Mock.Of<IContainer>(), Has.Autowiring<MyClass>(Has.Constructor(Has.Value("abc").At(0)), Has.Method("StringInitializeRet", Has.Value("xyz").At(0)))))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBeOfType<MyClass>();
                ((MyClass)instance).StringValue.ShouldBe("abc");
                ((MyClass)instance).StringValueFromInit.ShouldBe("xyz");
            }
        }


        [Fact]
        public void ShouldEmitNewGenericObject()
        {
            // Given
            var resolverGenerator = CreateInstance();
            using (var resolver = resolverGenerator.Generate<MyGenericClass<string>>(Key.Create<MyGenericClass<string>>(), Mock.Of<IContainer>(), Has.Autowiring(typeof(MyGenericClass<>), Has.Constructor(Has.Value("abc").At(0)))))
            {
                // When
                var instance = resolver.Resolve(Mock.Of<IContainer>());

                // Then
                instance.ShouldBeOfType<MyGenericClass<string>>();
                instance.Value.ShouldBe("abc");
            }
        }

        private static ResolverGenerator CreateInstance()
        {
            return new ResolverGenerator(
                new DependencyEmitter(
                    new ValueEmitter(),
                    Mock.Of<IDependencyEmitter<Argument>>(),
                    Mock.Of<IDependencyEmitter<FactoryMethod>>(),
                    Mock.Of<IDependencyEmitter<StaticMethod>>(),
                    new AutowiringEmitter()),
                Mock.Of<ILifetimeEmitter>());
        }

        public class MyClassWithoutConstructor
        {
        }

        public class MyClass
        {
            public readonly string StringValue;
            public readonly int IntValue;
            public string StringValueFromInit;

            public MyClass()
            {
            }

            public MyClass(int intValue)
            {
                IntValue = intValue;
            }

            public MyClass(string stringValue)
            {
                StringValue = stringValue;
            }

            public void StringInitialize(string stringValue)
            {
                StringValueFromInit = stringValue;
            }

            public string StringInitializeRet(string stringValue)
            {
                StringValueFromInit = stringValue;
                return "ret: " + stringValue;
            }
        }

        public class MyGenericClass<T>
        {
            public MyGenericClass(T value)
            {
                Value = value;
            }

            public T Value { get; }
        }
    }
}
