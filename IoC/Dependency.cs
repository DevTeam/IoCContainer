namespace IoC
{
    [PublicAPI]
    public struct Dependency
    {
        internal readonly Parameter Parameter;
        internal readonly Tag Tag;
        internal readonly int ArgIndex;
        internal readonly DependencyType Type;

        public static Dependency Arg<T>(int argIndex, int position, [CanBeNull] string name = null)
        {
            return new Dependency(new Parameter(typeof(T), position, name), argIndex);
        }

        public static Dependency Ref<T>([CanBeNull] object tag, int position, [CanBeNull] string name = null)
        {
            return new Dependency(new Parameter(typeof(T), position, name), new Tag(tag));
        }

        private Dependency(Parameter parameter, int argIndex)
        {
            Type = DependencyType.Arg;
            Parameter = parameter;
            ArgIndex = argIndex;
            Tag = Tag.Default;
        }

        private Dependency(Parameter parameter, Tag tag)
        {
            Type = DependencyType.Ref;
            Parameter = parameter;
            Tag = tag;
            ArgIndex = 0;
        }
    }

    public enum DependencyType
    {
        Arg,

        Ref
    }
}
