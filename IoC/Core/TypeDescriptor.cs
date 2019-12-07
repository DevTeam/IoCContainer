namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal struct TypeDescriptor
    {
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETCOREAPP2_0 && !NETCOREAPP2_1 && !NETCOREAPP2_2 && !WINDOWS_UWP
        private const BindingFlags DefaultBindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.Static;
        // ReSharper disable once MemberCanBePrivate.Global
        internal readonly Type Type;

        [MethodImpl((MethodImplOptions)256)]
        public TypeDescriptor([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type AsType() => Type;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public Guid GetId() => Type.GUID;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Assembly GetAssembly() => Type.Assembly;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsValueType() => Type.IsValueType;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsArray() => Type.IsArray;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsPublic() => Type.IsPublic;

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        [Pure]
        public Type GetElementType() => Type.GetElementType();

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsInterface() => Type.IsInterface;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsAbstract() => Type.IsAbstract;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsGenericParameter() => Type.IsGenericParameter;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsConstructedGenericType() => Type.IsGenericType && !Type.IsGenericTypeDefinition;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsGenericTypeDefinition() => Type.IsGenericTypeDefinition;

        public bool IsGenericTypeArgument() => Type.GetCustomAttributes(TypeDescriptor<GenericTypeArgumentAttribute>.Type, true).Length > 0;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeArguments() => Type.GetGenericArguments();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericParameterConstraints() => Type.GetGenericParameterConstraints();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeParameters() => Type.GetGenericArguments();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<ConstructorInfo> GetDeclaredConstructors() => Type.GetConstructors(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<MethodInfo> GetDeclaredMethods() => Type.GetMethods(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<MemberInfo> GetDeclaredMembers() => Type.GetMembers(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<FieldInfo> GetDeclaredFields() => Type.GetFields(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        [Pure]
        public Type GetBaseType() => Type.BaseType;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<Type> GetImplementedInterfaces() => Type.GetInterfaces();

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsAssignableFrom(TypeDescriptor typeDescriptor)
        {
            return Type.IsAssignableFrom(typeDescriptor.Type);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type MakeGenericType([NotNull] params Type[] typeArguments)
        {
            if (typeArguments == null) throw new ArgumentNullException(nameof(typeArguments));
            return Type.MakeGenericType(typeArguments);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type GetGenericTypeDefinition() => Type.GetGenericTypeDefinition();

        public override string ToString() => TypeToStringConverter.Convert(Type);

        public override bool Equals(object obj)
        {
            return obj is TypeDescriptor other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        private bool Equals(TypeDescriptor other)
        {
            return Type == other.Type;
        }
#else
        internal readonly Type Type;
        private readonly TypeInfo _typeInfo;

        [MethodImpl((MethodImplOptions)256)]
        public TypeDescriptor([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            _typeInfo = type.GetTypeInfo();
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type AsType() => Type;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public Guid GetId() => _typeInfo.GUID;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Assembly GetAssembly() => _typeInfo.Assembly;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsValueType() => _typeInfo.IsValueType;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsInterface() => _typeInfo.IsInterface;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsAbstract() => _typeInfo.IsAbstract;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsGenericParameter() => _typeInfo.IsGenericParameter;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsArray() => _typeInfo.IsArray;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsPublic() => _typeInfo.IsPublic;

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        [Pure]
        public Type GetElementType() => _typeInfo.GetElementType();

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsConstructedGenericType() => Type.IsConstructedGenericType;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsGenericTypeDefinition() => _typeInfo.IsGenericTypeDefinition;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeArguments() => _typeInfo.GenericTypeArguments;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericParameterConstraints() => _typeInfo.GetGenericParameterConstraints();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeParameters() => _typeInfo.GenericTypeParameters;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsGenericTypeArgument() => _typeInfo.GetCustomAttribute<GenericTypeArgumentAttribute>(true) != null;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<ConstructorInfo> GetDeclaredConstructors() => _typeInfo.DeclaredConstructors;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<MethodInfo> GetDeclaredMethods() => _typeInfo.DeclaredMethods;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<MemberInfo> GetDeclaredMembers() => _typeInfo.DeclaredMembers;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<FieldInfo> GetDeclaredFields() => _typeInfo.DeclaredFields;

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        [Pure]
        public Type GetBaseType() => _typeInfo.BaseType;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<Type> GetImplementedInterfaces() => _typeInfo.ImplementedInterfaces;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsAssignableFrom(TypeDescriptor typeDescriptor)
        {
            return _typeInfo.IsAssignableFrom(typeDescriptor._typeInfo);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type MakeGenericType([NotNull] params Type[] typeArguments)
        {
            if (typeArguments == null) throw new ArgumentNullException(nameof(typeArguments));
            return Type.MakeGenericType(typeArguments);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type GetGenericTypeDefinition() => Type.GetGenericTypeDefinition();

        public override string ToString() => TypeToStringConverter.Convert(Type);

        public override bool Equals(object obj)
        {
            return obj is TypeDescriptor other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Type != null ? Type.GetHashCode() : 0;
        }

        private bool Equals(TypeDescriptor other)
        {
            return Type == other.Type;
        }
#endif
    }
}

