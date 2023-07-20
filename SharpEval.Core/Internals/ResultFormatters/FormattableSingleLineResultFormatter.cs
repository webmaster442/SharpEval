using System.Globalization;

namespace SharpEval.Core.Internals.ResultFormatters
{
    internal sealed class FormattableSingleLineResultFormatter : SingleLineResultFormatter
    {
        public override string GetString(object? o, CultureInfo culture)
        {
            if (o is IFormattable formattable)
                return formattable.ToString(string.Empty, culture);

            return string.Empty;
        }

        public override bool IsTypeMatch(object? o)
        {
            return o is IFormattable;
        }
    }
}
