namespace IoC
{
    /// <summary>
    /// The enumeration of well-known lifetimes.
    /// </summary>
    [PublicAPI]
    public enum Lifetime
    {
        /// <summary>
        /// Default lifetime. New instance each time (default).
        /// </summary>
        Transient = 1,

        /// <summary>
        /// Single instance per registration
        /// </summary>
        Singleton = 2,

        /// <summary>
        /// Singleton per container
        /// </summary>
        ContainerSingleton = 3,

        /// <summary>
        /// Singleton per scope
        /// </summary>
        ScopeSingleton = 4,

#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_5 && !NETSTANDARD1_6
        /// <summary>
        /// Thread per thread
        /// </summary>
        ThreadSingleton = 5,
#endif
    }
}
