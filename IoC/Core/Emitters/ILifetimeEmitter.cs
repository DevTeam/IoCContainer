namespace IoC.Core.Emitters
{
    internal interface ILifetimeEmitter
    {
        void Emit([NotNull] EmitContext ctx, [NotNull] ILifetime lifetime);
    }
}
