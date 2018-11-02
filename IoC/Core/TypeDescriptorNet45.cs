#if !NET40
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal struct TypeDescriptor
    {
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
        public IEnumerable<T> GetCustomAttributes<T>(bool inherit)
            where T : Attribute
            => _typeInfo.GetCustomAttributes<T>(inherit);

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

        public override string ToString() => Type.ToString();

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
    }
}
#endif
