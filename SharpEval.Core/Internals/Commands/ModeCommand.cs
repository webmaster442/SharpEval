using SharpEval.Core.Properties;

namespace SharpEval.Core.Internals.Commands
{
    internal sealed class ModeCommand : ICommand
    {
        public string Name => "$mode";

        public string HelpMessage => Resources.CmdMode;

        public void Execute(ICommandHost host, Arguments commandArguments)
        {
            host.Settings.CurrentAngleSystem = commandArguments.GetEnum<AngleSystem>(0);
        }
    }

}
