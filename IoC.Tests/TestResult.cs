namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;

    internal sealed class TestResult : IComparable<TestResult>
    {
        private readonly string _name;
        [NotNull] private readonly ITestResult _testResult;

        public TestResult(
            [NotNull] string name,
            [NotNull] ITestResult testResult)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            _name = name;
            _testResult = testResult ?? throw new ArgumentNullException(nameof(testResult));
        }

        public int CompareTo(TestResult other)
        {
            return Comparer<ITestResult>.Default.Compare(_testResult, other._testResult);
        }

        public override string ToString()
        {
            return $"<td>{_name}</td><td>{_testResult}</td>";
        }
    }

}
