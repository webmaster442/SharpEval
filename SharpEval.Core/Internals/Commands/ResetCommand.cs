using SharpEval.Core.Properties;

namespace SharpEval.Core.Internals.Commands;

internal sealed class ResetCommand : ICommand
{
    public string Name => "$reset";

    public string HelpMessage => Resources.CmdReset;

    public void Execute(ICommandHost host, Arguments commandArguments)
    {
        host.Evaluator.Reset();
    }
}
