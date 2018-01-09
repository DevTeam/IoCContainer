namespace IoC.Internal.Configuration
{
    using System.Linq;
    using System.Text.RegularExpressions;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StatementToNamespacesConverter : IConverter<Statement, BindingContext, BindingContext>
    {
        private static readonly Regex Regex = new Regex(@"using\s+([\w.,\s]+)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public bool TryConvert(BindingContext baseContext, Statement statement, out BindingContext context)
        {
            var match = Regex.Match(statement.Text);
            if (match.Success)
            {
                var namespaces = match.Groups[1].Value.Split(Separators.Namespace).Select(i => i.Trim()).Where(i => !string.IsNullOrWhiteSpace(i));
                context = new BindingContext(
                    baseContext.Assemblies,
                    baseContext.Namespaces.Concat(namespaces).Distinct(),
                    baseContext.Bindings);
                return true;
            }

            context = default(BindingContext);
            return false;
        }
    }
}
