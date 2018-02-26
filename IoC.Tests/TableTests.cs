namespace IoC.Tests
{
    using Core.Collections;
    using Shouldly;
    using Xunit;

    public class TableTests
    {
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
            table.TryGet("c".GetHashCode(), "c", out var val).ShouldBeTrue();
            val.ShouldBe("c");

            table.TryGet("c".GetHashCode(), "Z", out var val2).ShouldBeTrue();
            val2.ShouldBe("Z");
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
            table = table.Remove("c".GetHashCode(), "c");

            // Then
            table.TryGet("c".GetHashCode(), "c", out var _).ShouldBeFalse();
        }
    }
}
