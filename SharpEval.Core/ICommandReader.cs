namespace SharpEval.Core
{
    public interface ICommandReader
    {
        IEnumerable<string> InputLines { get; }
    }
}
