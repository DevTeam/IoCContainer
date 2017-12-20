namespace IoC
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Impl;

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class Container
    {
        private static long _containerId;

        [NotNull]
        public static IContainer Create([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            var rootContainer = new ChildContainer();
            return new ChildContainer($"container://{CreateContainerName(name)}", rootContainer, true);
        }

        [NotNull]
        public static IContainer CreateChild([NotNull] this IContainer parent, [NotNull] string name = "")
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return new ChildContainer($"{parent}/{CreateContainerName(name)}", parent, false);
        }

        public static Registration<object> Map([NotNull] this IContainer container, [NotNull] Type instanceType)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (instanceType == null) throw new ArgumentNullException(nameof(instanceType));
            return new Registration<object>(container, instanceType);
        }

        [NotNull]
        public static IDisposable Autowiring([NotNull] this IContainer container, [NotNull] Type instanceType, [NotNull] Type contractType, [CanBeNull] object tagValue = null)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (contractType == null) throw new ArgumentNullException(nameof(contractType));
            if (instanceType == null) throw new ArgumentNullException(nameof(instanceType));
            if (tagValue == null)
            {
                return new Registration<object>(container, contractType).To(instanceType);
            }

            return new Registration<object>(container, contractType).Tag(tagValue).To(instanceType);
        }

        public static Registration<T> Map<T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T));
        }

        [NotNull]
        public static IDisposable Autowiring<TT, T1>([NotNull] this IContainer container, [CanBeNull] object tagValue = null)
            where TT: T1
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (tagValue == null)
            {
                return new Registration<T1>(container, typeof(T1)).To(typeof(TT));
            }

            return new Registration<T1>(container, typeof(T1)).Tag(tagValue).To(typeof(TT));
        }


        [NotNull]
        public static IDisposable Autowiring<TT, T1, T2>([NotNull] this IContainer container, [CanBeNull] object tagValue = null)
            where TT : T1, T2
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (tagValue == null)
            {
                return new Registration<T1>(container, typeof(T1), typeof(T2)).To(typeof(TT));
            }

            return new Registration<T1>(container, typeof(T1), typeof(T2)).Tag(tagValue).To(typeof(TT));
        }

        [NotNull]
        public static IDisposable Autowiring<TT, T1, T2, T3>([NotNull] this IContainer container, [CanBeNull] object tagValue = null)
            where TT : T1, T2, T3
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (tagValue == null)
            {
                return new Registration<T1>(container, typeof(T1), typeof(T2), typeof(T3)).To(typeof(TT));
            }

            return new Registration<T1>(container, typeof(T1), typeof(T2), typeof(T3)).Tag(tagValue).To(typeof(TT));
        }

        [NotNull]
        public static IDisposable Autowiring<TT, T1, T2, T3, T4>([NotNull] this IContainer container, [CanBeNull] object tagValue = null)
            where TT : T1, T2, T4
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (tagValue == null)
            {
                return new Registration<T1>(container, typeof(T1), typeof(T2), typeof(T3), typeof(T4)).To(typeof(TT));
            }

            return new Registration<T1>(container, typeof(T1), typeof(T2), typeof(T3), typeof(T4)).Tag(tagValue).To(typeof(TT));
        }

        public static Registration<T> Map<T, T1>([NotNull] this IContainer container)
            where T: T1
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1));
        }

        public static Registration<T> Map<T, T1, T2>([NotNull] this IContainer container)
            where T : T1, T2
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2));
        }

        public static Registration<T> Map<T, T1, T2, T3>([NotNull] this IContainer container)
            where T : T1, T2, T3
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3));
        }

        public static Registration<T> Map<T, T1, T2, T3, T4>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Registration<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public static Registration<T> Lifetime<T>(this Registration<T> registration, Lifetime lifetime)
        {
            return new Registration<T>(registration, lifetime);
        }

        public static Registration<T> Lifetime<T>(this Registration<T> registration, [NotNull] ILifetime lifetime)
        {
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            return new Registration<T>(registration, lifetime);
        }

        public static Registration<T> Tag<T>(this Registration<T> registration, [NotNull] object tagValue)
        {
            if (tagValue == null) throw new ArgumentNullException(nameof(tagValue));
            return new Registration<T>(registration, tagValue);
        }

        public static Registration<T> State<T>(this Registration<T> registration, [NotNull] Type[] stateTypes)
        {
            if (stateTypes == null) throw new ArgumentNullException(nameof(stateTypes));
            return new Registration<T>(registration, stateTypes);
        }

        public static RegistrationToken To<T>(this Registration<T> registration, [NotNull] IFactory factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return new RegistrationToken(registration.Container, CreateRegistration(registration, factory));
        }

        public static RegistrationToken To<T>(this Registration<T> registration, [NotNull] Func<Context, T> factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return registration.To(new FuncFactory<T>(factory));
        }

        public static RegistrationToken To<T>(this Registration<T> registration, [NotNull] Type instanceType, [NotNull] params Dependency[] dependencies)
        {
            if (instanceType == null) throw new ArgumentNullException(nameof(instanceType));
            if (dependencies == null) throw new ArgumentNullException(nameof(dependencies));
            var typeInfo = instanceType.GetTypeInfo();
            if (typeInfo.IsAbstract || typeInfo.IsInterface)
            {
                registration.Container.GetIssueResolver().CannotBeCeated(instanceType);
                throw new InvalidOperationException($"An instance of the type \"{instanceType}\" cannot be created.");
            }

            var factory = registration.Container.Get<IFactory>(instanceType, dependencies);
            return registration.To(factory);
        }

        public static void ToSelf(this RegistrationToken registrationToken)
        {
            registrationToken.Container.Get<IResourceStore>().AddResource(registrationToken);
        }

        public static Resolving Tag([NotNull] this IContainer container, [CanBeNull] object tag)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Resolving(container, tag);
        }

        public static bool TryGet([NotNull] this IContainer container, [NotNull] Type contractType, out object instance, [NotNull] params object[] args){
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (contractType == null) throw new ArgumentNullException(nameof(contractType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            var key = container.CreateKey(contractType);
            if (!container.TryGetResolver(key, out var resolver))
            {
                instance = null;
                return false;
            }

            instance = resolver.Resolve(container, contractType, args);
            return true;
        }

        public static bool TryGet<T>([NotNull] this IContainer container, out T contract, [NotNull] params object[] args)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (!container.TryGet(typeof(T), out var instanceObject, args))
            {
                contract = default(T);
                return false;
            }

            contract = (T) instanceObject;
            return true;
        }

        [NotNull]
        public static object Get([NotNull] this IContainer container, [NotNull] Type contractType, [NotNull] params object[] args)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (contractType == null) throw new ArgumentNullException(nameof(contractType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (!container.TryGet(contractType, out var instance, args))
            {
                return container.GetIssueResolver().CannotResolve(container, container.CreateKey(contractType));
            }

            return instance;
        }

        [NotNull]
        public static T Get<T>([NotNull] this IContainer container, [NotNull] params object[] args)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (!container.TryGet<T>(out var instance, args))
            {
                return (T)container.GetIssueResolver().CannotResolve(container, container.CreateKey(typeof(T)));
            }

            return instance;
        }

        [NotNull]
        public static Func<T> FuncGet<T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<Func<T>>();
        }

        [NotNull]
        public static Func<T1, T> FuncGet<T1, T>([NotNull] this IContainer container, [CanBeNull] T1 arg1)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<Func<T1, T>>(arg1);
        }

        [NotNull]
        public static Func<T1, T2, T> FuncGet<T1, T2, T>([NotNull] this IContainer container, [CanBeNull] T1 arg1, [CanBeNull] T2 arg2)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<Func<T1, T2, T>>(arg1, arg2);
        }

        [NotNull]
        public static Func<T1, T2, T3, T> FuncGet<T1, T2, T3, T>([NotNull] this IContainer container, [CanBeNull] T1 arg1, [CanBeNull] T2 arg2, [CanBeNull] T3 arg3)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<Func<T1, T2, T3, T>>(arg1, arg2, arg3);
        }

        [NotNull]
        public static Func<T1, T2, T3, T4, T> FuncGet<T1, T2, T3, T4, T>([NotNull] this IContainer container, [CanBeNull] T1 arg1, [CanBeNull] T2 arg2, [CanBeNull] T3 arg3, [CanBeNull] T4 arg4)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Get<Func<T1, T2, T3, T4, T>>(arg1, arg2, arg3, arg4);
        }

        [NotNull]
        public static Task<T> TaskGet<T>([NotNull] this IContainer container, [NotNull] params object[] args)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));
            return container.Get<Task<T>>(args);
        }

        [NotNull]
        public static async Task<T> StartGet<T>([NotNull] this IContainer container, [NotNull] TaskScheduler taskScheduler, [NotNull] params object[] args)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (taskScheduler == null) throw new ArgumentNullException(nameof(taskScheduler));
            if (args == null) throw new ArgumentNullException(nameof(args));
            var task = container.TaskGet<T>(args);
            task.Start(taskScheduler);
            return await task;
        }

        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            foreach (var configuration in configurations)
            {
                container.Get<IResourceStore>().AddResource(Disposable.Create(configuration.Apply(container)));
            }

            return container;
        }

        [NotNull]
        private static IDisposable CreateRegistration<T>(this Registration<T> registration, IFactory factory)
        {
            var keys =
                from contract in registration.ContractsTypes
                from tag in registration.Tags.DefaultIfEmpty(IoC.Tag.Default)
                select new Key(contract, tag);

            return registration.Container.Register(keys, factory, registration.Lifetime);
        }

        [NotNull]
        private static string CreateContainerName([CanBeNull] string name)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return !string.IsNullOrWhiteSpace(name) ? name : Interlocked.Increment(ref _containerId).ToString(CultureInfo.InvariantCulture);
        }

        private static Key CreateKey([NotNull] this IContainer container, [NotNull] Type contractType)
        {
            object tagValue = null;
            if (container is Resolving resolving)
            {
                tagValue = resolving.Tag;
            }

            var tag = tagValue != null ? new Tag(tagValue) : IoC.Tag.Default;
            return new Key(new Contract(contractType), tag);
        }

        private static IIssueResolver GetIssueResolver(this IContainer container)
        {
            return container.Get<IIssueResolver>();
        }
    }
}
