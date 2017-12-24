namespace IoC
{
    using System;

    [PublicAPI]
    public struct Has
    {
        internal readonly Parameter Parameter;
        internal readonly object Tag;
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

        public static Has Ref<T>([NotNull] string name, [NotNull] object tag, Scope scope = Scope.Current)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            return new Has(new Parameter(name, typeof(T)), tag, scope);
        }

        public static Has Ref<T>([NotNull] string name, Scope scope)
        {
            return new Has(new Parameter(name, typeof(T)), null, scope);
        }

        public static Has Ref([NotNull] string name, [NotNull] object tag, Scope scope = Scope.Current)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            return new Has(new Parameter(name), tag, scope);
        }

        public static Has Ref([NotNull] string name, Scope scope)
        {
            return new Has(new Parameter(name), null, scope);
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
            Tag = null;
            HasMethod = default(HasMethod);
            Scope = Scope.Current;
        }

        private Has(Parameter parameter, object tag, Scope scope)
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
            Tag = null;
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
