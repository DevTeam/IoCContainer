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
        internal readonly Scope Scope;

        public static Has Arg<T>([NotNull] string name, int argIndex)
        {
            return new Has(new Parameter(name, typeof(T)), argIndex);
        }

        public static Has Arg([NotNull] string name, int argIndex)
        {
            return new Has(new Parameter(name), argIndex);
        }

        public static Has Ref<T>([NotNull] string name, [CanBeNull] object tag, Scope scope = Scope.Current)
        {
            return new Has(new Parameter(name, typeof(T)), new Tag(tag), scope);
        }

        public static Has Ref([NotNull] string name, [CanBeNull] object tag, Scope scope = Scope.Current)
        {
            return new Has(new Parameter(name), new Tag(tag), scope);
        }

        public static Has Method(string name, params Has[] dependencies)
        {
            return new Has(new HasMethod(name, dependencies));
        }

        public static Has Property<T>(string propertyName, int argIndex)
        {
            return new Has(new HasMethod("set_" + propertyName, Arg<T>("value", argIndex)));
        }

        public static Has Property(string propertyName, int argIndex)
        {
            return new Has(new HasMethod("set_" + propertyName, Arg("value", argIndex)));
        }

        private Has(Parameter parameter, int argIndex)
        {
            Type = DependencyType.Arg;
            Parameter = parameter;
            ArgIndex = argIndex;
            Tag = Tag.Default;
            HasMethod = default(HasMethod);
            Scope = Scope.Current;
        }

        private Has(Parameter parameter, Tag tag, Scope scope)
        {
            Type = DependencyType.Ref;
            Parameter = parameter;
            Tag = tag;
            ArgIndex = 0;
            HasMethod = default(HasMethod);
            Scope = scope;
        }

        private Has(HasMethod hasMethod)
        {
            Type = DependencyType.Method;
            Parameter = default(Parameter);
            Tag = default(Tag);
            ArgIndex = 0;
            HasMethod = hasMethod;
            Scope = Scope.Current;
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
