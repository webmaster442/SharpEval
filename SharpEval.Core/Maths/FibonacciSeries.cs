namespace SharpEval.Core.Maths
{
    /// <summary>
    /// Represents the Fibonacci series
    /// </summary>
    public sealed class FibonacciSeries : NumberSeriesBase
    {
        /// <inheritdoc/>
        public override long Minimum => 0;

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
