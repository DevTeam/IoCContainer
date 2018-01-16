namespace IoC.Core.Emiters
{
    using System.Collections.Generic;

    internal class EmitStatistics
    {
        private readonly Dictionary<Key, int> _dependencyCounters = new Dictionary<Key, int>();

        public int EmitDependency(Key key)
        {
            _dependencyCounters.TryGetValue(key, out var counter);
            counter++;
            _dependencyCounters[key] = counter;
            return counter;
        }

        public IEnumerable<Key> Dependencies => _dependencyCounters.Keys;
    }
}
