// ReSharper disable IdentifierTypo
namespace IoC.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Issues;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StatementToBindingConverter: IConverter<Statement, BindingContext, BindingContext>
    {
        private static readonly Regex BindingRegex = new Regex(@"Bind<(?<contractTypes>[\w.,\s<>]+)>\(\)(?<config>.*)\.To<\s*(?<instanceType>[\w.<>]+)\s*>\(\)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);
        private readonly IConverter<string, BindingContext, Type> _typeConverter;
        [NotNull] private readonly IConverter<string, Statement, Lifetime> _lifetimeConverter;
        [NotNull] private readonly IConverter<string, Statement, IEnumerable<object>> _tagsConverter;
        private readonly ICannotParseType _сannotParseType;

        public StatementToBindingConverter(
            [NotNull] IConverter<string, BindingContext, Type> typeConverter,
            [NotNull] IConverter<string, Statement, Lifetime> lifetimeConverter,
            [NotNull] IConverter<string, Statement, IEnumerable<object>> tagsConverter,
            [NotNull] ICannotParseType сannotParseType)
        {
            _typeConverter = typeConverter ?? throw new ArgumentNullException(nameof(typeConverter));
            _lifetimeConverter = lifetimeConverter ?? throw new ArgumentNullException(nameof(lifetimeConverter));
            _tagsConverter = tagsConverter ?? throw new ArgumentNullException(nameof(tagsConverter));
            _сannotParseType = сannotParseType ?? throw new ArgumentNullException(nameof(сannotParseType));
        }

        public bool TryConvert(BindingContext baseContext, Statement statement, out BindingContext context)
        {
            if (baseContext == null) throw new ArgumentNullException(nameof(baseContext));
            var bindingMatch = BindingRegex.Match(statement.Text);
            if (bindingMatch.Success)
            {
                var instanceTypeName = bindingMatch.Groups["instanceType"].Value;
                if (!_typeConverter.TryConvert(baseContext, instanceTypeName, out var instanceType))
                {
                    instanceType = _сannotParseType.Resolve(statement.Text, statement.LineNumber, statement.Position, instanceTypeName);
                }

                var contractTypes = new List<Type>();
                foreach (var contractTypeName in bindingMatch.Groups["contractTypes"]?.Value.Split(Separators.Type) ?? Enumerable.Empty<string>())
                {
                    if (!_typeConverter.TryConvert(baseContext, contractTypeName, out var contractType))
                    {
                        contractType = _сannotParseType.Resolve(statement.Text, statement.LineNumber, statement.Position, contractTypeName);
                    }

                    contractTypes.Add(contractType);
                }

                var lifetime = Lifetime.Transient;
                var tags = new List<object>();
                var config = bindingMatch.Groups["config"]?.Value;
                if (config != null)
                {
                    if (_lifetimeConverter.TryConvert(statement, config, out var curLifetime))
                    {
                        lifetime = curLifetime;
                    }

                    if (_tagsConverter.TryConvert(statement, config, out var curTags))
                    {
                        tags.AddRange(curTags);
                    }

                    var binding = new Binding(contractTypes.Distinct().ToArray(), lifetime, tags.ToArray(), instanceType);

                    context = new BindingContext(
                        baseContext.Assemblies,
                        baseContext.Namespaces,
                        baseContext.Bindings.Concat(Enumerable.Repeat(binding, 1)).Distinct());

                    return true;
                }
            }

            context = default(BindingContext);
            return false;
        }
    }
}
