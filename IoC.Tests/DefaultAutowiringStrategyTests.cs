// ReSharper disable EmptyConstructor
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local
#if !NET40
namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Core;
    using IntegrationTests;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DefaultAutowiringStrategyTests
    {
        [Theory]
        [InlineData(typeof(string), typeof(string), typeof(string), true)]
        [InlineData(typeof(List<>), typeof(IEnumerable<string>), typeof(List<TT>), true)]
        [InlineData(typeof(AutowiringTests.MyGenericServiceWithConstraint<,>), typeof(AutowiringTests.IMyGenericServiceWithConstraint<IEnumerable<string>, int>), typeof(AutowiringTests.MyGenericServiceWithConstraint<TT, IEnumerable<string>>), true)]
        public void ShouldDefinedGenericType(Type type, Type targetType, Type expectedType, bool expectedResult)
        {
            // Given
            var autowiringStrategy = DefaultAutowiringStrategy.Shared;

            // When
            var actualResult = autowiringStrategy.TryResolveType(type, targetType, out var actualType);

            // Then
            actualResult.ShouldBe(expectedResult);
            if (actualResult)
            {
                actualType.ShouldBe(expectedType);
            }
        }

        [Theory]
        [InlineData(typeof(DefaultCtorClass), true)]
        [InlineData(typeof(PrivateDefaultCtorClass), true)]
        [InlineData(typeof(CtorClass), true)]
        [InlineData(typeof(NoCtorClass), false)]
        public void ShouldResolveConstructor(Type type, bool expectedResult)
        {
            // Given
            var autowiringStrategy = DefaultAutowiringStrategy.Shared;

            // When
            var actualResult = autowiringStrategy.TryResolveConstructor(type.Descriptor().GetDeclaredConstructors().Where(method => !method.IsStatic && (method.IsAssembly || method.IsPublic)).Select(constructorInfo => new Method<ConstructorInfo>(constructorInfo)), out var actualConstructor);

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
            var actualResult = autowiringStrategy.TryResolveInitializers(type.Descriptor().GetDeclaredMethods().Where(method => !method.IsStatic && (method.IsAssembly || method.IsPublic)).Select(methodInfo => new Method<MethodInfo>(methodInfo)), out var actualInitializers);
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

            [Marker]
            public DefaultCtorClass() { }

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

            public CtorClass(int i, int j) { }

            [Marker]
            public CtorClass(int i) { }
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

        private class MyEmptyClass
        {
            private void Init() { }
        }
    }
}
#endif