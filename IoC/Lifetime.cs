namespace IoC
{
    /// <summary>
    /// The enumeration of well-known lifetimes.
    /// </summary>
    [PublicAPI]
    public enum Lifetime
    {
        /// <summary>
        /// It is default lifetime. A new instance wll be created each time.
        /// </summary>
        Transient = 1,

        /// <summary>
        /// Singleton
        /// </summary>
        Singleton = 2,

        /// <summary>
        /// Singleton per container
        /// </summary>
        ContainerSingleton = 3,

        /// <summary>
        /// Singleton per scope
        /// </summary>
        ScopeSingleton = 4
    }
}
