namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class TypeToStringConverter : IConverter<Type, Type, string>
    {
        public static readonly IConverter<Type, Type, string> Shared = new TypeToStringConverter();
        private static readonly IDictionary<Type, string> PrimitiveTypes = new Dictionary<Type, string>
        {
            {typeof(byte), "byte"},
            {typeof(sbyte), "sbyte"},
            {typeof(int), "int"},
            {typeof(uint), "uint"},
            {typeof(short), "short"},
            {typeof(ushort), "ushort"},
            {typeof(long), "long"},
            {typeof(ulong), "ulong"},
            {typeof(float), "float"},
            {typeof(double), "double"},
            {typeof(char), "char"},
            {typeof(object), "object"},
            {typeof(string), "string"},
            {typeof(decimal), "decimal"}
        };

        private TypeToStringConverter() { }

        public bool TryConvert(Type context, Type type, out string typeName)
        {
            typeName = Convert(type);
            return true;
        }

        public static string Convert(Type type)
        {
            if (PrimitiveTypes.TryGetValue(type, out var typeName))
            {
                return typeName;
            }

            var typeDescriptor = type.Descriptor();
            if (typeDescriptor.IsConstructedGenericType())
            {
                return $"{GetTypeName(type)}<{string.Join(", ", typeDescriptor.GetGenericTypeArguments().Select(Convert))}>";
            }

            if (typeDescriptor.IsGenericTypeDefinition())
            {
                return $"{GetTypeName(type)}<{string.Join(", ", typeDescriptor.GetGenericTypeParameters().Select(Convert))}>";
            }

            return type.Name;
        }

        private static string GetTypeName(Type type)
        {
            var name = type.Name;
            var lastCharIndex = name.IndexOf('`');
            if (lastCharIndex > 0)
            {
                return name.Substring(0, lastCharIndex);
            }

            return name;
        }
    }
}
