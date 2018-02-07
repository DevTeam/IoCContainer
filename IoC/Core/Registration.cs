namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal struct Registration<T>: IRegistration<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly ITypeInfo GenericTypeArgumentInfo = Type<GenericTypeArgument>.Info;

        public Registration([NotNull] IContainer container, [NotNull][ItemNotNull] params Type[] types)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            Types = (types ?? throw new ArgumentNullException(nameof(types))).Select(ConvertType).ToArray();
            Lifetime = null;
            Tags = Enumerable.Empty<object>();
        }

        [NotNull]
        private static Type ConvertType([NotNull] Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var typeInfo = type.Info();
            if (!typeInfo.IsConstructedGenericType)
            {
                return type;
            }

            if (typeInfo.GenericTypeArguments.Any(t => GenericTypeArgumentInfo.IsAssignableFrom(t.Info())))
            {
                return typeInfo.GetGenericTypeDefinition();
            }

            return type;
        }

        public Registration([NotNull] IRegistration<T> registration, Lifetime lifetime)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            Container = registration.Container;
            Types = registration.Types;
            Tags = registration.Tags;
            Lifetime = lifetime != IoC.Lifetime.Transient ? registration.Container.Tag(lifetime).Get<ILifetime>() : null;
        }

        public Registration([NotNull] IRegistration<T> registration, [NotNull] ILifetime lifetime)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            Container = registration.Container;
            Types = registration.Types;
            Lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            Tags = registration.Tags;
        }

        public Registration([NotNull] IRegistration<T> registration, [CanBeNull] object tagValue)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            Container = registration.Container;
            Types = registration.Types;
            Lifetime = registration.Lifetime;
            Tags = registration.Tags.Concat(Enumerable.Repeat(tagValue, 1)).Distinct().ToArray();
        }

        public IContainer Container { get; }

        public IEnumerable<Type> Types { get; }

        public IEnumerable<object> Tags { get; }

        public ILifetime Lifetime { get; }
    }
}
