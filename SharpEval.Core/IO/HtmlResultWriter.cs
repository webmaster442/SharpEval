using System.Text;

namespace SharpEval.Core.IO
{
    /// <summary>
    /// A IResultWrtiter implementation that writes as HTML
    /// </summary>
    /// <seealso cref="IResultWrtiter"/>
    public class HtmlResultWriter : IResultWrtiter
    {
        private readonly StringBuilder _buffer;

        /// <summary>
        /// Creates a new instance of HtmlResultWriter
        /// </summary>
        public HtmlResultWriter() 
        {
            _buffer = new(1024);
        }

        /// <inheritdoc/>
        public void Echo(AngleSystem currentAngleSystem, string command)
        {
            _buffer.AppendFormat("<b>({0}) {1}:</b>\r\n", currentAngleSystem, command);
        }

        /// <inheritdoc/>
        public void Error(string message)
        {
            _buffer.AppendFormat("<span style=\"color: red; font-weight: bold;\">{0}</span>\r\n", message);
        }

        /// <inheritdoc/>
        public void Result(string result)
        {
            _buffer.AppendFormat("<i>{0}</i>\r\n", result);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return _buffer.ToString();
        }
    }
}
