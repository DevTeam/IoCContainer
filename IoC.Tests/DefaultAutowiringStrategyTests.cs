// ReSharper disable EmptyConstructor
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local
#if !NET40
namespace IoC.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Core;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DefaultAutowiringStrategyTests
    {
        [Theory]
        [InlineData(typeof(DefaultCtorClass), true)]
        [InlineData(typeof(PrivateDefaultCtorClass), true)]
        [InlineData(typeof(CtorClass), true)]
        [InlineData(typeof(CtorClass2), true)]
        [InlineData(typeof(NoCtorClass), false)]
        [InlineData(typeof(ObsoleteClass), true)]
        [InlineData(typeof(Obsolete2Class), true)]
        [InlineData(typeof(CannotResolveClass), true)]
        public void ShouldResolveConstructor(Type type, bool expectedResult)
        {
            // Given
            var autowiringStrategy = DefaultAutowiringStrategy.Shared;
            var container = new Mock<IContainer>();
            IDependency dependency;
            ILifetime lifetime;
            container.Setup(i => i.TryGetDependency(It.Is<Key>(i => i.Type == typeof(int)), out dependency, out lifetime)).Returns(true);

            // When
            var actualResult = autowiringStrategy
                .TryResolveConstructor(
                    container.Object,
                    type
                        .Descriptor()
                        .GetDeclaredConstructors()
                        .Where(method => !method.IsStatic && (method.IsAssembly || method.IsPublic))
                        .Select(constructorInfo => new Method<ConstructorInfo>(constructorInfo)),
                    out var actualConstructor);

            // Then
            actualResult.ShouldBe(expectedResult);
            if (actualResult)
            {
                actualConstructor.Info.GetCustomAttribute<MarkerAttribute>().ShouldNotBeNull();
            }
        }

        [Theory]
        [InlineData(typeof(MyClass), true, 0)]
        [InlineData(typeof(MyEmptyClass), true, 0)]
        public void ShouldResolveInitializers(Type type, bool expectedResult, int expectedInitializersCount)
        {
            // Given
            var autowiringStrategy = DefaultAutowiringStrategy.Shared;

            // When
            var actualResult = autowiringStrategy.TryResolveInitializers(Mock.Of<IContainer>(), type.Descriptor().GetDeclaredMethods().Where(method => !method.IsStatic && (method.IsAssembly || method.IsPublic)).Select(methodInfo => new Method<MethodInfo>(methodInfo)), out var actualInitializers);
            var initializers = actualInitializers.ToList();

            // Then
            actualResult.ShouldBe(expectedResult);
            initializers.Count.ShouldBe(expectedInitializersCount);
            if (actualResult)
            {
                initializers.All(i =>i.Info.GetCustomAttribute<MarkerAttribute>() != null).ShouldBeTrue();
            }
        }

        private class MarkerAttribute: Attribute
        {
        }

        private class DefaultCtorClass
        {
            static DefaultCtorClass() {}

            public DefaultCtorClass() { }

            [Marker]
            public DefaultCtorClass(int i) { }
        }

        private class PrivateDefaultCtorClass
        {
            static PrivateDefaultCtorClass() {}

            private PrivateDefaultCtorClass() { }

            [Marker]
            public PrivateDefaultCtorClass(int i) { }
        }

        private class CtorClass
        {
            static CtorClass() {}

            private CtorClass() { }

            [Marker]
            public CtorClass(int i, int j) { }

            public CtorClass(int i) { }
        }

        private class CtorClass2
        {
            static CtorClass2() { }

            private CtorClass2() { }

            [Marker]
            public CtorClass2(int i, int j) { }

            public CtorClass2(int i, int j, string str) { }
        }

        private class ObsoleteClass
        {
            static ObsoleteClass() { }

            private ObsoleteClass() { }

            [Obsolete]
            public ObsoleteClass(int i, int j) { }

            [Marker]
            public ObsoleteClass(int i) { }
        }

        private class Obsolete2Class
        {
            [Obsolete]
            [Marker]
            public Obsolete2Class(int i, int j) { }
        }

        private class NoCtorClass
        {
            static NoCtorClass() {}

            private NoCtorClass() { }

            private NoCtorClass(int i) { }
        }

        private class MyClass
        {
            [Marker]
            public void Init() { }

            [Marker]
            public void Init(int i) { }

            [Marker]
            internal void InternalInit() { }

            [Marker]
            internal void InternalInit(string i) { }

            public string Value { get; [Marker] set; }

            internal string InternalValue { get; [Marker] set; }
        }

        private class CannotResolveClass
        {
            [Marker]
            public CannotResolveClass(string str) { }
        }

        private class MyEmptyClass
        {
            private void Init() { }
        }
    }
}
#endif