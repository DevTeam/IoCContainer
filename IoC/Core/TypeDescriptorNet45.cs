#if !NET40
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal sealed class TypeDescriptor
    {
        internal readonly Type Type;
        internal readonly Lazy<TypeInfo> TypeInfo;
        private readonly Lazy<bool> _isGenericTypeArgument;

        [MethodImpl((MethodImplOptions)256)]
        public TypeDescriptor([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            TypeInfo = new Lazy<TypeInfo>(type.GetTypeInfo);
            _isGenericTypeArgument = new Lazy<bool>(() => GetCustomAttributes<GenericTypeArgumentAttribute>(true).Any());
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type AsType() => Type;

        [MethodImpl((MethodImplOptions)256)]
        public Guid GetId() => TypeInfo.Value.GUID;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Assembly GetAssembly() => TypeInfo.Value.Assembly;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsValueType() => TypeInfo.Value.IsValueType;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsInterface() => TypeInfo.Value.IsInterface;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsGenericParameter() => TypeInfo.Value.IsGenericParameter;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsArray() => TypeInfo.Value.IsArray;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsPublic() => TypeInfo.Value.IsPublic;

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public Type GetElementType() => TypeInfo.Value.GetElementType();

        [MethodImpl((MethodImplOptions)256)]
        public bool IsConstructedGenericType() => Type.IsConstructedGenericType;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsGenericTypeDefinition() => TypeInfo.Value.IsGenericTypeDefinition;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type[] GetGenericTypeArguments() => TypeInfo.Value.GenericTypeArguments;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type[] GetGenericParameterConstraints() => TypeInfo.Value.GetGenericParameterConstraints();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type[] GetGenericTypeParameters() => TypeInfo.Value.GenericTypeParameters;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsGenericTypeArgument() => _isGenericTypeArgument.Value;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<T> GetCustomAttributes<T>(bool inherit)
            where T : Attribute
            => TypeInfo.Value.GetCustomAttributes<T>(inherit);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<ConstructorInfo> GetDeclaredConstructors() => TypeInfo.Value.DeclaredConstructors;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<MethodInfo> GetDeclaredMethods() => TypeInfo.Value.DeclaredMethods;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<MemberInfo> GetDeclaredMembers() => TypeInfo.Value.DeclaredMembers;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<FieldInfo> GetDeclaredFields() => TypeInfo.Value.DeclaredFields;

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public Type GetBaseType() => TypeInfo.Value.BaseType;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<Type> GetImplementedInterfaces() => TypeInfo.Value.ImplementedInterfaces;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsAssignableFrom([NotNull] TypeDescriptor typeDescriptor)
        {
            if (typeDescriptor == null) throw new ArgumentNullException(nameof(typeDescriptor));
            return TypeInfo.Value.IsAssignableFrom(typeDescriptor.TypeInfo.Value);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type MakeGenericType([NotNull] params Type[] typeArguments)
        {
            if (typeArguments == null) throw new ArgumentNullException(nameof(typeArguments));
            return Type.MakeGenericType(typeArguments);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type GetGenericTypeDefinition() => Type.GetGenericTypeDefinition();
    }
}
#endif
