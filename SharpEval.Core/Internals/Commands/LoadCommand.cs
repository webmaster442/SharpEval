using SharpEval.Core.Properties;

namespace SharpEval.Core.Internals.Commands;

internal class LoadCommand : ICommand
{
    public string Name => "$load";

    public string HelpMessage => Resources.CmdLoad;

    public void Execute(ICommandHost host, Arguments commandArguments)
    {
        string file = commandArguments.Get<string>(0);
        if (!File.Exists(file))
            throw new FileNotFoundException($"File doesn't exist: {file}");

        if (Path.GetExtension(file)?.ToLower() == ".cs")
            throw new InvalidOperationException($"File doesn't have .cs extension");

        var contents = File.ReadAllText(file);
        host.Evaluator.EvaluateAsync(contents).GetAwaiter().GetResult();
    }
}
