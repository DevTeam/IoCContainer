namespace IoC.Core
{
    using System;
    using Issues;

    internal class FoundCyclicDependency : IFoundCyclicDependency
    {
        public static readonly IFoundCyclicDependency Shared = new FoundCyclicDependency();

        private FoundCyclicDependency() { }

        public void Resolve(Key key, int reentrancy)
        {
            if (reentrancy <= 0) throw new ArgumentOutOfRangeException(nameof(reentrancy));
            if (reentrancy >= 256)
            {
                throw new InvalidOperationException($"The cyclic dependency detected resolving the dependency {key}. The reentrancy is {reentrancy}.");
            }
        }
    }
}