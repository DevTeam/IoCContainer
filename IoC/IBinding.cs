namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The an abstract containers binding.
    /// </summary>
    [PublicAPI]
    // ReSharper disable once UnusedTypeParameter
    public interface IBinding
    {
        /// <summary>
        /// The target container to configure.
        /// </summary>
        [NotNull] IMutableContainer Container { get; }

        /// <summary>
        /// Binding tokens.
        /// </summary>
        [NotNull] IEnumerable<IToken> Tokens { get; }

        /// <summary>
        /// The contract type to bind.
        /// </summary>
        [NotNull] [ItemNotNull] IEnumerable<Type> Types { get; }

        /// <summary>
        /// The tags to mark this binding.
        /// </summary>
        [NotNull] [ItemCanBeNull] IEnumerable<object> Tags { get; }

        /// <summary>
        /// The lifetime instance or null by default.
        /// </summary>
        [CanBeNull] ILifetime Lifetime { get; }

        /// <summary>
        /// The autowiring strategy or null by default.
        /// </summary>
        [CanBeNull] IAutowiringStrategy AutowiringStrategy { get; }
    }

    /// <summary>
    /// The containers binding.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    // ReSharper disable once UnusedTypeParameter
    public interface IBinding<in T> : IBinding { }
}
