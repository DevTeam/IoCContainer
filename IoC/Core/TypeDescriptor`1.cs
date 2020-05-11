namespace IoC.Core
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    internal static class TypeDescriptor<T>
    {
        public static readonly TypeDescriptor Descriptor = new TypeDescriptor(typeof(T));
        public static readonly int HashCode = typeof(T).GetHashCode();
    }
}
