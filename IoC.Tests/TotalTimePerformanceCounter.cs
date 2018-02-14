namespace IoC.Tests
{
    using System;
    using System.Diagnostics;
    using Core;

    public class TotalTimePerformanceCounter: IPerformanceCounter
    {
        private readonly double _prformanceRate;

        public TotalTimePerformanceCounter(double prformanceRate = 1)
        {
            _prformanceRate = prformanceRate;
        }

        public ITestResult Result { get; private set; }

        public IDisposable Run()
        {
            var stopwatch = Stopwatch.StartNew();
            return Disposable.Create(() =>
            {
                Result = new TestResult((long)(stopwatch.ElapsedMilliseconds * _prformanceRate));
                stopwatch.Stop();
            });
        }

        private class TestResult : ITestResult, IComparable
        {
            private readonly long _elapsedMilliseconds;

            public TestResult(long elapsedMilliseconds)
            {
                _elapsedMilliseconds = elapsedMilliseconds;
            }

            public int CompareTo(object obj)
            {
                return obj is TestResult other ? (int)(_elapsedMilliseconds - other._elapsedMilliseconds) : 0;
            }

            public override string ToString()
            {
                return $"{_elapsedMilliseconds} ms";
            }
        }
    }
}
