namespace IoC.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class ContractTests
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void ShouldSupportGetHashCodeAndEquals(Contract contract1, Contract contract2, bool equals)
        {
            // Given

            // When
            var hashCode1 = contract1.GetHashCode();
            var hashCode2 = contract2.GetHashCode();
            var equals1 = Equals(contract1, contract2);
            var equals2 = Equals(contract2, contract1);

            // Then
            if (equals)
            {
                hashCode1.ShouldBe(hashCode2);
                equals1.ShouldBeTrue();
                equals2.ShouldBeTrue();
            }
            else
            {
                equals1.ShouldBeFalse();
                equals2.ShouldBeFalse();
            }
        }

        private class TestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {new Contract(typeof(string)), new Contract(typeof(string)), true},
                new object[] {new Contract(typeof(string)), new Contract(typeof(int)), false},
                new object[] {new Contract(typeof(object)), new Contract(typeof(object)), true},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
