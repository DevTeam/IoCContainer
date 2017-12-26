namespace IoC.Internal
{
    internal struct LifetimeBaseFactory: IFactory
    {
        private readonly ILifetime _lifetime;
        private readonly IFactory _factory;

        public LifetimeBaseFactory(ILifetime lifetime, IFactory factory)
        {
            _lifetime = lifetime;
            _factory = factory;
        }

        public object Create(Context context)
        {
            return _lifetime.GetOrCreate(context, _factory);
        }
    }
}
