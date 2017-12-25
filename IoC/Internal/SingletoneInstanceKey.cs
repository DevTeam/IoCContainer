namespace IoC.Internal
{
    internal struct SingletoneInstanceKey<T> : IInstanceKey
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly T _id;

        public SingletoneInstanceKey(T id)
        {
            _id = id;
        }
    }
}
