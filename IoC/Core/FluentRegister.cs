namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using Issues;

    /// <summary>
    /// Represents extensions to register a dependency in the container.
    /// </summary>
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    [PublicAPI]
    internal static partial class FluentRegister
    {
        private static readonly IEnumerable<object> DefaultTags = new object[] { null };

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The registration token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [IoC.NotNull]
        public static IToken Register<T>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null) 
            => container.Register(new[] { TypeDescriptor<T>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The registration token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [IoC.NotNull]
        public static IToken Register<T>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [IoC.NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            => container.Register(new[] { TypeDescriptor<T>.Type }, new AutowiringDependency(factory, null, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="types">The set of types.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The registration token.</returns>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        [IoC.NotNull]
        public static IToken Register([NotNull] this IContainer container, [NotNull][ItemNotNull] IEnumerable<Type> types, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime = null, [CanBeNull][ItemCanBeNull] params object[] tags)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var keys =
                from type in types
                from tag in tags ?? DefaultTags
                select new Key(type, tag);

            return container.TryRegisterDependency(keys, dependency, lifetime, out var dependencyToken) 
                ? dependencyToken
                : container.Resolve<ICannotRegister>().Resolve(container, keys.ToArray());
        }
    }
}