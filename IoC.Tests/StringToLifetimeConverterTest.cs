#if !NET40
namespace IoC.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using Core.Configuration;
    using Extensibility;
    using Moq;
    using Shouldly;
    using Xunit;

    public class StringToLifetimeConverterTest
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void ShouldConvertStringToLifetime(string text, bool expectedResult, Lifetime expectedLifetime)
        {
            // Given
            var converter = new StringToLifetimeConverter(Mock.Of<IIssueResolver>());

            // When
            var actualResult = converter.TryConvert(new Statement("statement", 1, 2), text, out var actualLifetime);

            // Then
            actualResult.ShouldBe(expectedResult);
            actualLifetime.ShouldBe(expectedLifetime);
        }

        private class TestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {".As(Lifetime.Transient)", true, Lifetime.Transient},
                new object[] { ".As(Lifetime.Singleton)", true, Lifetime.Singleton},
                new object[] { ".As(Lifetime.ScopeSingleton)", true, Lifetime.ScopeSingleton},
                new object[] { ".As(Lifetime.ContainerSingleton)", true, Lifetime.ContainerSingleton},
                new object[] { ".As(Lifetime.containerSingleton)", true, Lifetime.ContainerSingleton},
                new object[] { ".As(ContainerSingleton)", true, Lifetime.ContainerSingleton},
                new object[] { ".As(containerSingleton)", true, Lifetime.ContainerSingleton},
                new object[] { ".As(Lifetime.Transient).As(Lifetime.Singleton)", true, Lifetime.Singleton},
                new object[] { " . As  (  Lifetime.Singleton  )  ", true, Lifetime.Singleton},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
#endif
