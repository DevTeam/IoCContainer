namespace IoC
{
    [PublicAPI]
    public struct Has
    {
        internal readonly Parameter Parameter;
        internal readonly Tag Tag;
        internal readonly int ArgIndex;
        internal readonly DependencyType Type;
        internal readonly HasMethod HasMethod;

        public static Has Arg<T>(int argIndex, [NotNull] string name)
        {
            return new Has(new Parameter(name, typeof(T)), argIndex);
        }

        public static Has Arg(int argIndex, [NotNull] string name)
        {
            return new Has(new Parameter(name), argIndex);
        }

        public static Has Ref<T>([NotNull] string name, [CanBeNull] object tag)
        {
            return new Has(new Parameter(name, typeof(T)), new Tag(tag));
        }

        public static Has Ref([NotNull] string name, [CanBeNull] object tag)
        {
            return new Has(new Parameter(name), new Tag(tag));
        }

        public static Has Method(string name, params Has[] dependencies)
        {
            return new Has(new HasMethod(name, dependencies));
        }

        public static Has Property<T>(int argIndex, string propertyName)
        {
            return new Has(new HasMethod("set_" + propertyName, Arg<T>(argIndex, "value")));
        }

        public static Has Property(int argIndex, string propertyName)
        {
            return new Has(new HasMethod("set_" + propertyName, Arg(argIndex, "value")));
        }

        private Has(Parameter parameter, int argIndex)
        {
            Type = DependencyType.Arg;
            Parameter = parameter;
            ArgIndex = argIndex;
            Tag = Tag.Default;
            HasMethod = default(HasMethod);
        }

        private Has(Parameter parameter, Tag tag)
        {
            Type = DependencyType.Ref;
            Parameter = parameter;
            Tag = tag;
            ArgIndex = 0;
            HasMethod = default(HasMethod);
        }

        private Has(HasMethod hasMethod)
        {
            Type = DependencyType.Method;
            Parameter = default(Parameter);
            Tag = default(Tag);
            ArgIndex = 0;
            HasMethod = hasMethod;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case DependencyType.Arg:
                    return $"Argument: [{Parameter}, {ArgIndex}]";

                case DependencyType.Ref:
                    return $"Reference: [{Parameter}, {Tag}]";

                case DependencyType.Method:
                    return $"{HasMethod}";

                default:
                    return base.ToString();
            }
        }
    }
}
