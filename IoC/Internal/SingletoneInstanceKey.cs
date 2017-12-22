namespace IoC.Internal
{
    internal struct SingletoneInstanceKey : IInstanceKey
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly long _contextRegistrationId;

        public SingletoneInstanceKey(long contextRegistrationId)
        {
            _contextRegistrationId = contextRegistrationId;
        }
    }
}
