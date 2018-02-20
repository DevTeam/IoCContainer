namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    // ReSharper disable once RedundantUsingDirective
    using System.Runtime.CompilerServices;
    // ReSharper disable once RedundantUsingDirective
    using System.Threading.Tasks;
    using Core;
    using Extensibility;

    /// <summary>
    /// Extension method for IoC container.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [PublicAPI]
    public static class ContainerExtensions
    {
        /// <summary>
        /// Creates child container.
        /// </summary>
        /// <param name="parent">The parent container.</param>
        /// <param name="name">The name of child container.</param>
        /// <returns>The child container.</returns>
        [NotNull]
        public static IContainer CreateChild([NotNull] this IContainer parent, [NotNull] string name = "")
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return parent.GetResolver<IContainer>(typeof(IContainer), WellknownContainers.Child)(parent, name);
        }

        /// <summary>
        /// Binds the type(s).
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="types"></param>
        /// <returns>The binding token.</returns>
        [NotNull]
        public static IBinding<object> Bind([NotNull] this IContainer container, [NotNull][ItemNotNull] params Type[] types)
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
        [NotNull]
        public static IBinding<T> Bind<T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T));
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IContainer container)
            where T : T1
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1));
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2>([NotNull] this IContainer container)
            where T : T1, T2
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
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IContainer container)
            where T : T1, T2, T3
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
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4
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
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5
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
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5, T6
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
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5, T6, T7
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
        }

        /// <summary>
        /// Assigns well-known lifetime to the binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="lifetime"></param>
        /// <returns>The binding token.</returns>
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
        /// <param name="constructorFilter">The constructor's filter.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable To([NotNull] this IBinding<object> binding, [NotNull] Type type, [CanBeNull] Predicate<ConstructorInfo> constructorFilter = null)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new RegistrationToken(binding.Container, CreateRegistration(binding, new FullAutowring(type, constructorFilter)));
        }

        /// <summary>
        /// Creates full auto-wiring.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="constructorFilter">The constructor's filter.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable To<T>([NotNull] this IBinding<T> binding, [CanBeNull] Predicate<ConstructorInfo> constructorFilter = null)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new RegistrationToken(binding.Container, CreateRegistration(binding, new FullAutowring(typeof(T), constructorFilter)));
        }

        /// <summary>
        /// Creates manual auto-wiring.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable To<T>(
            [NotNull] this IBinding<T> binding,
            [NotNull] Expression<Func<Context, T>> factory,
            [NotNull][ItemNotNull] params Expression<Action<Context<T>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return new RegistrationToken(binding.Container, CreateRegistration(binding, new Autowring(factory, statements)));
        }

        /// <summary>
        /// Puts the registration token to the target contaier to manage it.
        /// </summary>
        /// <param name="registrationToken"></param>
        public static void ToSelf([NotNull] this IDisposable registrationToken)
        {
            if (registrationToken == null) throw new ArgumentNullException(nameof(registrationToken));
            if (registrationToken is RegistrationToken token)
            {
                token.Container.Get<IResourceStore>().AddResource(registrationToken);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static T Get<T>([NotNull] this IContainer container, [NotNull][ItemCanBeNull] params object[] args)
        {
            return container.GetResolver<T>(typeof(T))(container, args);
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="type">The instance type.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static object Get([NotNull] this IContainer container, [NotNull] Type type, [NotNull][ItemCanBeNull] params object[] args)
        {
            return container.GetResolver<object>(type)(container, args);
        }

        /// <summary>
        /// Specifies the tag of the instance.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag value.</param>
        /// <returns>The instance.</returns>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Resolving Tag([NotNull] this IContainer container, [CanBeNull] object tag)
        {
            return new Resolving(container, tag);
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="resolving"></param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static T Get<T>(this Resolving resolving, [NotNull][ItemCanBeNull] params object[] args)
        {
            return resolving.Container.GetResolver<T>(typeof(T), resolving.Tag, resolving.Container)(resolving.Container, args);
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <param name="resolving"></param>
        /// <param name="type">The instance type.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static object Get(this Resolving resolving, [NotNull] Type type, [NotNull][ItemCanBeNull] params object[] args)
        {
            return resolving.Container.GetResolver<object>(type, resolving.Tag, resolving.Container)(resolving.Container, args);
        }

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.ApplyData(configurationText);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.ApplyData(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.ApplyData(configurationReaders);
        }

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.UsingData(configurationText);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.UsingData(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.UsingData(configurationReaders);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull][ItemNotNull] IEnumerable<IConfiguration> configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            return Disposable.Create(configurations.Select(i => i.Apply(container)).SelectMany(i => i));
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (configurations.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurations));
            return container.Apply((IEnumerable<IConfiguration>)configurations);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (configurations.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurations));
            container.Get<IResourceStore>().AddResource(container.Apply(configurations));
            return container;
        }

        /// <summary>
        /// Applies configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using<T>([NotNull] this IContainer container)
            where T : IConfiguration, new()
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Using(new T());
        }

        /// <summary>
        /// Validates the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Validate([NotNull] this IContainer container)
        {
            // ReSharper disable once InvokeAsExtensionMethod
            return ValidateUtils.Validate(container);
        }

        [NotNull]
        private static IDisposable ApplyData<T>([NotNull] this IContainer container, [NotNull][ItemNotNull] params T[] configurationData)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationData == null) throw new ArgumentNullException(nameof(configurationData));
            if (configurationData.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationData));
            return container.Apply(configurationData.Select(configurationItem => container.Get<IConfiguration>(configurationItem)).ToArray());
        }

        [NotNull]
        private static IContainer UsingData<T>([NotNull] this IContainer container, [NotNull][ItemNotNull] params T[] configurationData)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationData == null) throw new ArgumentNullException(nameof(configurationData));
            if (configurationData.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationData));
            return container.Using(configurationData.Select(configurationItem => container.Get<IConfiguration>(configurationItem)).ToArray());
        }

        [NotNull]
        private static IDisposable CreateRegistration<T>([NotNull] this IBinding<T> binding, [NotNull] IDependency dependency)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));

            var keys = (
                from contract in binding.Types
                from tag in binding.Tags.DefaultIfEmpty(null)
                select new Key(contract, tag)).Distinct().ToArray();

            if (!binding.Container.TryRegister(keys, dependency, binding.Lifetime, out var registrationToken))
            {
                return binding.Container.GetIssueResolver().CannotRegister(binding.Container, keys);
            }

            return registrationToken;
        }

        [NotNull]
        private static IIssueResolver GetIssueResolver([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<IIssueResolver>();
        }

        /// <summary>
        /// Represents the resolving token.
        /// </summary>
        public struct Resolving
        {
            /// <summary>
            /// The target container.
            /// </summary>
            [NotNull] public readonly IContainer Container;

            /// <summary>
            /// The tag value for resolving.
            /// </summary>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            [CanBeNull] public readonly object Tag;

            internal Resolving([NotNull] IContainer container, [CanBeNull]object tag)
            {
                Container = container;
                Tag = tag;
            }
        }
    }
}
