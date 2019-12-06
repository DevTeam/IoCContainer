namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    /// <summary>
    /// Metadata for aspect oriented autowiring strategy.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    internal struct AspectOrientedMetadata: IAspectOrientedMetadata, IAutowiringStrategy
    {
        internal static readonly AspectOrientedMetadata Empty = new AspectOrientedMetadata(new Dictionary<Type, Func<Attribute, Type>>(), new Dictionary<Type, Func<Attribute, IComparable>>(), new Dictionary<Type, Func<Attribute, object>>());
        private readonly IDictionary<Type, Func<Attribute, Type>> _typeSelectors; 
        private readonly IDictionary<Type, Func<Attribute, IComparable>> _orderSelectors;
        private readonly IDictionary<Type, Func<Attribute, object>> _tagSelectors;
        private readonly ILockObject _lockObject;
        private volatile IAutowiringStrategy _autowiringStrategy;

        public static AspectOrientedMetadata Type<TTypeAttribute>(AspectOrientedMetadata metadata, Func<TTypeAttribute, Type> typeSelector)
            where TTypeAttribute : Attribute =>
            new AspectOrientedMetadata(
                new Dictionary<Type, Func<Attribute, Type>>(metadata._typeSelectors) { [typeof(TTypeAttribute)] = attribute => typeSelector((TTypeAttribute)attribute) },
                new Dictionary<Type, Func<Attribute, IComparable>>(metadata._orderSelectors),
                new Dictionary<Type, Func<Attribute, object>>(metadata._tagSelectors));

        public static AspectOrientedMetadata Order<TOrderAttribute>(AspectOrientedMetadata metadata, Func<TOrderAttribute, IComparable> orderSelector)
            where TOrderAttribute : Attribute =>
            new AspectOrientedMetadata(
                new Dictionary<Type, Func<Attribute, Type>>(metadata._typeSelectors),
                new Dictionary<Type, Func<Attribute, IComparable>>(metadata._orderSelectors) { [typeof(TOrderAttribute)] = attribute => orderSelector((TOrderAttribute)attribute) },
                new Dictionary<Type, Func<Attribute, object>>(metadata._tagSelectors));

        public static AspectOrientedMetadata Tag<TTagAttribute>(AspectOrientedMetadata metadata, Func<TTagAttribute, object> tagSelector)
            where TTagAttribute : Attribute =>
            new AspectOrientedMetadata(
                new Dictionary<Type, Func<Attribute, Type>>(metadata._typeSelectors),
                new Dictionary<Type, Func<Attribute, IComparable>>(metadata._orderSelectors),
                new Dictionary<Type, Func<Attribute, object>>(metadata._tagSelectors) { [typeof(TTagAttribute)] = attribute => tagSelector((TTagAttribute)attribute) });

        private AspectOrientedMetadata(
            [NotNull] IDictionary<Type, Func<Attribute, Type>> typeSelectors,
            [NotNull] IDictionary<Type, Func<Attribute, IComparable>> orderSelectors,
            [NotNull] IDictionary<Type, Func<Attribute, object>> tagSelectors)
        {
            _lockObject = new LockObject();
            _typeSelectors = typeSelectors;
            _orderSelectors = orderSelectors;
            _tagSelectors = tagSelectors;
            _autowiringStrategy = null;
        }

        bool IAspectOrientedMetadata.TryGetType(Attribute attribute, out Type type)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            if (_typeSelectors.TryGetValue(attribute.GetType(), out var selector))
            {
                type = selector(attribute);
                return true;
            }

            type = default(Type);
            return false;
        }

        bool IAspectOrientedMetadata.TryGetOrder(Attribute attribute, out IComparable comparable)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            if (_orderSelectors.TryGetValue(attribute.GetType(), out var selector))
            {
                comparable = selector(attribute);
                return true;
            }

            comparable = default(IComparable);
            return false;
        }

        bool IAspectOrientedMetadata.TryGetTag(Attribute attribute, out object tag)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            if (_tagSelectors.TryGetValue(attribute.GetType(), out var selector))
            {
                tag = selector(attribute);
                return true;
            }

            tag = default(object);
            return false;
        }

        /// <inheritdoc />
        public bool TryResolveType(Type registeredType, Type resolvingType, out Type instanceType) =>
            GetAutowiringStrategy().TryResolveType(
                registeredType ?? throw new ArgumentNullException(nameof(registeredType)),
                resolvingType ?? throw new ArgumentNullException(nameof(resolvingType)),
                out instanceType);

        /// <inheritdoc />
        public bool TryResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor) =>
            GetAutowiringStrategy().TryResolveConstructor(
                constructors ?? throw new ArgumentNullException(nameof(constructors)),
                out constructor);

        /// <inheritdoc />
        public bool TryResolveInitializers(IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers) =>
            GetAutowiringStrategy().TryResolveInitializers(
                methods ?? throw new ArgumentNullException(nameof(methods)),
                out initializers);

        private IAutowiringStrategy GetAutowiringStrategy()
        {
            if (_autowiringStrategy != null)
            {
                return _autowiringStrategy;
            }

            lock (_lockObject)
            {
                if (_autowiringStrategy == null)
                {
                    _autowiringStrategy = new AspectOrientedAutowiringStrategy(this);
                }
            }

            return _autowiringStrategy;
        }
    }
}