namespace IoC.Internal
{
    using System;

    internal class FuncFactory<T>: IFactory
    {
        [NotNull] private readonly Func<Context, T> _factory;

        public FuncFactory([NotNull] Func<Context, T> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public object Create(Context context)
        {
            return _factory(context);
        }
    }
}
