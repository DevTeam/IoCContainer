namespace IoC
{
    public struct HasMethod
    {
        internal readonly string Name;
        internal readonly Has[] Dependencies;

        public HasMethod(string name, params Has[] dependencies)
        {
            Name = name;
            Dependencies = dependencies;
        }

        public override string ToString()
        {
            return $"Method: [{Name}, {string.Join(",", Dependencies)}]";
        }
    }
}
