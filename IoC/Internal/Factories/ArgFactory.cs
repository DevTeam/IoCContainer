namespace IoC.Internal.Factories
{
    internal class ArgFactory : IFactory
    {
        private readonly int _argIndex;

        public ArgFactory(int argIndex)
        {
            _argIndex = argIndex;
        }

        public object Create(Context context)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return context.Args[_argIndex];
        }
    }
}
