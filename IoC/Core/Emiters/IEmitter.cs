namespace IoC.Core.Emiters
{
    internal interface IEmitter<in T>
        where T: IDependency
    {
        EmitResult Emit([NotNull] EmitContext ctx, [NotNull] T dependency);
    }
}
