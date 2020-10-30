namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstraction of a scope token which is used with <c>Lifetime.ScopeSingleton</c> and <c>Lifetime.ScopeRoot</c>.
    /// </summary>
    [PublicAPI]
    public interface IScopeToken : IDisposable
    {
    }
}