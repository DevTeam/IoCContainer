namespace IoC.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal static class Reflection
    {
        public static ITypeInfo AsTypeInfo(this Type type)
        {
#if NET40
            return new OldTypeInfo(type);
#else
            return new NewTypeInfo(type);
#endif
        }

        public static bool IsConstructedGenericType(this Type type)
        {
#if NET40
            return type.IsGenericType;
#else
            return type.IsConstructedGenericType;
#endif
        }

        public static Type[] GenericTypeArguments(this Type type)
        {
#if NET40
            return type.GetGenericArguments();
#else
            return type.GenericTypeArguments;
#endif
        }

        public static MethodInfo SetMethod(this PropertyInfo propertyInfo)
        {
#if NET40
            return propertyInfo.GetSetMethod();
#else
            return propertyInfo.SetMethod;
#endif
        }

#if NET40
        private sealed class OldTypeInfo : ITypeInfo
        {
            private readonly Type _type;

            public OldTypeInfo(Type type)
            {
                _type = type;
            }

            public Type Type => _type;

            public bool IsGenericTypeDefinition => _type.IsGenericTypeDefinition;

            public IEnumerable<ConstructorInfo> DeclaredConstructors => _type.GetConstructors();

            public IEnumerable<MethodInfo> DeclaredMethods => _type.GetMethods();

            public IEnumerable<PropertyInfo> DeclaredProperties => _type.GetProperties();

            public bool IsAbstract => _type.IsAbstract;

            public bool IsInterface => _type.IsInterface;
        }
#else
        private sealed class NewTypeInfo : ITypeInfo
        {
            private readonly Lazy<TypeInfo> _typeInfo;

            public NewTypeInfo(Type type)
            {
                Type = type;
                _typeInfo = new Lazy<TypeInfo>(() => Type.GetTypeInfo());
            }

            public Type Type { get; }

            public bool IsGenericTypeDefinition => _typeInfo.Value.IsGenericTypeDefinition;

            public IEnumerable<ConstructorInfo> DeclaredConstructors => _typeInfo.Value.DeclaredConstructors;

            public IEnumerable<MethodInfo> DeclaredMethods => _typeInfo.Value.DeclaredMethods;

            public IEnumerable<PropertyInfo> DeclaredProperties => _typeInfo.Value.DeclaredProperties;

            public bool IsAbstract => _typeInfo.Value.IsAbstract;

            public bool IsInterface => _typeInfo.Value.IsInterface;
        }
#endif
    }
}
