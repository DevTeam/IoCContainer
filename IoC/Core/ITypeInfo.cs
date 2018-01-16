namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal interface ITypeInfo
    {
        [NotNull]
        Type Type { get; }

        bool IsValueType { get; }

        bool IsConstructedGenericType { get; }

        bool IsGenericTypeDefinition { get; }

        [NotNull][ItemNotNull]
        Type[] GenericTypeArguments { get; }

        [NotNull][ItemNotNull]
        IEnumerable<ConstructorInfo> DeclaredConstructors { get; }

        [NotNull][ItemNotNull]
        IEnumerable<MethodInfo> DeclaredMethods { get; }

        bool IsAssignableFrom([NotNull] ITypeInfo typeInfo);

        [NotNull]
        Type MakeGenericType([NotNull][ItemNotNull] params Type[] typeArguments);

        [NotNull]
        Type GetGenericTypeDefinition();
    }
}
