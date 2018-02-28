﻿namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    // ReSharper disable once RedundantUsingDirective
    using System.Linq;
    using System.Reflection;
    using Collections;

    internal static class TypeExtensions
    {
        private static readonly object LockObject = new object();
        private static Table<Type, ITypeInfo> _typeInfos = Table<Type, ITypeInfo>.Empty;

        public static ITypeInfo Info(this Type type)
        {
            var hashCode = type.GetHashCode();
            lock (LockObject)
            {
                if (!_typeInfos.TryGet(hashCode, type, out var typeInfo))
                {
                    typeInfo = new InternalTypeInfo(type);
                    _typeInfos = _typeInfos.Set(hashCode, type, typeInfo);
                }

                return new InternalTypeInfo(type);
            }
        }

        public static ITypeInfo Info<T>()
        {
            return TypeInfoHolder<T>.Shared;
        }

        public static Assembly LoadAssembly(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(assemblyName));
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        public static bool IsGenericTypeArgument([NotNull] this ITypeInfo typeInfo)
        {
            if (typeInfo == null) throw new ArgumentNullException(nameof(typeInfo));
            return typeInfo.GetCustomAttributes<GenericTypeArgumentAttribute>().Any();
        }

        [NotNull]
        public static Type ToDefinedGenericType([NotNull] this ITypeInfo typeInfo)
        {
            if (!typeInfo.IsGenericTypeDefinition)
            {
                return typeInfo.Type;
            }

            var genericTypeParameters = typeInfo.GenericTypeParameters;
            var typesMap = genericTypeParameters.Distinct().Zip(GenericTypeArguments.Types, Tuple.Create).ToDictionary(i => i.Item1, i => i.Item2);
            var genericTypeArguments = new Type[genericTypeParameters.Length];
            for (var position = 0; position < genericTypeParameters.Length; position++)
            {
                genericTypeArguments[position] = typesMap[genericTypeParameters[position]];
            }

            return typeInfo.MakeGenericType(genericTypeArguments);
        }

        private static class TypeInfoHolder<T>
        {
            [NotNull] public static readonly ITypeInfo Shared = typeof(T).Info();
        }

#if !NET40
        private sealed class InternalTypeInfo : ITypeInfo
        {
            private readonly Type _type;
            private readonly Lazy<TypeInfo> _typeInfo;
            public InternalTypeInfo([NotNull] Type type)
            {
                _type = type ?? throw new ArgumentNullException(nameof(type));
                _typeInfo = new Lazy<TypeInfo>(type.GetTypeInfo);
            }

            public Type Type => _type;

            public Guid Id => _typeInfo.Value.GUID;

            public bool IsValueType => _typeInfo.Value.IsValueType;

            public bool IsInterface => _typeInfo.Value.IsInterface;

            public bool IsGenericParameter => _typeInfo.Value.IsGenericParameter;

            public bool IsArray => _typeInfo.Value.IsArray;

            public Type ElementType => _typeInfo.Value.GetElementType();

            public bool IsConstructedGenericType => _type.IsConstructedGenericType;

            public bool IsGenericTypeDefinition => _typeInfo.Value.IsGenericTypeDefinition;

            public Type[] GenericTypeArguments => _typeInfo.Value.GenericTypeArguments;

            public Type[] GenericTypeParameters => _typeInfo.Value.GenericTypeParameters;

            public IEnumerable<T> GetCustomAttributes<T>(bool inherit)
                where T: Attribute
            {
                return _typeInfo.Value.GetCustomAttributes<T>(inherit);
            }

            public IEnumerable<ConstructorInfo> DeclaredConstructors => _typeInfo.Value.DeclaredConstructors;

            public IEnumerable<MethodInfo> DeclaredMethods => _typeInfo.Value.DeclaredMethods;

            public IEnumerable<MemberInfo> DeclaredMembers => _typeInfo.Value.DeclaredMembers;

            public Type BaseType => _typeInfo.Value.BaseType;

            public IEnumerable<Type> ImplementedInterfaces => _typeInfo.Value.ImplementedInterfaces;

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
#else
        private sealed class InternalTypeInfo : ITypeInfo
        {
            private static readonly BindingFlags DefaultBindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.Static;
            private readonly Type _type;

            public InternalTypeInfo([NotNull] Type type)
            {
                _type = type ?? throw new ArgumentNullException(nameof(type));
            }

            public Type Type => _type;

            public Guid Id => _type.GUID;

            public bool IsValueType => _type.IsValueType;

            public bool IsArray => _type.IsArray;

            public Type ElementType => _type.GetElementType();

            public bool IsInterface => _type.IsInterface;

            public bool IsGenericParameter => _type.IsGenericParameter;

            public bool IsConstructedGenericType => _type.IsGenericType;

            public bool IsGenericTypeDefinition => _type.IsGenericTypeDefinition;

            public IEnumerable<T> GetCustomAttributes<T>(bool inherit)
                where T : Attribute
            {
                return _type.GetCustomAttributes(typeof(T), inherit).Cast<T>();
            }

            public Type[] GenericTypeArguments => _type.GetGenericArguments();

            public Type[] GenericTypeParameters => _type.GetGenericArguments();

            public IEnumerable<ConstructorInfo> DeclaredConstructors => _type.GetConstructors(DefaultBindingFlags);

            public IEnumerable<MethodInfo> DeclaredMethods => _type.GetMethods(DefaultBindingFlags);

            public IEnumerable<MemberInfo> DeclaredMembers => _type.GetMembers(DefaultBindingFlags);

            public Type BaseType => _type.BaseType;

            public IEnumerable<Type> ImplementedInterfaces => _type.GetInterfaces();

            public bool IsAssignableFrom(ITypeInfo typeInfo)
            {
                if (typeInfo == null) throw new ArgumentNullException(nameof(typeInfo));
                return _type.IsAssignableFrom(((InternalTypeInfo)typeInfo)._type);
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
#endif
    }
}