namespace SharpEval.Core.Internals.ResultFormatters;

internal sealed class NullSingleLineResultFormatter : SingleLineResultFormatter
{
    public override string GetString(object? o, AngleSystem angleSystem)
    {
        return string.Empty;
    }

    public override bool IsTypeMatch(object? o)
    {
        return o is null;
    }
}
