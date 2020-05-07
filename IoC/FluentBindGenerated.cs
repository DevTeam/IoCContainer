namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Represents extensions to add bindings to the container.
    /// </summary>
    public static partial class FluentBind
    {
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IMutableContainer container)
            where T: T1
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1));
        }

        
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IBinding binding)
            where T: T1
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1));
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IToken token)
            where T: T1
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1));
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2>([NotNull] this IMutableContainer container)
            where T: T1, T2
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2));
        }

        
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2>([NotNull] this IBinding binding)
            where T: T1, T2
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2));
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2>([NotNull] this IToken token)
            where T: T1, T2
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3));
        }

        
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IBinding binding)
            where T: T1, T2, T3
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3));
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IToken token)
            where T: T1, T2, T3
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IToken token)
            where T: T1, T2, T3, T4
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
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
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
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
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
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
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <typeparam name="T31">The contract type #31.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <typeparam name="T31">The contract type #31.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <typeparam name="T31">The contract type #31.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <typeparam name="T31">The contract type #31.</typeparam>
        /// <typeparam name="T32">The contract type #32.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31), typeof(T32));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <typeparam name="T31">The contract type #31.</typeparam>
        /// <typeparam name="T32">The contract type #32.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31), typeof(T32));
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
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <typeparam name="T31">The contract type #31.</typeparam>
        /// <typeparam name="T32">The contract type #32.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31), typeof(T32));
        }
    }
}