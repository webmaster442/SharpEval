using SharpEval.Core.Properties;

namespace SharpEval.Core.Internals.Commands
{
    internal sealed class EchoCommand : ICommand
    {
        public string Name => "$echo";

        public string HelpMessage => Resources.CmdEcho;

        public void Execute(ICommandHost host, Arguments commandArguments)
        {
            host.Settings.EchoExpression = commandArguments.Get<bool>(0);
        }
    }
}
