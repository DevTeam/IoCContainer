namespace IoC.Core.Collections
{
    using System.Runtime.CompilerServices;

    internal struct KeyValue<TKey, TValue>
    {
        public readonly int HashCode;
        public readonly TKey Key;
        public readonly TValue Value;

        [MethodImpl((MethodImplOptions)256)]
        public KeyValue(int hashCode, TKey key, TValue value)
        {
            HashCode = hashCode;
            Key = key;
            Value = value;
        }
    }
}
