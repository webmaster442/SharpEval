using System.Collections;
using System.Globalization;
using System.Reflection;

namespace SharpEval.Core.Internals;

internal sealed record class EvaluatorResult
{
    public enum ResultType
    {
        Table,
        SingleLine,
        Null,
    }

    public required string Error { get; init; }
    public required object? ResultData { get; init; }

    public override string ToString()
    {
        if (ResultData == null)
            return string.Empty;

        return ResultData switch
        {
            IFormattable formattable => formattable.ToString("", CultureInfo.InvariantCulture),
            _ => ResultData?.ToString() ?? string.Empty
        };
    }

    public ResultType ResultTypeInformation
    {
        get
        {
            if (ResultData == null)
                return ResultType.Null;

            if (ResultData is IEnumerable
                || ResultData is not IFormattable)
            {
                return ResultType.Table;
            }
            return ResultType.SingleLine;
        }
    }

    public IEnumerable<ITableRow> ToTable()
    {
        if (ResultData is IDictionary dictionary)
        {
            foreach (DictionaryEntry item in dictionary)
            {
                yield return new TableRow(item.Key?.ToString() ?? string.Empty, item.Value?.ToString() ?? string.Empty);
            }
        }
        else if (ResultData is IEnumerable<IFormattable> formattable)
        {
            foreach (var row in formattable) 
            {
                yield return new TableRow(row.ToString("", CultureInfo.InvariantCulture));
            }
        }
        else if (ResultData is IEnumerable enumerable)
        {
            foreach (var row in enumerable)
            {
                yield return new TableRow(row.ToString() ?? string.Empty);
            }
        }
        else
        {
            Dictionary<string, string> objectData = GetObjectData();
            foreach (var row in objectData)
            {
                yield return new TableRow(row.Key, row.Value);
            }
        }
    }

    private Dictionary<string, string> GetObjectData()
    {
        var properties = ResultData?
            .GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            ?? Array.Empty<PropertyInfo>();

        Dictionary<string, string> results = new();

        foreach (var property in properties)
        {
            if (!property.CanRead)
                continue;
            
            var value = property.GetValue(ResultData)?.ToString() ?? string.Empty;
            results.Add(property.Name, value);
        }

        return results;
    }
}
