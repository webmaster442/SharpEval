﻿namespace SharpEval.Core;

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
    /// Writes an error message
    /// </summary>
    /// <param name="message">error message string</param>
    /// <param name="trace">Trace information for the error</param>
    void Error(string message, string trace);
    /// <summary>
    /// Writes result as a string
    /// </summary>
    /// <param name="result">result string</param>
    void Result(string result);
    /// <summary>
    /// Write result that is a table
    /// </summary>
    /// <param name="tableRows">table rows</param>
    void Result(IEnumerable<ITableRow> tableRows);

    /// <summary>
    /// Write a result, that is an SVG image
    /// </summary>
    /// <param name="image">Svg image</param>
    void Result(ISvgImage image);
}