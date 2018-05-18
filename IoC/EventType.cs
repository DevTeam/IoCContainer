namespace IoC
{
    /// <summary>
    /// The types of event.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// The dependency was registered.
        /// </summary>
        DependencyRegistration,

        /// <summary>
        /// The dependency was unregistered.
        /// </summary>
        DependencyUnregistration,
    }
}