namespace IoC
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Linq.Expressions;
    using Core;

    /// <summary>
    /// Represents extensions to add bindings to the container.
    /// </summary>
    [PublicAPI]
    public static class FluentBind
    {
        /// <summary>
        /// Binds the type(s).
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="types"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<object> Bind([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Type[] types)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (types.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(types));
            return new Binding<object>(container, types);
        }

        /// <summary>
        /// Binds the type.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IContainer container)
            where T : T1
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2>([NotNull] this IContainer container)
            where T : T1, T2
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IContainer container)
            where T : T1, T2, T3
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5, T6
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5, T6, T7
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type);
        }


        /// <summary>
        /// Assigns well-known lifetime to the binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="lifetime"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Lifetime<T>([NotNull] this IBinding<T> binding, [NotNull] ILifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            return new Binding<T>(binding, lifetime);
        }

        /// <summary>
        /// Marks the binding by the tag. Is it possible to use multiple times.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="tagValue"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> AnyTag<T>([NotNull] this IBinding<T> binding)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return binding.Tag(Key.AnyTag);
        }

        /// <summary>
        /// Creates full auto-wiring.
        /// </summary>
        /// <param name="binding">The binding token.</param>
        /// <param name="type">The instance type.</param>
        /// <param name="autoWiringStrategy">The optional auto-wiring strategy.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable To([NotNull] this IBinding<object> binding, [NotNull] Type type, [CanBeNull] IAutowiringStrategy autoWiringStrategy = null)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new DependencyToken(binding.Container, CreateDependency(binding, new FullAutoWiringDependency(type, autoWiringStrategy)));
        }

        /// <summary>
        /// Creates full auto-wiring.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="autoWiringStrategy">The optional auto-wiring strategy.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable To<T>([NotNull] this IBinding<T> binding, [CanBeNull] IAutowiringStrategy autoWiringStrategy = null)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new DependencyToken(binding.Container, CreateDependency(binding, new FullAutoWiringDependency(TypeDescriptor<T>.Type, autoWiringStrategy)));
        }

        /// <summary>
        /// Creates manual auto-wiring.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable To<T>(
            [NotNull] this IBinding<T> binding,
            [NotNull] Expression<Func<Context, T>> factory,
            [NotNull][ItemNotNull] params Expression<Action<Context<T>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return new DependencyToken(binding.Container, CreateDependency(binding, new AutoWiringDependency(factory, statements)));
        }

        /// <summary>
        /// Puts the dependency token into the target container to manage it.
        /// </summary>
        /// <param name="dependencyToken"></param>
        [MethodImpl((MethodImplOptions)256)]
        public static IContainer ToSelf([NotNull] this IDisposable dependencyToken)
        {
            if (dependencyToken == null) throw new ArgumentNullException(nameof(dependencyToken));
            if (dependencyToken is DependencyToken token)
            {
                token.Container.RegisterResource(dependencyToken);
                return token.Container;
            }

            throw new NotSupportedException();
        }

        [NotNull]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private static IDisposable CreateDependency<T>([NotNull] this IBinding<T> binding, [NotNull] IDependency dependency)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));

            var tags = binding.Tags.DefaultIfEmpty(null);
            var keys =
                from type in binding.Types
                from tag in tags
                select new Key(type, tag);

            return binding.Container.TryRegisterDependency(keys, dependency, binding.Lifetime, out var dependencyToken)
                ? dependencyToken
                : binding.Container.GetIssueResolver().CannotRegister(binding.Container, keys.ToArray());
        }
    }
}