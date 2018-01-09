namespace IoC.Internal.Configuration
{
    internal interface IConverter<in TSrc, in TContext, TDst>
    {
        bool TryConvert(TContext context, TSrc src, out TDst dts);
    }
}
