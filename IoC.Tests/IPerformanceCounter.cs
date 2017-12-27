namespace IoC.Tests
{
    using System;

    public interface IPerformanceCounter
    {
        ITestResult Result { get; }

        IDisposable Run();
    }
}
