namespace IoC.Internal
{
    internal struct ArgFactory : IFactory
    {
        private readonly int _argIndex;

        public ArgFactory(int argIndex)
        {
            _argIndex = argIndex;
        }

        public object Create(Context context)
        {
            return context.Args[_argIndex];
        }
    }
}
