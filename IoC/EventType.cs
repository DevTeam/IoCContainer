namespace IoC
{
    /// <summary>
    /// The types of event.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// On container creation.
        /// </summary>
        CreateContainer,

        /// <summary>
        /// On container dispose.
        /// </summary>
        DisposeContainer,

        /// <summary>
        /// On dependency registration.
        /// </summary>
        RegisterDependency,

        /// <summary>
        /// On dependency unregistration.
        /// </summary>
        UnregisterDependency,

        /// <summary>
        /// On resolver compilation
        /// </summary>
        ResolverCompilation
    }
}