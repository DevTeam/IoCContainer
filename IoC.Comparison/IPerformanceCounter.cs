namespace IoC.Comparison
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    public interface IPerformanceCounter
    {
        // ReSharper disable once UnusedMemberInSuper.Global
        ITestResult Result { get; }

        IDisposable Run();
    }
}
