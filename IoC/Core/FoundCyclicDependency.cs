namespace IoC.Core
{
    using System;
    using Issues;

    internal sealed class FoundCyclicDependency : IFoundCyclicDependency
    {
        public static readonly IFoundCyclicDependency Shared = new FoundCyclicDependency();

        private FoundCyclicDependency() { }

        public void Resolve(IBuildContext buildContext)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));

            if (buildContext.Depth >= 256)
            {
                throw new InvalidOperationException($"The cyclic dependency detected resolving the dependency {buildContext.Key}. The reentrancy is {buildContext.Depth}.\n{buildContext}");
            }
        }
    }
}