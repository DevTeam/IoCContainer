namespace IoC.Core
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using static TypeDescriptorExtensions;

    internal class ContainerEventToStringConverter: IConverter<ContainerEvent, IContainer, string>
    {
        private static readonly PropertyInfo PropertyInfo = Descriptor<Expression>().GetDeclaredProperties().SingleOrDefault(i => i.Name == "DebugView");
        public static readonly IConverter<ContainerEvent, IContainer, string> Shared = new ContainerEventToStringConverter();

        private ContainerEventToStringConverter() { }

        public bool TryConvert(IContainer context, ContainerEvent src, out string dst)
        {
            string text;
            switch (src.EventType)
            {
                case EventType.CreateContainer:
                    text = "was created.";
                    break;

                case EventType.DisposeContainer:
                    text = "was disposed.";
                    break;

                case EventType.RegisterDependency:
                    text = $"adds {FormatDependency(src)}.";
                    break;

                case EventType.UnregisterDependency:
                    text = $"removes {FormatDependency(src)}.";
                    break;

                case EventType.ResolverCompilation:
                    var body = src.ResolverExpression?.Body;
                    text = $"compiles {FormatDependency(src)} from:\n{GetString(GetDebugView(body))}.";
                    break;

                default:
                    dst = default(string);
                    return false;
            }

            dst = FormatPrefix(context) + text;
            return true;
        }

        [CanBeNull] private static string GetDebugView([CanBeNull] Expression expression) => 
            expression == null ? null : PropertyInfo?.GetValue(expression, null) as string ?? expression.ToString();

        [NotNull] private static string FormatDependency(ContainerEvent containerEvent)
        {
            var lifetime = containerEvent.Lifetime != null ? $" as {GetString(containerEvent.Lifetime, string.Empty)}" : string.Empty;
            return $"{FormatKeys(containerEvent)} implemented by {Quoted(GetString(containerEvent.Dependency))}{lifetime}";
        }

        [NotNull] private static string FormatKeys(ContainerEvent containerEvent) => 
            containerEvent.Keys != null ? string.Join(", ", containerEvent.Keys.Select(FormatKey)) : "";

        [NotNull] private static string FormatKey(Key key)
        {
            string tag;
            if (key.Tag == Key.AnyTag)
            {
                tag = " of any";
            }
            else
            {
                if (key.Tag == null)
                {
                    tag = string.Empty;
                }
                else
                {
                    tag = $" of {key.Tag}";
                }
            }

            return $"{key.Type.Descriptor()}{tag}";
        }

        [NotNull] private static string FormatPrefix([NotNull] IContainer container) => $"[{container}] ";

        [NotNull] private static string GetString<T>([CanBeNull] T value, string defaultString = "null")
            where T: class =>
            value?.ToString() ?? defaultString;

        [NotNull] private static string Quoted([NotNull] string text) =>
            $"\"{text}\"";
    }
}
