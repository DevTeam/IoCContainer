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
        public readonly bool IsSuccess;

        /// <summary>
        /// Error during operation.
        /// </summary>
        [CanBeNull] public readonly Exception Error;

        /// <summary>
        /// The changed keys.
        /// </summary>
        [CanBeNull] public readonly IEnumerable<Key> Keys;

        /// <summary>
        /// Related dependency.
        /// </summary>
        [CanBeNull] public readonly IDependency Dependency;

        /// <summary>
        /// Related lifetime.
        /// </summary>
        [CanBeNull] public readonly ILifetime Lifetime;

        /// <summary>
        /// Related lifetime.
        /// </summary>
        [CanBeNull] public readonly LambdaExpression ResolverExpression;

        internal ContainerEvent(
            [NotNull] IContainer container,
            EventType eventType,
            bool isSuccess,
            [CanBeNull] Exception error,
            [CanBeNull] IEnumerable<Key> keys,
            [CanBeNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [CanBeNull] LambdaExpression resolverExpression)
        {
            Container = container;
            EventType = eventType;
            IsSuccess = isSuccess;
            Error = error;
            Keys = keys;
            Dependency = dependency;
            Lifetime = lifetime;
            ResolverExpression = resolverExpression;
        }

        internal static ContainerEvent NewContainer(
            [NotNull] IContainer newContainer)
        {
            return new ContainerEvent(
                newContainer,
                EventType.CreateContainer,
                true,
                default(Exception),
                default(IEnumerable<Key>),
                default(IDependency),
                default(ILifetime),
                default(LambdaExpression));
        }

        internal static ContainerEvent DisposeContainer(
            [NotNull] IContainer disposingContainer)
        {
            return new ContainerEvent(
                disposingContainer,
                EventType.DisposeContainer,
                true,
                default(Exception),
                default(IEnumerable<Key>),
                default(IDependency),
                default(ILifetime),
                default(LambdaExpression));
        }

        internal static ContainerEvent RegisterDependency(
            [NotNull] IContainer registeringContainer,
            [NotNull] IEnumerable<Key> keys,
            [CanBeNull] IDependency dependency = default(IDependency),
            [CanBeNull] ILifetime lifetime = default(ILifetime))
        {
            return new ContainerEvent(
                registeringContainer,
                EventType.RegisterDependency,
                true,
                default(Exception),
                keys,
                dependency,
                lifetime,
                default(LambdaExpression));
        }

        internal static ContainerEvent RegisterDependencyFailed(
            [NotNull] IContainer registeringContainer,
            [NotNull] IEnumerable<Key> keys,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] Exception error)
        {
            return new ContainerEvent(
                registeringContainer,
                EventType.RegisterDependency,
                false,
                error,
                keys,
                dependency,
                lifetime,
                default(LambdaExpression));
        }

        internal static ContainerEvent UnregisterDependency(
            [NotNull] IContainer registeringContainer,
            [NotNull] IEnumerable<Key> keys,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime)
        {
            return new ContainerEvent(
                registeringContainer,
                EventType.ContainerStateSingletonLifetime,
                true,
                default(Exception),
                keys,
                dependency,
                lifetime,
                default(LambdaExpression));
        }

        internal static ContainerEvent Compilation(
            [NotNull] IContainer registeringContainer,
            [NotNull] IEnumerable<Key> keys,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] LambdaExpression resolverExpression)
        {
            return new ContainerEvent(
                registeringContainer,
                EventType.ResolverCompilation,
                true,
                default(Exception),
                keys,
                dependency,
                lifetime,
                resolverExpression);
        }

        internal static ContainerEvent CompilationFailed(
            [NotNull] IContainer registeringContainer,
            [NotNull] IEnumerable<Key> keys,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] LambdaExpression resolverExpression,
            [NotNull] Exception error)
        {
            return new ContainerEvent(
                registeringContainer,
                EventType.ResolverCompilation,
                false,
                error,
                keys,
                dependency,
                lifetime,
                resolverExpression);
        }
    }
}
