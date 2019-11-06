namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents extensions to add bindings to the container.
    /// </summary>
    internal static partial class FluentRegister
    {
        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The registration token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T: T1
            => container.Register(new[] { TypeDescriptor<T1>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T: T1
            // ReSharper disable once CoVariantArrayConversion
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type }, new AutowiringDependency(factory, null, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The registration token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1, T2>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T: T1, T2
            => container.Register(new[] { TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1, T2>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T: T1, T2
            // ReSharper disable once CoVariantArrayConversion
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type }, new AutowiringDependency(factory, null, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The registration token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1, T2, T3>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T: T1, T2, T3
            => container.Register(new[] { TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1, T2, T3>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T: T1, T2, T3
            // ReSharper disable once CoVariantArrayConversion
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type }, new AutowiringDependency(factory, null, statements), lifetime, tags);

    }
}