namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface ITypeInfo
    {
        Type Type { get; }

        string Name { get; }

        bool IsGenericTypeDefinition { get; }

        IEnumerable<ConstructorInfo> DeclaredConstructors { get; }

        IEnumerable<MethodInfo> DeclaredMethods { get; }

        IEnumerable<PropertyInfo> DeclaredProperties { get; }

        bool IsAbstract { get; }

        bool IsInterface { get; }
    }
}
