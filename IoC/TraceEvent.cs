namespace IoC
{
    /// <summary>
    /// Represents a trace event.
    /// </summary>
    [PublicAPI]
    public struct TraceEvent
    {
        /// <summary>
        /// The origin container event.
        /// </summary>
        public readonly ContainerEvent ContainerEvent;

        /// <summary>
        /// The trace message.
        /// </summary>
        [NotNull] public readonly string Message;

        /// <summary>
        /// Creates new instance of trace event.
        /// </summary>
        /// <param name="containerEvent">The original instance of container event.</param>
        /// <param name="message">The trace message.</param>
        internal TraceEvent(ContainerEvent containerEvent, [NotNull] string message)
        {
            ContainerEvent = containerEvent;
            Message = message;
        }
    }
}