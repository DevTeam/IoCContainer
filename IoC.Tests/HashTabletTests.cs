#if !NET40
namespace IoC.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Collections;
    using Shouldly;
    using Xunit;

    public class HashTabletTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        [InlineData(100)]
        public void ShouldGet(int number)
        {
            // Given
            var hashTable = HashTable<Key, string>.Empty;

            // When
            hashTable = AddKeys(hashTable, GenerateSimpleKeys(number));

            // Then
            CheckKeys(hashTable, GenerateSimpleKeys(number));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        [InlineData(100)]
        public void ShouldGetWhenTheSameValue(int number)
        {
            // Given
            var hashTable = HashTable<Key, string>.Empty;

            // When
            hashTable = AddKeys(hashTable, GenerateKeysWithEqualValue(number, "abc"));

            // Then
            CheckKeys(hashTable, GenerateKeysWithEqualValue(number, "abc"));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 10)]
        [InlineData(5, 10)]
        public void ShouldGetWhenTheSameHashCode(int number, int groupSize)
        {
            // Given
            var hashTable = HashTable<Key, string>.Empty;

            // When
            hashTable = AddKeys(hashTable, GenerateKeysWithEqualHashCode(number, groupSize));

            // Then
            CheckKeys(hashTable, GenerateKeysWithEqualHashCode(number, groupSize));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 10)]
        [InlineData(5, 10)]
        public void ShouldGetWhenTheSameHashCode2(int number, int groupSize)
        {
            // Given
            var hashTable = HashTable<Key, string>.Empty;

            // When
            hashTable = AddKeys(hashTable, GenerateKeysWithEqualHashCode2(number, groupSize));

            // Then
            CheckKeys(hashTable, GenerateKeysWithEqualHashCode2(number, groupSize));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(10)]
        public void ShouldGetWhenDifferentKeys(int number)
        {
            // Given
            var hashTable = HashTable<Key, string>.Empty;

            // When
            hashTable = AddKeys(hashTable, GenerateDifferentKeys(number));

            // Then
            CheckKeys(hashTable, GenerateDifferentKeys(number));
        }

        private static IEnumerable<Key> GenerateSimpleKeys(int number)
        {
            for (var i = 0; i < number; i++)
            {
                yield return new Key(i, i.ToString());
            }
        }

        private static IEnumerable<Key> GenerateKeysWithEqualHashCode(int number, int groupSize)
        {
            for (var group = 0; group < groupSize; group++)
            { 
                for (var i = 0; i < number; i++)
                {
                    yield return new Key(group, i.ToString());
                }
            }
        }

        private static IEnumerable<Key> GenerateKeysWithEqualHashCode2(int number, int groupSize)
        {
            for (var i = 0; i < number; i++)
            {
                for (var group = 0; group < groupSize; group++)
                {
                    yield return new Key(group, i.ToString());
                }
            }
        }

        private static IEnumerable<Key> GenerateKeysWithEqualValue(int number, string value)
        {
            for (var i = 0; i < number; i++)
            {
                yield return new Key(i, value);
            }
        }

        private static IEnumerable<Key> GenerateDifferentKeys(int number)
        {
            return GenerateDifferentKeySets(number).SelectMany(i => i).Distinct();
        }

        private static IEnumerable<IEnumerable<Key>> GenerateDifferentKeySets(int number)
        {
            yield return GenerateSimpleKeys(number);
            yield return GenerateKeysWithEqualHashCode2(number + 10, 10);
            yield return GenerateKeysWithEqualValue(number + 20, "abc");
            yield return GenerateKeysWithEqualHashCode(number + 30, 10);
            yield return GenerateKeysWithEqualValue(number + 40, "xyz");
        }

        private static HashTable<Key, string> AddKeys(HashTable<Key, string> hashTable, IEnumerable<Key> keys)
        {
            foreach (var key in keys)
            {
                hashTable = hashTable.Add(key, key.ToString());
            }

            return hashTable;
        }

        private static void CheckKeys(HashTable<Key, string> hashTable, IEnumerable<Key> keys)
        {
            var curKeys = keys.ToArray();
            foreach (var key in curKeys)
            {
                hashTable.Get(key).ShouldBe(key.ToString());
            }

            var expected = new HashSet<string>(curKeys.Select(i => i.ToString()));
            var actual = new HashSet<string>(hashTable.Enumerate().Select(i => i.Value.ToString()));
            actual.SetEquals(expected).ShouldBeTrue();
        }

        private class Key
        {
            private readonly int _hashCode;
            private readonly string _value;

            public Key(int hashCode, string value)
            {
                _hashCode = hashCode;
                _value = value;
            }

            public override string ToString()
            {
                return $"{_hashCode} {_value}";
            }

            public override bool Equals(object obj)
            {
                return Equals((Key) obj);
            }

            public override int GetHashCode()
            {
                return _hashCode;
            }
            private bool Equals(Key other)
            {
                return other._value == _value;
            }
        }
    }
}
#endif