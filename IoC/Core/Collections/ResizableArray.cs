namespace IoC.Core.Collections
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class ResizableArray<T>
    {
        public static readonly ResizableArray<T> Empty = new ResizableArray<T>();
        public readonly T[] Items;

        private ResizableArray()
        {
            Items = new T[0];
        }

        private ResizableArray(ResizableArray<T> previous, T value)
        {
            var length = previous.Items.Length;
            Items = new T[length + 1];
            Array.Copy(previous.Items, Items, length);
            Items[length] = value;
        }

        [MethodImpl((MethodImplOptions)256)]
        public ResizableArray<T> Add(T value)
        {
            return new ResizableArray<T>(this, value);
        }
    }
}