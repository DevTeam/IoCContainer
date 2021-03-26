namespace IoC
{
    /// <summary>
    /// A set of well-known lifetimes.
    /// </summary>
    [PublicAPI]
    public enum Lifetime
    {
        /// <summary>
        /// For a new instance each time (default).
        /// </summary>
        Transient = 1,

        /// <summary>
        /// For a singleton instance.
        /// </summary>
        Singleton = 2,

        /// <summary>
        /// For a singleton instance per container.
        /// </summary>
        ContainerSingleton = 3,

        /// <summary>
        /// Automatically creates a new scope.
        /// </summary>
        ScopeRoot = 4,

        /// <summary>
        /// Automatically calls a <c>Disposable()</c> method for disposable instances after a container has disposed off.
        /// </summary>
        Disposing = 5
    }
}
