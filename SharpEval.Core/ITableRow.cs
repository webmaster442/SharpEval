namespace SharpEval.Core
{
    /// <summary>
    /// Represents a table row
    /// </summary>
    public interface ITableRow
    {
        /// <summary>
        /// Number of columns
        /// </summary>
        int ColumnCount { get; }

        /// <summary>
        /// Column data
        /// </summary>
        IEnumerable<string> Columns { get; }
    }
}
