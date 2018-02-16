namespace IoC
{
    [PublicAPI]
    public enum Lifetime
    {
        // Default lifetime. New instance each time (default).
        Transient = 1,

        // Single instance per registration
        Singleton = 2,

        // Singleton per container
        ContainerSingleton = 3,

        // Singleton per scope
        ScopeSingleton = 4,

#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_5 && !NETSTANDARD1_6
        // Thread per thread
        ThreadSingleton = 5,
#endif
    }
}
