namespace SharpEval.Core.Internals;

internal sealed class EvaluatorResult
{
    public required string Error { get; init; }
    public required string Trace { get; init; }

    public required object? ResultData { get; init; }
}
