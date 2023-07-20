namespace SharpEval.Core.Maths.Sequences
{
    /// <summary>
    /// Represents prime number Sequence
    /// </summary>
    public sealed class PrimeSequence : NumberSequenceBase
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
