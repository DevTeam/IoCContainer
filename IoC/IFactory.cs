namespace IoC
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    [PublicAPI]
    public interface IFactory
    {
        [CanBeNull] object Create(Context context);
    }
}
