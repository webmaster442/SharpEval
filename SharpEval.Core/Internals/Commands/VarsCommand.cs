using SharpEval.Core.Properties;

namespace SharpEval.Core.Internals.Commands
{
    internal sealed class VarsCommand : ICommand
    {
        public string Name => "$vars";

        public string HelpMessage => Resources.CmdVars;

        public void Execute(ICommandHost host, Arguments commandArguments)
        {
            foreach (var variable in host.Evaluator.Variables)
            {
                host.ResultWrtiter.Result($"{variable.Key} = {variable.Value} //{variable.Value.GetType().Name}");
            }
        }
    }

}
