namespace IoC
{
    /// <summary>
    /// Represents the enumeration of well-known containers.
    /// </summary>
    [PublicAPI]
    public enum WellknownContainers
    {
        /// <summary>
        /// Current container.
        /// </summary>
        Current = 1,

        /// <summary>
        /// Parent container.
        /// </summary>
        Parent = 2,

        /// <summary>
        /// Creates new child container.
        /// </summary>
        Child = 3
    }
}
