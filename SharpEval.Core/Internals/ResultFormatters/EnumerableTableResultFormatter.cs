using System.Collections;
using System.Globalization;

namespace SharpEval.Core.Internals.ResultFormatters
{
    internal class EnumerableTableResultFormatter : TableResultFormatter
    {
        public override bool IsTypeMatch(object? o)
        {
            return o is IEnumerable;
        }

        public override IEnumerable<ITableRow> ToTable(object? o, CultureInfo culture)
        {
            if (o is not IEnumerable enumerable)
                yield break;

            foreach (var row in enumerable)
            {
                if (row is IFormattable formattable)
                    yield return new TableRow(formattable.ToString("", CultureInfo.InvariantCulture));
                else
                    yield return new TableRow(row.ToString() ?? string.Empty);
            }
        }
    }
}
