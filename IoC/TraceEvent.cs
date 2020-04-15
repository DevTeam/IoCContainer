namespace IoC
{
    /// <summary>
    /// Represents a container trace event.
    /// </summary>
    [PublicAPI]
    public struct TraceEvent
    {
        /// <summary>
        /// The original container event.
        /// </summary>
        public readonly ContainerEvent ContainerEvent;

        /// <summary>
        /// The trace message.
        /// </summary>
        [NotNull] public readonly string Message;

        /// <summary>
        /// Creates new instance of a trace event.
        /// </summary>
        /// <param name="containerEvent">The original container event.</param>
        /// <param name="message">The trace message.</param>
        internal TraceEvent(ContainerEvent containerEvent, [NotNull] string message)
        {
            ContainerEvent = containerEvent;
            Message = message;
        }
    }
}