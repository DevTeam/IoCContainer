// ReSharper disable UnusedParameter.Global
namespace IoC
{
    [PublicAPI]
    public static class Injections
    {
        public static T Inject<T>(this IContainer container)
        {
            return default(T);
        }

        public static T Inject<T>(this IContainer container, [CanBeNull] object tag)
        {
            return default(T);
        }

        public static void Inject<T>(this IContainer container, [NotNull] T destination, [CanBeNull] T source)
        {
        }
    }
}
