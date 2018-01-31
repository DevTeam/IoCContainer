namespace IoC
{
    [PublicAPI]
    public enum Lifetime
    {
        // Default lifetime. New instance each time.
        Transient,

        // Single instance per registration
        Singletone,

        // Singletone per container
        Container,

        // Singletone per scope
        Scope
    }
}
