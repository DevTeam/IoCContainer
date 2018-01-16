namespace IoC
{
    using System;

    [PublicAPI]
    public struct ContainerEvent
    {
        [NotNull] public readonly IContainer Container;
        public readonly EventType EventTypeType;
        [NotNull] public readonly Key Key;

        public ContainerEvent([NotNull] IContainer container, EventType eventTypeType, [NotNull] Key key)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            EventTypeType = eventTypeType;
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public enum EventType
        {
            Registration,

            Unregistration,
        }
    }
}
