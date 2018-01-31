namespace IoC.Core.Emitters
{
    internal interface IDependencyEmitter<in T>
        where T: IDependency
    {
        void Emit([NotNull] EmitContext ctx, [NotNull] T dependency);
    }
}
