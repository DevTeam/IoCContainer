namespace IoC.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    internal static class TypeDescriptor<T>
    {
        public static readonly int HashCode = typeof(T).GetHashCode();

        public static readonly TypeDescriptor Descriptor = new TypeDescriptor(typeof(T));

        public static readonly Key Key = new Key(typeof(T));
    }
}
