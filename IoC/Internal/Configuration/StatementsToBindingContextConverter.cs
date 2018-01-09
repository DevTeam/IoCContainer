namespace IoC.Internal.Configuration
{
    using System;
    using System.Collections.Generic;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StatementsToBindingContextConverter : IConverter<IEnumerable<Statement>, BindingContext, BindingContext>
    {
        private readonly IEnumerable<IConverter<Statement, BindingContext, BindingContext>> _statementToContextConverters;

        public StatementsToBindingContextConverter(
            [NotNull] IEnumerable<IConverter<Statement, BindingContext, BindingContext>> statementToContextConverters)
        {
            _statementToContextConverters = statementToContextConverters ?? throw new ArgumentNullException(nameof(statementToContextConverters));
        }

        public bool TryConvert(BindingContext baseContext, IEnumerable<Statement> statements, out BindingContext context)
        {
            foreach (var statement in statements)
            {
                foreach (var statementToContextConverter in _statementToContextConverters)
                {
                    if (statementToContextConverter.TryConvert(baseContext, statement, out var newContext))
                    {
                        baseContext = newContext;
                        break;
                    }
                }
            }

            context = baseContext;
            return true;
        }
    }
}
