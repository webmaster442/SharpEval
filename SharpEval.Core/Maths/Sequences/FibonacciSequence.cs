namespace SharpEval.Core.Maths.Sequences
{
    /// <summary>
    /// Represents the Fibonacci Sequence
    /// </summary>
    public sealed class FibonacciSequence : NumberSequenceBase
    {
        /// <inheritdoc/>
        public override long Minimum 
        {
            get => 0; 
            set => throw new NotSupportedException("Can't change minumum");
        }

        /// <inheritdoc/>
        public override IEnumerator<long> GetEnumerator()
        {
            long current = 1;
            long next = 1;

            while (current <= Maximum)
            {
                if (current >= Minimum)
                    yield return current;

                long temp = current;
                current = next;
                next = temp + next;
            }
        }
    }
}
