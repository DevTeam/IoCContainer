namespace IoC.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    internal static class TypeDescriptor<T>
    {
        [NotNull] public static readonly Type Type = typeof(T);

        public static readonly int HashCode = Type.GetHashCode();

        public static readonly TypeDescriptor Descriptor = new TypeDescriptor(Type);

        public static readonly Key Key = new Key(Type);
    }
}
