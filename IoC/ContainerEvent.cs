namespace IoC
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides information about changes in the container.
    /// </summary>
    [PublicAPI]
    public struct ContainerEvent
    {
        /// <summary>
        /// The target container.
        /// </summary>
        [NotNull] public readonly IContainer Container;

        /// <summary>
        /// The type of event.
        /// </summary>
        public readonly EventType EventTypeType;

        /// <summary>
        /// The changed keys.
        /// </summary>
        [NotNull] public readonly IEnumerable<Key> Keys;

        /// <summary>
        /// Create new instance of container event.
        /// </summary>
        /// <param name="container">The origin container.</param>
        /// <param name="eventTypeType">The vent type.</param>
        /// <param name="keys">The set of keys related to this event.</param>
        public ContainerEvent([NotNull] IContainer container, EventType eventTypeType, IEnumerable<Key> keys)
        {
            Container = container;
            EventTypeType = eventTypeType;
            Keys = keys;
        }
    }
}
