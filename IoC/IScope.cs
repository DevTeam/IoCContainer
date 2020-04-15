namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstraction of a scope which is used with <c>Lifetime.ScopeSingleton</c>.
    /// </summary>
    [PublicAPI]
    public interface IScope : IDisposable
    {
        /// <summary>
        /// Activate the scope.
        /// </summary>
        /// <returns>The token to deactivate the activated scope.</returns>
        IDisposable Activate();
    }
}