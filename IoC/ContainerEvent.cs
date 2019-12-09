namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

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
        public readonly EventType EventType;

        /// <summary>
        /// True if it is success.
        /// </summary>
        public bool IsSuccess;

        /// <summary>
        /// Error during operation.
        /// </summary>
        public Exception Error;

        /// <summary>
        /// The changed keys.
        /// </summary>
        [CanBeNull] public IEnumerable<Key> Keys;

        /// <summary>
        /// Related dependency.
        /// </summary>
        [CanBeNull] public IDependency Dependency;

        /// <summary>
        /// Related lifetime.
        /// </summary>
        [CanBeNull] public ILifetime Lifetime;

        /// <summary>
        /// Related lifetime.
        /// </summary>
        [CanBeNull] public LambdaExpression ResolverExpression;

        /// <summary>
        /// Create new instance of container event.
        /// </summary>
        /// <param name="container">The origin container.</param>
        /// <param name="eventType">The event type.</param>
        public ContainerEvent([NotNull] IContainer container, EventType eventType)
        {
            Container = container;
            EventType = eventType;
            IsSuccess = true;
            Error = default(Exception);
            Keys = default(IEnumerable<Key>);
            Dependency = default(IDependency);
            Lifetime = default(ILifetime);
            ResolverExpression = default(LambdaExpression);
        }
    }
}
