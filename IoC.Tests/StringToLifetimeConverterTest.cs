namespace IoC.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using Core.Configuration;
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
                new object[] {".Lifetime(Lifetime.Transient)", true, Lifetime.Transient},
                new object[] { ".Lifetime(Lifetime.Singletone)", true, Lifetime.Singletone},
                new object[] { ".Lifetime(Lifetime.Resolve)", true, Lifetime.Resolve},
                new object[] { ".Lifetime(Lifetime.Container)", true, Lifetime.Container},
                new object[] { ".Lifetime(Lifetime.container)", true, Lifetime.Container},
                new object[] { ".LifeTime(Container)", true, Lifetime.Container},
                new object[] { ".Lifetime(container)", true, Lifetime.Container},
                new object[] { ".Lifetime(Lifetime.Transient).Lifetime(Lifetime.Singletone)", true, Lifetime.Singletone},
                new object[] { " . Lifetime  (  Lifetime.Singletone  )  ", true, Lifetime.Singletone},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
