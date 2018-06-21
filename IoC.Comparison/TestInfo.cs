namespace IoC.Comparison
{
    using System;

    public class TestInfo
    {
        public TestInfo(
            [NotNull] string name,
            [NotNull] Action<long, IPerformanceCounter> test)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Test = test ?? throw new ArgumentNullException(nameof(test));
        }


        public string Name { get; }

        public Action<long, IPerformanceCounter> Test { get; }

        public double PerformanceRate { get; set; } = 1;
    }
}
