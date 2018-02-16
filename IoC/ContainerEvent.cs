namespace IoC
{
    using System;

    [PublicAPI]
    public struct ContainerEvent
    {
        [NotNull] public readonly IContainer Container;
        public readonly EventType EventTypeType;
        public readonly Key Key;

        public ContainerEvent([NotNull] IContainer container, EventType eventTypeType, Key key)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            EventTypeType = eventTypeType;
            Key = key;
        }

        public enum EventType
        {
            Registration,

            Unregistration,
        }
    }
}
