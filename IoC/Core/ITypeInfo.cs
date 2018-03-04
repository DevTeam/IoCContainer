namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal interface ITypeInfo
    {
        [NotNull]
        Type Type { get; }

        Guid Id { get; }

        Assembly Assembly { get; }

        bool IsValueType { get; }

        bool IsInterface { get; }

        bool IsGenericParameter { get; }

        bool IsArray { get; }

        bool IsPublic { get; }

        Type ElementType { get; }

        bool IsConstructedGenericType { get; }

        bool IsGenericTypeDefinition { get; }

        bool IsGenericTypeArgument { get; }

        [NotNull] [ItemNotNull]
        IEnumerable<T> GetCustomAttributes<T>(bool inherit = true) where T : Attribute;

        [NotNull][ItemNotNull]
        Type[] GenericTypeArguments { get; }

        [NotNull][ItemNotNull]
        Type[] GenericTypeParameters { get; }

        [NotNull][ItemNotNull]
        IEnumerable<ConstructorInfo> DeclaredConstructors { get; }

        [NotNull][ItemNotNull]
        IEnumerable<MethodInfo> DeclaredMethods { get; }

        [NotNull][ItemNotNull]
        IEnumerable<MemberInfo> DeclaredMembers { get; }

        Type BaseType { get; }

        IEnumerable<Type> ImplementedInterfaces { get; }

        bool IsAssignableFrom([NotNull] ITypeInfo typeInfo);

        [NotNull]
        Type MakeGenericType([NotNull][ItemNotNull] params Type[] typeArguments);

        [NotNull]
        Type GetGenericTypeDefinition();
    }
}
