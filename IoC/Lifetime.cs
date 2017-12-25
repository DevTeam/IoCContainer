namespace IoC
{
    [PublicAPI]
    public enum Lifetime
    {
        // New instance each time
        Transient,

        // Single instance per registration
        Singletone,

        // Singletone per container
        Container,

        // Singletone per resolve
        Resolve
    }
}
