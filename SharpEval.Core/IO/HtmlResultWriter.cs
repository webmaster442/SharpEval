﻿
using System.Text;

namespace SharpEval.Core.IO;

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
    public void Error(string message, string trace = "")
    {
        _buffer.AppendFormat("<span style=\"color: red; font-weight: bold;\">{0}</span>\r\n", message);
        _buffer.AppendFormat("<code><pre>{0}</pre></code>\r\n", trace);
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
    public void Result(IEnumerable<ITableRow> tableRows)
    {
        _buffer.Append("<table>\r\n");
        foreach (ITableRow row in tableRows)
        {
            _buffer.Append("<tr>\r\n");
            foreach (var column in row.Columns)
            {
                _buffer.AppendFormat("<td>{0}</td>\r\n", column);
            }
            _buffer.Append("</tr>\r\n");
        }
        _buffer.Append("</table>\r\n");
    }

    /// <inheritdoc/>
    public void Result(ISvgImage image)
    {
        _buffer.Append("<figure>");
        _buffer.Append(image.Data);
        _buffer.Append("</figure>");
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return _buffer.ToString();
    }
}
