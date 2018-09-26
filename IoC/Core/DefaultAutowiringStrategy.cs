namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class DefaultAutowiringStrategy: IAutowiringStrategy
    {
        public static readonly IAutowiringStrategy Shared = new DefaultAutowiringStrategy();

        private DefaultAutowiringStrategy()
        {
        }

        [MethodImpl((MethodImplOptions)256)]
        public bool TryResolveType(Type registeredType, Type resolvingType, out Type instanceType)
        {
            if (registeredType == null) throw new ArgumentNullException(nameof(registeredType));
            if (resolvingType == null) throw new ArgumentNullException(nameof(resolvingType));
            var registeredTypeDescriptor = registeredType.Descriptor();
            if (!registeredTypeDescriptor.IsGenericTypeDefinition())
            {
                instanceType = registeredTypeDescriptor.AsType();
                return true;
            }

            var resolvingTypeDescriptor = resolvingType.Descriptor();
            var registeredGenericTypeParameters = registeredTypeDescriptor.GetGenericTypeParameters();
            var typesMap = registeredGenericTypeParameters.Distinct().Zip(GenericTypeArguments.Types, Tuple.Create).ToDictionary(i => i.Item1, i => i.Item2);

            var resolvingTypeDefinitionDescriptor = resolvingTypeDescriptor.GetGenericTypeDefinition().Descriptor();
            var resolvingTypeDefinitionGenericTypeParameters = resolvingTypeDefinitionDescriptor.GetGenericTypeParameters();
            var constraintsMap = resolvingTypeDescriptor.GetGenericTypeArguments().Zip(resolvingTypeDefinitionGenericTypeParameters, (type, typeDefinition) => Tuple.Create(type, typeDefinition.Descriptor().GetGenericParameterConstraints())).ToArray();

            for (var position = 0; position < registeredGenericTypeParameters.Length; position++)
            {
                var genericType = registeredGenericTypeParameters[position];
                if (!genericType.IsGenericParameter)
                {
                    continue;
                }

                var descriptor =  genericType.Descriptor();
                var constraints = descriptor.GetGenericParameterConstraints();
                if (constraints.Length == 0)
                {
                    registeredGenericTypeParameters[position] = typesMap[genericType];
                    continue;
                }

                var isDefined = false;
                foreach (var constraintsEntry in constraintsMap)
                {
                    if (!CoreExtensions.SequenceEqual(constraints, constraintsEntry.Item2))
                    {
                        continue;
                    }

                    registeredGenericTypeParameters[position] = constraintsEntry.Item1;
                    isDefined = true;
                    break;
                }

                if (!isDefined)
                {
                    instanceType = default(Type);
                    return false;
                }
            }

            instanceType = registeredTypeDescriptor.MakeGenericType(registeredGenericTypeParameters);
            return true;
        }


        public bool TryResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
            => (constructor = constructors.OrderBy(i => GetOrder(i.Info)).FirstOrDefault()) != null;

        public bool TryResolveInitializers(IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        {
            initializers = Enumerable.Empty<IMethod<MethodInfo>>();
            return true;
        }

        [MethodImpl((MethodImplOptions)256)]
        private static int GetOrder(MethodBase method)
        {
            var order = method.GetParameters().Length + 1;
            return method.IsPublic ? order : order << 10;
        }
    }
}
