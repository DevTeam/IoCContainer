namespace IoC.Tests
{
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
            table = table.Set("c".GetHashCode(), "c", "c");
            table = table.Set("c".GetHashCode(), "Z", "Z");
            
            // Then
            table.Count().ShouldBe(2);
        }

        [Fact]
        public void ShouldSetAndGet()
        {
            // Given
            var table = Table<string, string>.Empty;

            // When
            table = table.Set("a".GetHashCode(), "a", "a");
            table = table.Set("b".GetHashCode(), "b", "b");
            table = table.Set("c".GetHashCode(), "c", "c");
            table = table.Set("d".GetHashCode(), "d", "d");
            table = table.Set("c".GetHashCode(), "Z", "Z");

            // Then
            table.Count().ShouldBe(5);
            table.GetByRef("c".GetHashCode(), "c").ShouldBe("c");
            table.GetByRef("c".GetHashCode(), "Z").ShouldBe("Z");
        }

        [Fact]
        public void ShouldRemove()
        {
            // Given
            var table = Table<string, string>.Empty;
            table = table.Set("a".GetHashCode(), "a", "a");
            table = table.Set("b".GetHashCode(), "b", "b");
            table = table.Set("c".GetHashCode(), "Z", "Z");
            table = table.Set("c".GetHashCode(), "c", "c");
            table = table.Set("d".GetHashCode(), "d", "d");

            // When
            table = table.Remove("c".GetHashCode(), "c", out var removed);

            // Then
            removed.ShouldBe(true);
            table.GetByRef("c".GetHashCode(), "c").ShouldBe(null);
            table.Count().ShouldBe(4);
            table.Count.ShouldBe(4);
        }
    }
}
