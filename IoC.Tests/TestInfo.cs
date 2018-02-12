namespace IoC.Tests
{
    using System;

    public class TestInfo
    {
        public TestInfo(
            [NotNull] string name,
            [NotNull] Action<int, IPerformanceCounter> test)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Test = test ?? throw new ArgumentNullException(nameof(test));
        }


        public string Name { get; }

        public Action<int, IPerformanceCounter> Test { get; }

        public double PerformanceRate { get; set; } = 1;
    }
}
