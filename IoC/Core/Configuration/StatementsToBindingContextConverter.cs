namespace IoC.Core.Configuration
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

        public bool TryConvert(BindingContext baseContext, IEnumerable<Statement> statements, out BindingContext dst)
        {
            if (baseContext == null) throw new ArgumentNullException(nameof(baseContext));
            if (statements == null) throw new ArgumentNullException(nameof(statements));
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

            dst = baseContext;
            return true;
        }
    }
}
