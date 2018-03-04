namespace IoC.Comparison
{
    using System;

    public interface IPerformanceCounter
    {
        ITestResult Result { get; }

        IDisposable Run();
    }
}
