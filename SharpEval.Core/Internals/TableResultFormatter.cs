using System.Globalization;

namespace SharpEval.Core.Internals
{
    internal abstract class TableResultFormatter : ResultFormatter
    {
        public abstract IEnumerable<ITableRow> ToTable(object? o, CultureInfo culture);
    }
}
