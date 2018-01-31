namespace IoC.Core.Emitters
{
    using System;

    internal sealed class EmitContext
    {
        [NotNull] public readonly Key Key;
        [NotNull] public readonly IContainer Container;
        [NotNull] public readonly IDependencyEmitter<IDependency> DependencyEmitter;
        [NotNull] public readonly ILifetimeEmitter LifetimeEmitter;
        [NotNull] public readonly Emitter Emitter;
        [NotNull] public readonly EmitStatistics Statistics;

        public EmitContext(
            [NotNull] Key key,
            [NotNull] IContainer container,
            [NotNull] IDependencyEmitter<IDependency> dependencyEmitter,
            [NotNull] ILifetimeEmitter lifetimeEmitter,
            [NotNull] Emitter emitter,
            [NotNull] EmitStatistics statistics)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Container = container ?? throw new ArgumentNullException(nameof(container));
            DependencyEmitter = dependencyEmitter ?? throw new ArgumentNullException(nameof(dependencyEmitter));
            LifetimeEmitter = lifetimeEmitter ?? throw new ArgumentNullException(nameof(lifetimeEmitter));
            Emitter = emitter ?? throw new ArgumentNullException(nameof(emitter));
            Statistics = statistics ?? throw new ArgumentNullException(nameof(statistics));
        }
    }
}
