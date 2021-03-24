namespace IoC.Core
{
    internal struct ArgsResolver<T>
    {
        public readonly T Value;
        public readonly bool HasValue;

        // ReSharper disable once SuggestBaseTypeForParameter
        public ArgsResolver([NotNull] object[] args)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < args.Length; index++)
            {
                if (args[index] is T value)
                {
                    Value = value;
                    HasValue = true;
                    return;
                }
            }

            Value = default(T);
            HasValue = false;
        }
    }
}