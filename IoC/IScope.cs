namespace IoC
{
    using System;

    /// <summary>
    /// Represents the scope which could be used with <c>Lifetime.ScopeSingleton</c>
    /// </summary>
    [PublicAPI]
    public interface IScope : IDisposable
    {
        /// <summary>
        /// Activate the scope.
        /// </summary>
        /// <returns>The token to deactivate the scope.</returns>
        IDisposable Activate();
    }
}