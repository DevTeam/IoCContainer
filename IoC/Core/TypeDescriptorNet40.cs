#if NET40
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal struct TypeDescriptor
    {
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
        public bool IsConstructedGenericType() => Type.IsGenericType;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsGenericTypeDefinition() => Type.IsGenericTypeDefinition;

        public bool IsGenericTypeArgument() => Type.GetCustomAttributes(TypeDescriptor<GenericTypeArgumentAttribute>.Type, true).Any();

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

        public override string ToString() => Type.ToString();

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
    }
}
#endif
