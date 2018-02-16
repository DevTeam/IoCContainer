namespace IoC.Core.Collections
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    internal sealed class List<T>
    {
        private static readonly T[] EmptyArray = new T[0];
        public static readonly List<T> Empty = new List<T>();
        public readonly T[] Items;

        public List(List<T> previousList, T value)
        {
            var items = previousList.Items;
            var count = items.Length;
            Items = new T[count + 1];
            Array.Copy(items, Items, count);
            Items[count] = value;
        }

        private List()
        {
            Items = EmptyArray;
        }

        public List<T> Add(T value)
        {
            return new List<T>(this, value);
        }
    }
}