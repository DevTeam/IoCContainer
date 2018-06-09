namespace IoC.Core
{
    using System;

    internal static class TypeDescriptor<T>
    {
        [NotNull] public static readonly Type Type = typeof(T);
        // ReSharper disable once StaticMemberInGenericType
        public static readonly int HashCode = Type.GetHashCode();
        // ReSharper disable once StaticMemberInGenericType
        [NotNull] public static readonly TypeDescriptor Descriptor = Type.Descriptor();
    }
}
