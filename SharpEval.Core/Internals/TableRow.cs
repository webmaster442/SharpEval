namespace SharpEval.Core.Internals;

internal sealed class TableRow : ITableRow
{
    private readonly string[] _items;

    public TableRow(params string[] items)
    {
        _items = items;
    }

    public int ColumnCount => _items.Length;

    public IEnumerable<string> Columns => _items;

    public override bool Equals(object? obj)
    {
        return Equals(obj as ITableRow);
    }

    public bool Equals(ITableRow? other)
    {
        if (other is null
            || other.ColumnCount != ColumnCount)
        {
            return false;
        }

        var ziped = Columns.Zip(other.Columns);

        foreach (var item in ziped) 
        {
            if (item.First != item.Second) return false;
        }

        return true;

    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        foreach (var item in _items) 
        {
            hash.Add(item);
        }
        return hash.ToHashCode();
    }

    public override string ToString()
    {
        return string.Join(' ', _items);
    }
}