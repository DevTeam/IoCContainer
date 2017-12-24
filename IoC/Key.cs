namespace IoC
{
    [PublicAPI]
    public struct Key
    {
        internal readonly Contract Contract;
        internal readonly Tag Tag;

        public Key(Contract contract, Tag tag)
        {
            Contract = contract;
            Tag = tag;
        }

        public override string ToString()
        {
            return $"Key: [{Contract}, {Tag}]";
        }
    }
}
