namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Linq.Expressions;
    using Core;
    using Issues;

    /// <summary>
    /// Represents extensions to add bindings to the container.
    /// </summary>
    [PublicAPI]
    public static partial class FluentBind
    {
        /// <summary>
        /// Binds types.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="types"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<object> Bind([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params Type[] types)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (types.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(types));
            return new Binding<object>(container, types);
        }

        /// <summary>
        /// Binds types.
        /// </summary>
        /// <param name="token">The container binding token.</param>
        /// <param name="types"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<object> Bind([NotNull] this IToken token, [NotNull] [ItemNotNull] params Type[] types)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (types.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(types));
            return new Binding<object>(token, types);
        }

        /// <summary>
        /// Binds the type.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T>([NotNull] this IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T));
        }

        /// <summary>
        /// Binds the type.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="token">The container binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T>([NotNull] this IToken token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T));
        }

        /// <summary>
        /// Binds the type.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="binding"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T>([NotNull] this IBinding binding)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T));
        }

        /// <summary>
        /// Assigns well-known lifetime to the binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="lifetime"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> As<T>([NotNull] this IBinding<T> binding, Lifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, lifetime);
        }

        /// <summary>
        /// Assigns the lifetime to the binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="lifetime"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Lifetime<T>([NotNull] this IBinding<T> binding, [NotNull] ILifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            return new Binding<T>(binding, lifetime);
        }

        /// <summary>
        /// Assigns the autowiring strategy to the binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="autowiringStrategy"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Autowiring<T>([NotNull] this IBinding<T> binding, [NotNull] IAutowiringStrategy autowiringStrategy)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            if (autowiringStrategy == null) throw new ArgumentNullException(nameof(autowiringStrategy));
            return new Binding<T>(binding, autowiringStrategy);
        }

        /// <summary>
        /// Marks the binding by the tag. Is it possible to use multiple times.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="tagValue"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Tag<T>([NotNull] this IBinding<T> binding, [CanBeNull] object tagValue = null)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, tagValue);
        }

        /// <summary>
        /// Marks the binding to be used for any tags.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> AnyTag<T>([NotNull] this IBinding<T> binding)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return binding.Tag(Key.AnyTag);
        }

        /// <summary>
        /// Registers autowiring binding.
        /// </summary>
        /// <param name="binding">The binding token.</param>
        /// <param name="type">The instance type.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken To(
            [NotNull] this IBinding<object> binding,
            [NotNull] Type type,
            [NotNull][ItemNotNull] params Expression<Action<Context<object>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return binding.To(new FullAutowiringDependency(type, binding.AutowiringStrategy, statements));
        }

        /// <summary>
        /// Registers autowiring binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken To<T>(
            [NotNull] this IBinding<T> binding,
            [NotNull][ItemNotNull] params Expression<Action<Context<T>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return binding.To(new FullAutowiringDependency(typeof(T), binding.AutowiringStrategy, statements));
        }

        /// <summary>
        /// Registers autowiring binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken To<T>(
            [NotNull] this IBinding<T> binding,
            [NotNull] Expression<Func<Context, T>> factory,
            [NotNull][ItemNotNull] params Expression<Action<Context<T>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return binding.To(new AutowiringDependency(factory, binding.AutowiringStrategy, statements));
        }

        /// <summary>
        /// Registers autowiring binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="dependency">The dependency.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken To<T>(
            [NotNull] this IBinding<T> binding,
            [NotNull] IDependency dependency)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var tags = binding.Tags.DefaultIfEmpty(null);
            var keys = (
                from type in binding.Types
                from tag in tags
                select new Key(type, tag))
                .ToArray();

            var token = (binding.Container.TryRegisterDependency(keys, dependency, binding.Lifetime, out var dependencyToken)
                ? dependencyToken
                : binding.Container.Resolve<ICannotRegister>().Resolve(binding.Container, keys, dependency, binding.Lifetime)).AsTokenOf(binding.Container);

            var tokens = new List<IToken>(binding.Tokens) { token };
            return Disposable.Create(tokens).AsTokenOf(binding.Container);
        }
    }
}