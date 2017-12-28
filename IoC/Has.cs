namespace IoC
{
    using System;

    [PublicAPI]
    public struct Has
    {
        internal readonly Parameter Parameter;
        [CanBeNull] internal readonly object Tag;
        internal readonly int ArgIndex;
        internal readonly DependencyType Type;
        internal readonly HasMethod HasMethod;
        internal readonly Scope Scope;

        public static Has Arg<T>([NotNull] string name, int argIndex)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
            return new Has(new Parameter(name, typeof(T)), argIndex);
        }

        public static Has Arg([NotNull] string name, int argIndex)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
            return new Has(new Parameter(name), argIndex);
        }

        public static Has Ref<T>([NotNull] string name, [NotNull] object tag, Scope scope = Scope.Current)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            return new Has(new Parameter(name, typeof(T)), tag, scope);
        }

        public static Has Ref<T>([NotNull] string name, Scope scope)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return new Has(new Parameter(name, typeof(T)), null, scope);
        }

        public static Has Ref([NotNull] string name, [NotNull] object tag, Scope scope = Scope.Current)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            return new Has(new Parameter(name), tag, scope);
        }

        public static Has Ref([NotNull] string name, Scope scope)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return new Has(new Parameter(name), null, scope);
        }

        public static Has Method([NotNull] string name, [NotNull] params Has[] dependencies)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (dependencies == null) throw new ArgumentNullException(nameof(dependencies));
            return new Has(new HasMethod(name, dependencies));
        }

        public static Has Property<T>([NotNull] string propertyName, int argIndex)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
            return new Has(new HasMethod("set_" + propertyName, Arg<T>("value", argIndex)));
        }

        public static Has Property([NotNull] string propertyName, int argIndex)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
            return new Has(new HasMethod("set_" + propertyName, Arg("value", argIndex)));
        }

        private Has(Parameter parameter, int argIndex)
        {
            if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
            Type = DependencyType.Arg;
            Parameter = parameter;
            ArgIndex = argIndex;
            Tag = null;
            HasMethod = default(HasMethod);
            Scope = Scope.Current;
        }

        private Has(Parameter parameter, [CanBeNull] object tag, Scope scope)
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
