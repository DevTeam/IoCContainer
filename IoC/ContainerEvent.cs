namespace IoC
{
    using System;
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
        public readonly IEnumerable<Key> Keys;

        internal ContainerEvent([NotNull] IContainer container, EventType eventTypeType, IEnumerable<Key> keys)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            EventTypeType = eventTypeType;
            Keys = keys;
        }
    }
}
