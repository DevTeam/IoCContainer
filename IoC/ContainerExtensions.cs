namespace IoC
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Core;

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [PublicAPI]
    public static class ContainerExtensions
    {
        [ThreadStatic] private static object _tagValue;

        [NotNull]
        public static IContainer CreateChild([NotNull] this IContainer parent, [NotNull] string name = "")
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return parent.Tag(ContainerReference.Child).Get<IContainer>(name);
        }

        [NotNull]
        public static IRegistration<object> Bind([NotNull] this IContainer container, [NotNull][ItemNotNull] params Type[] types)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (types.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(types));
            return new Registration<object>(container, types);
        }

        [NotNull]
        public static IRegistration<T> Bind<T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T));
        }

        [NotNull]
        public static IRegistration<T> Bind<T, T1>([NotNull] this IContainer container)
            where T : T1
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1));
        }

        [NotNull]
        public static IRegistration<T> Bind<T, T1, T2>([NotNull] this IContainer container)
            where T : T1, T2
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2));
        }

        [NotNull]
        public static IRegistration<T> Bind<T, T1, T2, T3>([NotNull] this IContainer container)
            where T : T1, T2, T3
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3));
        }

        [NotNull]
        public static IRegistration<T> Bind<T, T1, T2, T3, T4>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        [NotNull]
        public static IRegistration<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        [NotNull]
        public static IRegistration<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5, T6
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }

        [NotNull]
        public static IRegistration<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5, T6, T7
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
        }

        [NotNull]
        public static IRegistration<T> Lifetime<T>([NotNull] this IRegistration<T> registration, Lifetime lifetime)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            return new Registration<T>(registration, lifetime);
        }

        [NotNull]
        public static IRegistration<T> Lifetime<T>([NotNull] this IRegistration<T> registration, [NotNull] ILifetime lifetime)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            return new Registration<T>(registration, lifetime);
        }

        [NotNull]
        public static IRegistration<T> Tag<T>([NotNull] this IRegistration<T> registration, [CanBeNull] object tagValue = null)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            return new Registration<T>(registration, tagValue);
        }

        [NotNull]
        public static IRegistration<T> AnyTag<T>([NotNull] this IRegistration<T> registration)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            return registration.Tag(Key.AnyTag);
        }

        [NotNull]
        public static IDisposable To([NotNull] this IRegistration<object> registration, [NotNull] Type type, [CanBeNull] Predicate<ConstructorInfo> constructorFilter = null)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            return new RegistrationToken(registration.Container, CreateRegistration(registration, new FullAutowring(type, constructorFilter)));
        }

        [NotNull]
        public static IDisposable To<T>([NotNull] this IRegistration<T> registration, [CanBeNull] Predicate<ConstructorInfo> constructorFilter = null)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            return new RegistrationToken(registration.Container, CreateRegistration(registration, new FullAutowring(typeof(T), constructorFilter)));
        }

        [NotNull]
        public static IDisposable To<T>(
            [NotNull] this IRegistration<T> registration,
            [NotNull] Expression<Func<Context, T>> factory,
            [NotNull][ItemNotNull] params Expression<Action<Context<T>>>[] statements)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            // ReSharper disable once CoVariantArrayConversion
            return new RegistrationToken(registration.Container, CreateRegistration(registration, new Autowring(factory, statements)));
        }

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

        [NotNull]
        public static IContainer Tag([NotNull] this IContainer container, [CanBeNull] object tag = null)
        {
#if DEBUG
            if (container == null) throw new ArgumentNullException(nameof(container));
#endif
            _tagValue = tag;
            return container;
        }

        [NotNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Get([NotNull] this IContainer container, [NotNull] Type type, [NotNull][ItemCanBeNull] params object[] args)
        {
#if DEBUG
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (args == null) throw new ArgumentNullException(nameof(args));
#endif
            var tag = _tagValue;
            _tagValue = null;
            if (container.TryGet(type, tag, out var instance, args))
            {
                return instance;
            }

            return container.GetIssueResolver().CannotResolve(container, new Key(type, tag));
        }

        [NotNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Get([NotNull] this Container container, [NotNull] Type type, [NotNull][ItemCanBeNull] params object[] args)
        {
#if DEBUG
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (args == null) throw new ArgumentNullException(nameof(args));
#endif
            var tag = _tagValue;
            _tagValue = null;
            if (container.TryGet(type, tag, out var instance, args))
            {
                return instance;
            }

            return container.GetIssueResolver().CannotResolve(container, new Key(type, tag));
        }

        [NotNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>([NotNull] this IContainer container, [NotNull][ItemCanBeNull] params object[] args)
        {
#if DEBUG
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));
#endif
            var tag = _tagValue;
            _tagValue = null;
            if (container.TryGet<T>(tag, out var instance, args))
            {
                return instance;
            }

            return (T)container.GetIssueResolver().CannotResolve(container, Key.Create<T>(tag));
        }

        [NotNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>([NotNull] this Container container, [NotNull][ItemCanBeNull] params object[] args)
        {
#if DEBUG
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));
#endif
            var tag = _tagValue;
            _tagValue = null;
            if (container.TryGet<T>(tag, out var instance, args))
            {
                return instance;
            }

            return (T)container.GetIssueResolver().CannotResolve(container, Key.Create<T>(tag));
        }

        [NotNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T> FuncGet<T>([NotNull] this IContainer container)
        {
#if DEBUG
            if (container == null) throw new ArgumentNullException(nameof(container));
#endif
            return container.Get<Func<T>>();
        }

        [NotNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T> FuncGet<T1, T>([NotNull] this IContainer container)
        {
#if DEBUG
            if (container == null) throw new ArgumentNullException(nameof(container));
#endif
            return container.Get<Func<T1, T>>();
        }

        [NotNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T2, T> FuncGet<T1, T2, T>([NotNull] this IContainer container)
        {
#if DEBUG
            if (container == null) throw new ArgumentNullException(nameof(container));
#endif
            return container.Get<Func<T1, T2, T>>();
        }

        [NotNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T2, T3, T> FuncGet<T1, T2, T3, T>([NotNull] this IContainer container)
        {
#if DEBUG
            if (container == null) throw new ArgumentNullException(nameof(container));
#endif
            return container.Get<Func<T1, T2, T3, T>>();
        }

        [NotNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T2, T3, T4, T> FuncGet<T1, T2, T3, T4, T>([NotNull] this IContainer container)
        {
#if DEBUG
            if (container == null) throw new ArgumentNullException(nameof(container));
#endif
            return container.Get<Func<T1, T2, T3, T4, T>>();
        }

#if !NET40
        [NotNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> AsyncGet<T>([NotNull] this IContainer container, [CanBeNull] TaskScheduler taskScheduler = null)
        {
#if DEBUG
            if (container == null) throw new ArgumentNullException(nameof(container));
#endif
            var task = container.Get<Task<T>>();
            if (taskScheduler != null)
            {
                task.Start(taskScheduler);
            }

            return await task;
        }
#endif

        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.ApplyData(configurationText);
        }

        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.ApplyData(configurationStreams);
        }

        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.ApplyData(configurationReaders);
        }

        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.UsingData(configurationText);
        }

        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.UsingData(configurationStreams);
        }

        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.UsingData(configurationReaders);
        }

        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (configurations.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurations));
            return Disposable.Create(configurations.Select(i => i.Apply(container)).SelectMany(i => i));
        }

        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (configurations.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurations));
            container.Get<IResourceStore>().AddResource(container.Apply(configurations));
            return container;
        }

        [NotNull]
        public static IContainer Using<T>([NotNull] this IContainer container)
            where T : IConfiguration, new()
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Using(new T());
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
        private static IDisposable CreateRegistration<T>([NotNull] this IRegistration<T> registration, [NotNull] IDependency dependency)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));

            var keys = (
                from contract in registration.Types
                from tag in registration.Tags.DefaultIfEmpty(null)
                select new Key(contract, tag)).Distinct().ToArray();

            if (!registration.Container.TryRegister(keys, dependency, registration.Lifetime, out var registrationToken))
            {
                return registration.Container.GetIssueResolver().CannotRegister(registration.Container, keys);
            }

            return registrationToken;
        }

        [NotNull]
        private static IIssueResolver GetIssueResolver([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<IIssueResolver>();
        }
    }
}
