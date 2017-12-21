namespace IoC
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    [PublicAPI]
    public interface IFactory
    {
        [NotNull] object Create(Context context);
    }
}
