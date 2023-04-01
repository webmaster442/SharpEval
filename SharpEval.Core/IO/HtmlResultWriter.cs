using System.Text;

namespace SharpEval.Core.IO
{
    public class HtmlResultWriter : IResultWrtiter
    {
        private readonly StringBuilder _buffer;

        public HtmlResultWriter() 
        {
            _buffer = new(1024);
        }

        public void Echo(AngleSystem currentAngleSystem, string command)
        {
            _buffer.AppendFormat("<b>({0}) {1}:</b>\r\n", currentAngleSystem, command);
        }

        public void Error(string message)
        {
            _buffer.AppendFormat("<span style=\"color: red; font-weight: bold;\">{0}</span>\r\n", message);
        }

        public void Result(string result)
        {
            _buffer.AppendFormat("<i>{0}</i>\r\n", result);
        }

        public override string ToString()
        {
            return _buffer.ToString();
        }
    }
}
