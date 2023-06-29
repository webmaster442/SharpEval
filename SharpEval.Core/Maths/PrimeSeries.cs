namespace SharpEval.Core.Maths
{
    /// <summary>
    /// Represents prime number series
    /// </summary>
    public sealed class PrimeSeries : NumberSeriesBase
    {
        /// <inheritdoc/>
        public override long Minimum => 0;

        /// <inheritdoc/>
        public override IEnumerator<long> GetEnumerator()
        {
            for (long number = Math.Max(2, Minimum); number <= Maximum; number++)
            {
                if (IsPrime(number))
                    yield return number;
            }
        }

        private static bool IsPrime(long input)
        {
            if (input <= 1)
                return false;

            if (input <= 3)
                return true;

            if (input % 2 == 0 || input % 3 == 0)
                return false;

            for (long i = 5; i * i <= input; i += 6)
            {
                if (input % i == 0 || input % (i + 2) == 0)
                    return false;
            }

            return true;
        }
    }
}
