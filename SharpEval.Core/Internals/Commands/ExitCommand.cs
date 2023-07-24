using SharpEval.Core.Properties;

namespace SharpEval.Core.Internals.Commands;

internal sealed class ExitCommand : ICommand
{
    public string Name => "$exit";

    public string HelpMessage => Resources.CmdExit;

    public void Execute(ICommandHost host, Arguments commandArguments)
    {
        host.ExitFlag = true;
    }
}
