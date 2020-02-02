namespace IoC
{
    /// <summary>
    /// The enumeration of well-known lifetimes.
    /// </summary>
    [PublicAPI]
    public enum Lifetime
    {
        /// <summary>
        /// A new instance is creating each time (it's default).
        /// </summary>
        Transient = 1,

        /// <summary>
        /// Single instance.
        /// </summary>
        Singleton = 2,

        /// <summary>
        /// Singleton instance per container.
        /// </summary>
        ContainerSingleton = 3,

        /// <summary>
        /// Singleton instance per scope.
        /// </summary>
        ScopeSingleton = 4
    }
}
