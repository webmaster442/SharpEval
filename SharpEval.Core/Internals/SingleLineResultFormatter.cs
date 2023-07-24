namespace SharpEval.Core.Internals;

internal abstract class SingleLineResultFormatter : ResultFormatter
{
    public abstract string GetString(object? o, AngleSystem angleSystem);
}
