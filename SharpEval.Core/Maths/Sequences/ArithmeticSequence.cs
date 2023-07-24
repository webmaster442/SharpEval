namespace SharpEval.Core.Maths.Sequences;

/// <summary>
/// Represents a generic arithmetic sequence
/// </summary>
public sealed class ArithmeticSequence : NumberSequenceBase
{
    /// <summary>
    /// Creates a new Arithmetic Sequence with a given difference between the items
    /// </summary>
    /// <param name="difference">difference to use</param>
    public ArithmeticSequence(long difference)
    {
        Difference = difference;
    }

    /// <inheritdoc/>
    public override long Minimum
    {
        get;
        set;
    }

    /// <summary>
    /// Difference
    /// </summary>
    public long Difference { get; }

    /// <inheritdoc/>
    public override IEnumerator<long> GetEnumerator()
    {
        yield return Minimum;

        long number = Minimum;
        do
        {
            number += Difference;
            yield return number;
        }
        while ((number + Difference) < Maximum);
    }
}
