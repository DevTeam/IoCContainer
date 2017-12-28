namespace IoC
{
    using System;

    public struct HasMethod
    {
        internal readonly string Name;
        internal readonly Has[] Dependencies;

        public HasMethod([NotNull] string name, [NotNull] params Has[] dependencies)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
        }

        public override string ToString()
        {
            return $"Method: [{Name}, {string.Join(",", Dependencies)}]";
        }
    }
}
