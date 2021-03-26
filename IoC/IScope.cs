namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstraction of a scope which is used with <c>Lifetime.ScopeSingleton</c> and <c>Lifetime.ScopeRoot</c>.
    /// </summary>
    [PublicAPI]
    public interface IScope : IScopeToken
    {
        IContainer Container { get; }

        /// <summary>
        /// Activate the scope.
        /// </summary>
        /// <returns>The token to deactivate the activated scope.</returns>
        IDisposable Activate();
    }
}