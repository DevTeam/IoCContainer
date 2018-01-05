namespace IoC.Internal.Factories
{
    internal sealed class ResolvingContextFactory : IFactory
    {
        public static readonly IFactory Shared = new ResolvingContextFactory();

        private ResolvingContextFactory()
        {
        }

        public object Create(ResolvingContext context)
        {
            return context;
        }
    }
}
