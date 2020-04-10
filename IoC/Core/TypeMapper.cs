namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal sealed class TypeMapper
    {
        public static readonly TypeMapper Shared = new TypeMapper();

        private TypeMapper() { }

        public void Map(Type type, Type targetType, IDictionary<Type, Type> typesMap)
        {
            if (type == targetType)
            {
                return;
            }

            if (typesMap.ContainsKey(type))
            {
                return;
            }

            // Generic type marker
            var typeDescriptor = type.Descriptor();
            if (typeDescriptor.IsGenericTypeArgument())
            {
                typesMap[type] = targetType;
                return;
            }

            var targetTypeDescriptor = targetType.Descriptor();

            // Constructed generic
            if (targetTypeDescriptor.IsConstructedGenericType())
            {
                if (typeDescriptor.GetId() == targetTypeDescriptor.GetId())
                {
                    typesMap[typeDescriptor.Type] = targetTypeDescriptor.Type;
                    var typeArgs = typeDescriptor.GetGenericTypeArguments();
                    var targetTypeArgs = targetTypeDescriptor.GetGenericTypeArguments();
                    for (var i = 0; i < typeArgs.Length; i++)
                    {
                        Map(typeArgs[i], targetTypeArgs[i], typesMap);
                    }

                    return;
                }

                foreach (var implementedInterface in targetTypeDescriptor.GetImplementedInterfaces())
                {
                    Map(type, implementedInterface, typesMap);
                }

                foreach (var implementedInterface in typeDescriptor.GetImplementedInterfaces())
                {
                    Map(implementedInterface, targetType, typesMap);
                }

                return;
            }

            // Array
            if (targetTypeDescriptor.IsArray())
            {
                Map(typeDescriptor.GetElementType(), targetTypeDescriptor.GetElementType(), typesMap);
                typesMap[typeDescriptor.Type] = targetTypeDescriptor.Type;
            }
        }
    }
}
