#if !NET40
// ReSharper disable UnusedTypeParameter
namespace IoC.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Core.Configuration;
    using IoC;
    using Shouldly;
    using Xunit;

    public class StringToTypeConverterTests
    {
        [Theory]
        [InlineData("", "", "int", true, typeof(int))]
        [InlineData("", "", "int2", false, null)]
        [InlineData("", "", "System.Int32", true, typeof(int))]
        [InlineData("", "", "string", true, typeof(string))]
        [InlineData("", "", "System.String", true, typeof(string))]
        [InlineData("", "System", "Func<>", true, typeof(Func<>))]
        [InlineData("", "", "Func<>", false, null)]
        [InlineData("", "", "System.Func<>", true, typeof(Func<>))]
        [InlineData("", "System", "Func<int, string>", true, typeof(Func<int,string>))]
        [InlineData("", "", "System.Func<int, string>", true, typeof(Func<int, string>))]
        [InlineData("IoC", "IoC, System", "Func<IContainer, string>", true, typeof(Func<IContainer, string>))]
        [InlineData("IoC", "IoC", "IContainer", true, typeof(IContainer))]
        [InlineData("IoC", "", "IoC.IContainer", true, typeof(IContainer))]
        [InlineData("IoC.Tests", "IoC.Tests", "IService<>", true, typeof(IService<>))]
        [InlineData("IoC.Tests", "", "IoC.Tests.IService<>", true, typeof(IService<>))]
        [InlineData("IoC.Tests", "IoC.Tests", "IService<int,string>", true, typeof(IService<int, string>))]
        [InlineData("IoC.Tests", "IoC.Tests", "IService<,>", true, typeof(IService<,>))]
        [InlineData("IoC.Tests", "", "IoC.Tests.IService<,,>", true, typeof(IService<,,>))]
        [InlineData("IoC.Tests", "IoC.Tests", "IService<string>", true, typeof(IService<string>))]
        [InlineData("IoC.Tests", "IoC.Tests", "IService<IService<string>>", true, typeof(IService<IService<string>>))]
        [InlineData("IoC.Tests", "IoC.Tests", "IService<IService<string, int, float>, double, IService<IService<IService<string, int, float>>>>", true, typeof(IService<IService<string, int, float>, double, IService<IService<IService<string, int, float>>>>))]
        [InlineData("IoC.Tests", "IoC.Tests", "SomeClass.NestedClass", true, typeof(SomeClass.NestedClass))]
        [InlineData("IoC.Tests", "IoC.Tests", "SomeClass.NestedClass.NestedClass2", true, typeof(SomeClass.NestedClass.NestedClass2))]
        [InlineData("IoC.Tests", "IoC", "Tests.SomeClass.NestedClass.NestedClass2", true, typeof(SomeClass.NestedClass.NestedClass2))]
        [InlineData("IoC.Tests", "IoC.Tests", "SomeClass.NestedClass.NestedClass3<>", true, typeof(SomeClass.NestedClass.NestedClass3<>))]
        [InlineData("IoC.Tests", "IoC.Tests", "SomeClass.NestedClass.NestedClass3<int>", true, typeof(SomeClass.NestedClass.NestedClass3<int>))]
        public void ShouldConvertStringToType(
            string assemblies,
            string namespaces,
            string typeName,
            bool expectedResult,
            Type expectedType)
        {
            // Given
            var typeConverter = new StringToTypeConverter();
            var bindingContext = new BindingContext(
                    assemblies.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(assemblyName => Assembly.Load(new AssemblyName(assemblyName))),
                    namespaces.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                    Enumerable.Empty<Binding>()
                );

            // When
            var actualResult = typeConverter.TryConvert(bindingContext, typeName, out var actualType);

            // Then
            actualResult.ShouldBe(expectedResult);
            if (expectedResult)
            {
                actualType.ShouldBe(expectedType);
            }
        }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class SomeClass
    {
        public class NestedClass
        {
            public class NestedClass2
            {
            }

            // ReSharper disable once UnusedTypeParameter
            public class NestedClass3<T>
            {
            }
        }
    }

    public interface IService<T>
    {
    }

    public interface IService<T1, T2>
    {
    }

    public interface IService<T1, T2, T3>
    {
    }
}
#endif