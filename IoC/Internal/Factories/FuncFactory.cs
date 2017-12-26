namespace IoC.Internal.Factories
{
    using System;

    internal sealed class FuncFactory<T>: IFactory
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
