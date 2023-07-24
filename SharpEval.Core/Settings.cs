namespace SharpEval.Core;

/// <summary>
/// Represents application settings
/// </summary>
public sealed class Settings
{
    /// <summary>
    /// The current angle system
    /// </summary>
    public AngleSystem CurrentAngleSystem { get; set; }
    /// <summary>
    /// Enable or disable command echo in output
    /// </summary>
    public bool EchoExpression { get; set; }

    /// <summary>
    /// Report error trace information
    /// </summary>
    public bool Trace { get; set; }

    /// <summary>
    /// Creates a new instance of Settings.
    /// </summary>
    public Settings()
    {
        CurrentAngleSystem = AngleSystem.Deg;
        EchoExpression = true;
        Trace = false;
    }
}
