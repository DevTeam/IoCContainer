namespace IoC.Core.Collections
{
    using System;
    using System.Runtime.CompilerServices;

    internal struct ResizableArray<T>
    {
        public static readonly ResizableArray<T> Empty = new ResizableArray<T>(0);
        [NotNull] public readonly T[] Items;

        [MethodImpl((MethodImplOptions) 256)]
        public static ResizableArray<T> Create(int size = 0, T value = default(T))
        {
            if (size == 0)
            {
                return Empty;
            }

            var array = new ResizableArray<T>(size);
#if NETCOREAPP2_0
            Array.Fill(array.Items, value);
#else
            for (var i = 0; i < size; i++)
            {
                array.Items[i] = value;
            }
#endif
            return array;
        }

        [MethodImpl((MethodImplOptions) 256)]
        private ResizableArray(int size)
        {
            Items = new T[size];
        }

        [MethodImpl((MethodImplOptions) 256)]
        private ResizableArray(ResizableArray<T> previous, [CanBeNull] T value)
        {
            var length = previous.Items.Length;
            Items = new T[length + 1];
            Array.Copy(previous.Items, Items, length);
            Items[length] = value;
        }

        [MethodImpl((MethodImplOptions) 256)]
        private ResizableArray(ResizableArray<T> previous)
        {
            var length = previous.Items.Length;
            Items = new T[length];
            Array.Copy(previous.Items, Items, length);
        }

        [MethodImpl((MethodImplOptions) 256)]
        [Pure]
        public ResizableArray<T> Add([CanBeNull] T value)
        {
            return new ResizableArray<T>(this, value);
        }

        [MethodImpl((MethodImplOptions) 256)]
        [Pure]
        public ResizableArray<T> Copy()
        {
            return new ResizableArray<T>(this);
        }
    }
}