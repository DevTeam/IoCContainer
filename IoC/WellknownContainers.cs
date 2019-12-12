namespace IoC
{
    /// <summary>
    /// The enumeration of well-known containers.
    /// </summary>
    [PublicAPI]
    public enum WellknownContainers
    {
        /// <summary>
        /// Parent container.
        /// </summary>
        Parent = 1,

        /// <summary>
        /// Creates new child container.
        /// </summary>
        NewChild = 2
    }
}
