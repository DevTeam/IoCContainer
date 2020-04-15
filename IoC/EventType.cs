namespace IoC
{
    /// <summary>
    /// Container event types.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// On container creation.
        /// </summary>
        CreateContainer,

        /// <summary>
        /// On container disposing.
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
        /// On resolver compilation.
        /// </summary>
        ResolverCompilation
    }
}