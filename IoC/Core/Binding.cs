namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal struct Binding<T>: IBinding<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly ITypeInfo GenericTypeArgumentInfo = TypeExtensions.Info<GenericTypeArgument>();

        public Binding([NotNull] IContainer container, [NotNull][ItemNotNull] params Type[] types)
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

        public Binding([NotNull] IBinding<T> binding, Lifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Types = binding.Types;
            Tags = binding.Tags;
            Lifetime = lifetime != IoC.Lifetime.Transient ? binding.Container.Tag(lifetime).Get<ILifetime>() : null;
        }

        public Binding([NotNull] IBinding<T> binding, [NotNull] ILifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Types = binding.Types;
            Lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            Tags = binding.Tags;
        }

        public Binding([NotNull] IBinding<T> binding, [CanBeNull] object tagValue)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Types = binding.Types;
            Lifetime = binding.Lifetime;
            Tags = binding.Tags.Concat(Enumerable.Repeat(tagValue, 1)).Distinct().ToArray();
        }

        public IContainer Container { get; }

        public IEnumerable<Type> Types { get; }

        public IEnumerable<object> Tags { get; }

        public ILifetime Lifetime { get; }
    }
}
