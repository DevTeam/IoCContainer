namespace IoC.Core.Configuration
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StatementToReferencesConverter : IConverter<Statement, BindingContext, BindingContext>
    {
        private static readonly Regex Regex = new Regex(@"ref\s+([\w.,\s]+)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public bool TryConvert(BindingContext baseContext, Statement statement, out BindingContext context)
        {
            if (baseContext == null) throw new ArgumentNullException(nameof(baseContext));
            var match = Regex.Match(statement.Text);
            if (match.Success)
            {
                var assemblies = match.Groups[1].Value.Split(Separators.Assembly).Select(Types.LoadAssembly);
                context = new BindingContext(
                    baseContext.Assemblies.Concat(assemblies).Distinct(),
                    baseContext.Namespaces,
                    baseContext.Bindings);
                return true;
            }

            context = default(BindingContext);
            return false;
        }
    }
}
