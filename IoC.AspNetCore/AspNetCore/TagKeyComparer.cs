namespace IoC.Features.AspNetCore
{
    using System.Collections.Generic;

    internal class TagKeyComparer : IComparer<Key>
    {
        public static readonly IComparer<Key> Shared = new TagKeyComparer();

        private TagKeyComparer() { }

        public int Compare(Key x, Key y) =>
            -Comparer<object>.Default.Compare(x.Tag, y.Tag);
    }
}