namespace IoC.Core.Collections
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    internal sealed class List<T>
    {
        public static readonly List<T> Empty = new List<T>();
        public readonly T[] Items;
        public readonly int Count;

        public List(List<T> previousList, T value)
        {
            Items = new T[previousList.Items.Length + 1];
            Array.Copy(previousList.Items, Items, previousList.Items.Length);
            var count = Items.Length;
            Items[count - 1] = value;
            Count = count;
        }

        private List()
        {
            Items = new T[0];
        }

        public List<T> Add(T value)
        {
            return new List<T>(this, value);
        }
    }
}