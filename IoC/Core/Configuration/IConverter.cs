namespace IoC.Core.Configuration
{
    internal interface IConverter<in TSrc, in TContext, TDst>
    {
        bool TryConvert([NotNull] TContext context, [NotNull] TSrc src, out TDst dts);
    }
}
