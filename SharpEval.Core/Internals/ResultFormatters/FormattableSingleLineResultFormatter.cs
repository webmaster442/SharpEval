using System.Diagnostics;
using System.Globalization;

namespace SharpEval.Core.Internals.ResultFormatters;

internal sealed class FormattableSingleLineResultFormatter : SingleLineResultFormatter
{
    public override string GetString(object? o, AngleSystem angleSystem)
    {
        if (o is IFormattable formattable)
            return formattable.ToString(string.Empty, CultureInfo.InvariantCulture);

        throw new UnreachableException("type should be IFormattable");
    }

    public override bool IsTypeMatch(object? o)
    {
        return o is IFormattable;
    }
}
