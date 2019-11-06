namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The container's binding.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    // ReSharper disable once UnusedTypeParameter
    public interface IBinding<in T>
    {
        /// <summary>
        /// The target container.
        /// </summary>
        [NotNull] IContainer Container { get; }

        /// <summary>
        /// Binding tokens.
        /// </summary>
        [NotNull] IEnumerable<IToken> Tokens { get; }

        /// <summary>
        /// The type to bind.
        /// </summary>
        [NotNull][ItemNotNull] IEnumerable<Type> Types { get; }

        /// <summary>
        /// The tags to mark the binding.
        /// </summary>
        [NotNull][ItemCanBeNull] IEnumerable<object> Tags { get; }

        /// <summary>
        /// The specified lifetime instance or null.
        /// </summary>
        [CanBeNull] ILifetime Lifetime { get; }

        /// <summary>
        /// The specified autowiring strategy or null.
        /// </summary>
        [CanBeNull] IAutowiringStrategy AutowiringStrategy { get; }
    }
}
