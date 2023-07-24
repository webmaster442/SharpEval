namespace SharpEval.Core;

/// <summary>
/// Command reader decoupling interface for the command parser
/// </summary>
/// <seealso cref="CommandParser"/>
public interface ICommandReader
{
    /// <summary>
    /// The Input lines to be parsed as commands
    /// </summary>
    IEnumerable<string> InputLines { get; }
}
