namespace IoC
{
    [PublicAPI]
    public enum Lifetime
    {
        // Default lifetime. New instance each time (default).
        Transient,

        // Single instance per registration
        Singleton,

        // Singleton per container
        ContainerSingleton,

        // Singleton per scope
        ScopeSingleton,

#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_5 && !NETSTANDARD1_6
        // Thread per thread
        ThreadSingleton,
#endif
    }
}
