namespace IoC.Internal.Factories
{
    using System;
    using System.Reflection;

    internal class ArgFactory : IFactory
    {
        private readonly ITypeInfo _typeInfo;
        private readonly Has _dependency;
        private readonly ParameterInfo _parameter;
        private readonly int _argIndex;

        public ArgFactory(ITypeInfo typeInfo, Has dependency, ParameterInfo parameter)
        {
            _typeInfo = typeInfo;
            _dependency = dependency;
            _parameter = parameter;
            _argIndex = _dependency.ArgIndex;
        }

        public object Create(ResolvingContext context)
        {
            try
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                return context.Args[_argIndex];
            }
            catch (IndexOutOfRangeException)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                return context.ResolvingContainer.Get<IIssueResolver>().CannotResolveParameter(_typeInfo.Type, _dependency, _parameter);
            }
        }
    }
}
