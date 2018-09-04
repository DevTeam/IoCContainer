namespace IoC
{
    using System;

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
        /// The changed binding key.
        /// </summary>
        public readonly Key Key;

        internal ContainerEvent([NotNull] IContainer container, EventType eventTypeType, Key key)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            EventTypeType = eventTypeType;
            Key = key;
        }
    }
}
