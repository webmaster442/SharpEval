namespace SharpEval.Core.Internals
{
    internal interface ICommand
    {
        void Execute(ICommandHost host, Arguments commandArguments);
        string Name { get; }
        string HelpMessage { get; }
    }
}
