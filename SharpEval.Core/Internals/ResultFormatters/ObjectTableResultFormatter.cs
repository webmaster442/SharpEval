using System.Reflection;

namespace SharpEval.Core.Internals.ResultFormatters;

internal sealed class ObjectTableResultFormatter : TableResultFormatter
{
    private Dictionary<string, string> GetPropertyValues(object o)
    {
        var properties = o?
            .GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            ?? Array.Empty<PropertyInfo>();

        Dictionary<string, string> results = new();

        foreach (var property in properties)
        {
            if (!property.CanRead)
                continue;

            var value = property.GetValue(o)?.ToString() ?? string.Empty;
            results.Add(property.Name, value);
        }

        return results;
    }

    public override bool IsTypeMatch(object? o)
    {
        return o is not null;
    }


    public override IEnumerable<ITableRow> ToTable(object? o, AngleSystem angleSystem)
    {
        foreach (var row in GetPropertyValues(o!))
        {
            yield return new TableRow(row.Key, row.Value);
        }
    }
}
