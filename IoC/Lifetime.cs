namespace IoC
{
    [PublicAPI]
    public enum Lifetime
    {
        // Default lifetime. New instance each time.
        Transient,

        // Single instance per registration
        Singleton,

        // Singleton per container
        Container,

        // Singleton per scope
        Scope
    }
}
