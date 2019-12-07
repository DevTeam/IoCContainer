namespace IoC.Core
{
    internal interface IConverter<in TSrc, in TContext, TDst>
    {
        bool TryConvert([NotNull] TContext context, [NotNull] TSrc src, out TDst dst);
    }
}
