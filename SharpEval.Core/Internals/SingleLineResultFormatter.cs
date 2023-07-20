using System.Globalization;

namespace SharpEval.Core.Internals
{
    internal abstract class SingleLineResultFormatter : ResultFormatter
    {
        public abstract string GetString(object? o, CultureInfo culture);
    }
}
