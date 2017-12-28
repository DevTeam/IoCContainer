namespace IoC.Internal.Factories
{
    using System;

    internal sealed class FuncFactory<T>: IFactory
    {
        private readonly string _description;
        [NotNull] private readonly Func<Context, T> _factory;

        public FuncFactory([NotNull] string description, [NotNull] Func<Context, T> factory)
        {
            _description = description ?? throw new ArgumentNullException(nameof(description));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public object Create(Context context)
        {
            return _factory(context);
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(_description) ? $"FuncFactory<{typeof(T).Name}>" : _description;
        }
    }
}
