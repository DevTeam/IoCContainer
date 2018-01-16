namespace IoC.Core.Collections
{
    internal sealed class KeyValue<TKey, TValue>
    {
        public readonly TKey Key;
        public readonly TValue Value;

        public KeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}