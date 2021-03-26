namespace IoC.Core
{
    using System;

    internal interface IScopeManager
    {
        [NotNull] IScope Current { get; }

        [NotNull] IDisposable Activate([NotNull] IScope scope);
    }
}