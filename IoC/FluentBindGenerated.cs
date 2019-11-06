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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IContainer container)
            where T: T1
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IToken token)
            where T: T1
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type);
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
            where T: T1, T2
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
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2>([NotNull] this IToken token)
            where T: T1, T2
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type);
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
            where T: T1, T2, T3
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
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IToken token)
            where T: T1, T2, T3
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type);
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
            where T: T1, T2, T3, T4
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
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IToken token)
            where T: T1, T2, T3, T4
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type);
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
            where T: T1, T2, T3, T4, T5
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
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type);
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
            where T: T1, T2, T3, T4, T5, T6
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
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type);
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
            where T: T1, T2, T3, T4, T5, T6, T7
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type, TypeDescriptor<T31>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type, TypeDescriptor<T31>.Type);
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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type, TypeDescriptor<T31>.Type, TypeDescriptor<T32>.Type);
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
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type, TypeDescriptor<T31>.Type, TypeDescriptor<T32>.Type);
        }
    }
}