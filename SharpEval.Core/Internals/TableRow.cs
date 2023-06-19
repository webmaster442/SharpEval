namespace SharpEval.Core.Internals;

internal class TableRow : ITableRow
{
    private readonly string[] _items;

    public TableRow(params string[] items)
    {
        _items = items;
    }

    public TableRow(IEnumerable<object> items)
    {
        _items = items.Select(x => x?.ToString() ?? string.Empty).ToArray();
    }

    public int ColumnCount => _items.Length;

    public IEnumerable<string> Columns => _items;
}