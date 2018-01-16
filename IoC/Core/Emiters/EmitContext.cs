namespace IoC.Core.Emiters
{
    using System;

    internal sealed class EmitContext
    {
        [NotNull] public readonly Key Key;
        [NotNull] public readonly IContainer Container;
        [NotNull] public readonly IEmitter<IDependency> DependencyEmitter;
        [NotNull] public readonly Emmiters.Emmiter Emitter;
        [NotNull] public readonly EmitStatistics Statistics;

        public EmitContext(
            [NotNull] Key key,
            [NotNull] IContainer container,
            [NotNull] IEmitter<IDependency> dependencyEmitter,
            [NotNull] Emmiters.Emmiter emmiter,
            [NotNull] EmitStatistics statistics)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Container = container ?? throw new ArgumentNullException(nameof(container));
            DependencyEmitter = dependencyEmitter ?? throw new ArgumentNullException(nameof(dependencyEmitter));
            Emitter = emmiter ?? throw new ArgumentNullException(nameof(emmiter));
            Statistics = statistics ?? throw new ArgumentNullException(nameof(statistics));
        }
    }
}
