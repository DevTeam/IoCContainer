namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal static class Types
    {
        public static ITypeInfo Info(this Type type)
        {
            return new InternalTypeInfo(type);
        }

        public static Assembly LoadAssembly(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(assemblyName));
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        private class InternalTypeInfo : ITypeInfo
        {
            private readonly Type _type;
            private readonly Lazy<TypeInfo> _typeInfo;

            public InternalTypeInfo([NotNull] Type type)
            {
                _type = type ?? throw new ArgumentNullException(nameof(type));
                _typeInfo = new Lazy<TypeInfo>(type.GetTypeInfo);
            }

            public Type Type => _type;

            public bool IsValueType => _typeInfo.Value.IsValueType;

            public bool IsConstructedGenericType => _type.IsConstructedGenericType;

            public bool IsGenericTypeDefinition => _typeInfo.Value.IsGenericTypeDefinition;

            public Type[] GenericTypeArguments => _typeInfo.Value.GenericTypeArguments;

            public IEnumerable<ConstructorInfo> DeclaredConstructors => _typeInfo.Value.DeclaredConstructors;

            public IEnumerable<MethodInfo> DeclaredMethods => _typeInfo.Value.DeclaredMethods;

            public bool IsAssignableFrom(ITypeInfo typeInfo)
            {
                if (typeInfo == null) throw new ArgumentNullException(nameof(typeInfo));
                return _typeInfo.Value.IsAssignableFrom(((InternalTypeInfo) typeInfo)._typeInfo.Value);
            }

            public Type MakeGenericType(params Type[] typeArguments)
            {
                if (typeArguments == null) throw new ArgumentNullException(nameof(typeArguments));
                return _type.MakeGenericType(typeArguments);
            }

            public Type GetGenericTypeDefinition()
            {
                return _type.GetGenericTypeDefinition();
            }
        }
    }
}
