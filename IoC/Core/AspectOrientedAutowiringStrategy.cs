// ReSharper disable ForCanBeConvertedToForeach
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    internal sealed class AspectOrientedAutowiringStrategy: IAutowiringStrategy
    {
        [NotNull] private readonly IAspectOrientedMetadata _metadata;

        public AspectOrientedAutowiringStrategy([NotNull] IAspectOrientedMetadata metadata)
        {
            _metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        /// <inheritdoc />
        public bool TryResolveType(IContainer container, Type registeredType, Type resolvingType, out Type instanceType)
        {
            instanceType = default(Type);
            // Says that the default logic should be used
            return false;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryResolveConstructor(IContainer container, IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
        {
            constructor = PrepareMethods(constructors).FirstOrDefault();
            if (constructor == null && DefaultAutowiringStrategy.Shared.TryResolveConstructor(container, constructors, out var defaultConstructor))
            {
                // Initialize default ctor
                constructor = PrepareMethods(new[] { defaultConstructor }, true).FirstOrDefault();
            }

            // Says that current logic should be used
            return constructor != default(IMethod<ConstructorInfo>);
        }

        /// <inheritdoc />
        public bool TryResolveInitializers(IContainer container, IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        {
            initializers = PrepareMethods(methods);
            // Says that current logic should be used
            return true;
        }

        private IEnumerable<IMethod<TMethodInfo>> PrepareMethods<TMethodInfo>(IEnumerable<IMethod<TMethodInfo>> methods, bool enforceSelection = false)
            where TMethodInfo : MethodBase =>
            from method in methods
            let methodMetadata = new Metadata(_metadata, method.Info.GetCustomAttributes(true))
            where enforceSelection || !methodMetadata.IsEmpty
            orderby methodMetadata.Order
            select SetDependencies(method, methodMetadata);

        private IMethod<TMethodInfo> SetDependencies<TMethodInfo>(IMethod<TMethodInfo> method, Metadata methodMetadata)
            where TMethodInfo : MethodBase
        {
            var parameters = method.Info.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                if (param.IsOut)
                {
                    continue;
                }

                var parameterMetadata = new Metadata(_metadata, param.GetCustomAttributes(true));
                method.SetDependency(param.Position, parameterMetadata.Type ?? param.ParameterType, parameterMetadata.Tag ?? methodMetadata.Tag, param.IsOptional);
            }

            return method;
        }

        private struct Metadata
        {
            [CanBeNull] public readonly Type Type;
            [CanBeNull] public readonly IComparable Order;
            [CanBeNull] public readonly object Tag;

            public Metadata(IAspectOrientedMetadata metadata, IEnumerable<object> attributes)
            {
                Type = default(Type);
                Order = null;
                Tag = default(object);
                foreach (var attribute in attributes)
                {
                    if (!(attribute is Attribute attributeValue))
                    {
                        continue;
                    }

                    if (Type == default(Type) && metadata.TryGetType(attributeValue, out var curType))
                    {
                        Type = curType;
                    }

                    if (Order == null && metadata.TryGetOrder(attributeValue, out var curOrder))
                    {
                        Order = curOrder;
                    }

                    if (Tag == default(object) && metadata.TryGetTag(attributeValue, out var curTag))
                    {
                        Tag = curTag;
                    }

                    if (Type != default(Type) && Order != null && Tag != default(object))
                    {
                        break;
                    }
                }
            }

            public bool IsEmpty => Type == default(Type) && Order == null && Tag == default(object);
        }
    }
}
