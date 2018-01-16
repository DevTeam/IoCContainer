namespace IoC.Core
{
    internal static class Type<T>
    {
        [NotNull] public static readonly ITypeInfo Info = typeof(T).Info();
    }
}
