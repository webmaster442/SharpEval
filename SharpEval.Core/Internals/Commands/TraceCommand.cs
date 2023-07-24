using SharpEval.Core.Properties;

namespace SharpEval.Core.Internals.Commands;

internal sealed class TraceCommand : ICommand
{
    public string Name => "$trace";

    public string HelpMessage => Resources.CmdTrace;

    public void Execute(ICommandHost host, Arguments commandArguments)
    {
        host.Settings.Trace = commandArguments.Get<bool>(0);
    }
}
