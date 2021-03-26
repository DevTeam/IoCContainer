namespace IoC.Benchmark.Model
{
    using System;

    public class Disposable: IDisposable
    {
        public bool IsDisposed;

        public void Dispose() => IsDisposed = true;
    }
}
