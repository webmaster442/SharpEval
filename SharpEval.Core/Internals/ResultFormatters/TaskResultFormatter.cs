namespace SharpEval.Core.Internals.ResultFormatters;
internal class TaskResultFormatter : SingleLineResultFormatter
{
    public override string GetString(object? o, AngleSystem angleSystem)
    {
        return "This operation returns a task. To execute it use the await keyword";
    }

    public override bool IsTypeMatch(object? o)
    {
        return o is Task;
    }
}
