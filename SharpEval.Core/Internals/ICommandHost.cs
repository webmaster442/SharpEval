namespace SharpEval.Core.Internals;

internal interface ICommandHost
{
    IResultWrtiter ResultWrtiter { get; }
    Settings Settings { get; }
    Evaluator Evaluator { get; }
    bool ExitFlag { get; set; }
}
