namespace IoC
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    // ReSharper disable once RedundantUsingDirective
    using System.Reflection;
    using System.Threading;
    // ReSharper disable once RedundantUsingDirective
    using System.Threading.Tasks;
    using Features;
    using Internal;
    using Internal.Factories;
    using Internal.Features;

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class Container
    {
        private const string RootName = "container://";
        private static long _containerId;

        [NotNull]
        public static IContainer Create([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            var rootContainer = new ChildContainer(
                RootName,
                CoreFeature.Shared,
                EnumerableFeature.Shared,
                FuncFeature.Shared,
                TaskFeature.Shared);

            return new ChildContainer($"{RootName}{CreateContainerName(name)}", rootContainer, true);
        }

        [NotNull]
        public static IContainer CreatePure([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            var rootContainer = new ChildContainer(
                RootName,
                CoreFeature.Shared);

            return new ChildContainer($"{RootName}{CreateContainerName(name)}", rootContainer, true);
        }

        [NotNull]
        public static IContainer CreateChild([NotNull] this IContainer parent, [NotNull] string name = "")
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return new ChildContainer($"{parent}/{CreateContainerName(name)}", parent, false);
        }

        public static IRegistration<object> Bind([NotNull] this IContainer container, [NotNull][ItemNotNull] params Type[] contractTypes)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (contractTypes == null) throw new ArgumentNullException(nameof(contractTypes));
            if (contractTypes.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(contractTypes));
            return new Registration<object>(container, contractTypes);
        }

        public static IRegistration<T> Bind<T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T));
        }

        public static IRegistration<T> Bind<T, T1>([NotNull] this IContainer container)
            where T: T1
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1));
        }

        public static IRegistration<T> Bind<T, T1, T2>([NotNull] this IContainer container)
            where T : T1, T2
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2));
        }

        public static IRegistration<T> Bind<T, T1, T2, T3>([NotNull] this IContainer container)
            where T : T1, T2, T3
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3));
        }

        public static IRegistration<T> Bind<T, T1, T2, T3, T4>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public static IRegistration<T> Lifetime<T>([NotNull] this IRegistration<T> registration, Lifetime lifetime)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            return new Registration<T>(registration, lifetime);
        }

        public static IRegistration<T> Lifetime<T>([NotNull] this IRegistration<T> registration, [NotNull] ILifetime lifetime)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            return new Registration<T>(registration, lifetime);
        }

        public static IRegistration<T> Tag<T>([NotNull] this IRegistration<T> registration, [CanBeNull] object tagValue)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            return new Registration<T>(registration, tagValue);
        }

        public static IRegistration<T> AnyTag<T>([NotNull] this IRegistration<T> registration)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            return registration.Tag(null).Tag(Key.AnyTag);
        }

        public static IDisposable To<T>([NotNull] this IRegistration<T> registration, [NotNull] IFactory factory)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return new RegistrationToken(registration.Container, CreateRegistration(registration, factory));
        }

        public static IDisposable To<T>([NotNull] this IRegistration<T> registration, [NotNull] Func<IoC.ResolvingContext, T> factory, [NotNull] string description = "")
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (description == null) throw new ArgumentNullException(nameof(description));
            return registration.To(new FuncFactory<T>(description, factory));
        }

        public static IDisposable To<T>([NotNull] this IRegistration<T> registration, [NotNull] Func<T> factory, [NotNull] string description = "")
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (description == null) throw new ArgumentNullException(nameof(description));
            return registration.To(new FuncFactory<T>(description, ctx => factory()));
        }

        public static IDisposable To<T>([NotNull] this IRegistration<T> registration, [NotNull] Type instanceType, [NotNull] params Has[] dependencies)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            if (instanceType == null) throw new ArgumentNullException(nameof(instanceType));
            if (dependencies == null) throw new ArgumentNullException(nameof(dependencies));
            var typeInfo = instanceType.AsTypeInfo();
            if (typeInfo.IsAbstract || typeInfo.IsInterface)
            {
                return registration.To(registration.Container.GetIssueResolver().CannotBeCeated(instanceType));
            }

            var factory = registration.Container.Get<IFactory>(instanceType, dependencies);
            return registration.To(factory);
        }

        public static IDisposable To<T>([NotNull] this IRegistration<T> registration, [NotNull] params Has[] dependencies)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            if (dependencies == null) throw new ArgumentNullException(nameof(dependencies));
            return registration.To(typeof(T), dependencies);
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

        public static IContainer Tag([NotNull] this IContainer container, [NotNull] object tag)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            ResolvingContext.TagValue = tag ?? throw new ArgumentNullException(nameof(tag));
            return container;
        }

        public static bool TryGet([NotNull] this IContainer container, [NotNull] Type targetContractType, out object instance, [NotNull][ItemCanBeNull] params object[] args)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (targetContractType == null) throw new ArgumentNullException(nameof(targetContractType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            var resolvingKey = new Key(targetContractType, ResolvingContext.TagValue);
            ResolvingContext.TagValue = null;
            if (!container.TryGetResolver(resolvingKey, out var resolver))
            {
                instance = null;
                return false;
            }

            instance = resolver.Resolve(resolvingKey, container, 0, args);
            return true;
        }

        public static bool TryGet<T>([NotNull] this IContainer container, out T instance, [NotNull][ItemCanBeNull] params object[] args)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));
            var resolvingKey = new Key(typeof(T), ResolvingContext.TagValue);
            ResolvingContext.TagValue = null;
            if (!container.TryGetResolver(resolvingKey, out var resolver))
            {
                instance = default(T);
                return false;
            }

            instance = (T)resolver.Resolve(resolvingKey, container, 0, args);
            return true;
        }

        [NotNull]
        public static object Get([NotNull] this IContainer container, [NotNull] Type targetContractType, [NotNull][ItemCanBeNull] params object[] args)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (targetContractType == null) throw new ArgumentNullException(nameof(targetContractType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            var resolvingKey = new Key(targetContractType, ResolvingContext.TagValue);
            ResolvingContext.TagValue = null;
            if (!container.TryGetResolver(resolvingKey, out var resolver))
            {
                return container.GetIssueResolver().CannotResolve(container, resolvingKey);
            }

            return resolver.Resolve(resolvingKey, container, 0, args);
        }

        [NotNull]
        public static T Get<T>([NotNull] this IContainer container, [NotNull][ItemCanBeNull] params object[] args)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));
            var resolvingKey = new Key(typeof(T), ResolvingContext.TagValue);
            ResolvingContext.TagValue = null;
            if (!container.TryGetResolver(resolvingKey, out var resolver))
            {
                return (T)container.GetIssueResolver().CannotResolve(container, resolvingKey);
            }

            return (T)resolver.Resolve(resolvingKey, container, 0, args);
        }

        [NotNull]
        public static Func<T> FuncGet<T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<Func<T>>();
        }

        [NotNull]
        public static Func<T1, T> FuncGet<T1, T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<Func<T1, T>>();
        }

        [NotNull]
        public static Func<T1, T2, T> FuncGet<T1, T2, T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<Func<T1, T2, T>>();
        }

        [NotNull]
        public static Func<T1, T2, T3, T> FuncGet<T1, T2, T3, T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<Func<T1, T2, T3, T>>();
        }

        [NotNull]
        public static Func<T1, T2, T3, T4, T> FuncGet<T1, T2, T3, T4, T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<Func<T1, T2, T3, T4, T>>();
        }

#if !NET40
        [NotNull]
        public static async Task<T> AsyncGet<T>([NotNull] this IContainer container, [CanBeNull] TaskScheduler taskScheduler = null)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var task = container.Get<Task<T>>();
            if (taskScheduler != null)
            {
                task.Start(taskScheduler);
            }

            return await task;
        }
#endif

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
            where T: IConfiguration, new()
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Using(new T());
        }

        [NotNull]
        private static IDisposable CreateRegistration<T>([NotNull] this IRegistration<T> registration, [NotNull] IFactory factory)
        {
            var keys = (
                from contract in registration.ContractsTypes
                from tag in registration.Tags.DefaultIfEmpty(null)
                select new Key(contract, tag)).Distinct().ToArray();

            if (!registration.Container.TryRegister(keys, factory, registration.Lifetime, out var registrationToken))
            {
                return registration.Container.GetIssueResolver().CannotRegister(registration.Container, keys);
            }

            return registrationToken;
        }

        [NotNull]
        private static string CreateContainerName([NotNull] string name)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return !string.IsNullOrWhiteSpace(name) ? name : Interlocked.Increment(ref _containerId).ToString(CultureInfo.InvariantCulture);
        }

        private static IIssueResolver GetIssueResolver(this IContainer container)
        {
            return container.Get<IIssueResolver>();
        }

        private static class ResolvingContext
        {
            [ThreadStatic]
            public static object TagValue;
        }
    }
}
