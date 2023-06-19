namespace SharpEval.Core
{
    /// <summary>
    /// Result writer decoupling interface for the command parser
    /// </summary>
    /// <seealso cref="CommandParser"/>
    public interface IResultWrtiter
    {
        /// <summary>
        /// Echoes the command input
        /// </summary>
        /// <param name="currentAngleSystem">Current angle system</param>
        /// <param name="command">command text</param>
        void Echo(AngleSystem currentAngleSystem, string command);
        /// <summary>
        /// Writes an error message
        /// </summary>
        /// <param name="message">error message string</param>
        void Error(string message);
        /// <summary>
        /// Writes result as a string
        /// </summary>
        /// <param name="result">result string</param>
        void Result(string result);
        /// <summary>
        /// Write result that is a table
        /// </summary>
        /// <param name="tableRows">table rows</param>
        void ResultTable(IEnumerable<ITableRow> tableRows);
    }
}
