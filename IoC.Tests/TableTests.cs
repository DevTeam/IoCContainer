namespace IoC.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Shouldly;
    using Xunit;

    public class TableTests
    {
        [Fact]
        public void ShouldUseBucketsForSameHashCode()
        {
            // Given
            var table = Table<string, string>.Empty;

            // When
            table = table.Set("c", "c");
            table = table.Set("Z", "Z");
            
            // Then
            table.Count().ShouldBe(2);
        }

        [Fact]
        public void ShouldEnumerate()
        {
            // Given
            var table = Table<string, string>.Empty;

            // When
            table = table.Set("c", "c");
            table = table.Set("Z", "Z");
            var dict = table.ToDictionary(i => i.Key, i => i.Value).ToList();

            // Then
            dict.Count.ShouldBe(2);
            dict.Contains(new KeyValuePair<string, string>("c", "c")).ShouldBeTrue();
            dict.Contains(new KeyValuePair<string, string>("Z", "Z")).ShouldBeTrue();
        }

        [Fact]
        public void ShouldSetAndGet()
        {
            // Given
            var table = Table<string, string>.Empty;

            // When
            table = table.Set("a", "a");
            table = table.Set("b", "b");
            table = table.Set("c", "c");
            table = table.Set("d", "d");
            table = table.Set("Z", "Z");

            // Then
            table.Count().ShouldBe(5);
            table.Get("c").ShouldBe("c");
            table.Get("Z").ShouldBe("Z");
        }

        [Fact]
        public void ShouldRemove()
        {
            // Given
            var table = Table<string, string>.Empty;
            table = table.Set("a", "a");
            table = table.Set("b", "b");
            table = table.Set("Z", "Z");
            table = table.Set("c", "c");
            table = table.Set("d", "d");

            // When

            // Then
            table = table.Remove("c", out var removed);
            removed.ShouldBe(true);
            table.Get("c").ShouldBe(null);

            table = table.Remove("d", out removed);
            removed.ShouldBe(true);

            table = table.Remove("Z", out removed);
            removed.ShouldBe(true);

            table = table.Remove("a", out removed);
            removed.ShouldBe(true);

            table.Count().ShouldBe(1);
            table.Count.ShouldBe(1);
        }
    }
}
