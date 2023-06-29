using SharpEval.Core.Maths;

namespace SharpEval.Tests
{
    [TestFixture]
    internal class TestSeries
    {
        [Test]
        public void TestPrimes()
        {
            PrimeSeries primes = new PrimeSeries
            {
                Maximum = 100,
            };

            var result = primes.ToArray();

            long[] expected = new long[]
            {
                2, 3, 5, 7, 11, 13, 17, 19,
                23, 29, 31, 37, 41, 43, 47,
                53, 59, 61, 67, 71, 73, 79,
                83, 89, 97
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void TestFibonacci()
        {
            FibonacciSeries fibonacci = new FibonacciSeries
            {
                Maximum = 100,
            };

            var result = fibonacci.ToArray();

            long[] expected = new long[]
            {
                1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void TestAdd()
        {
            FibonacciSeries a = new FibonacciSeries
            {
                Maximum = 100,
            };
            FibonacciSeries b = new FibonacciSeries
            {
                Maximum = 10,
            };

            var result = (a + b).ToArray();

            long[] expected = new long[]
            {
                2, 2, 4, 6, 10, 16,
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void TestSubtract()
        {
            FibonacciSeries a = new FibonacciSeries
            {
                Maximum = 100,
            };
            FibonacciSeries b = new FibonacciSeries
            {
                Maximum = 10,
            };

            var result = (a - b).ToArray();

            long[] expected = new long[]
            {
                0, 0, 0, 0, 0, 0,
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void TestMultiply()
        {
            FibonacciSeries a = new FibonacciSeries
            {
                Maximum = 100,
            };
            FibonacciSeries b = new FibonacciSeries
            {
                Maximum = 10,
            };

            var result = (a * b).ToArray();

            long[] expected = new long[]
            {
                //1, 1, 2, 3, 5, 8
                1, 1, 4, 9, 25, 64,
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void TestDivide()
        {
            FibonacciSeries a = new FibonacciSeries
            {
                Maximum = 100,
            };
            FibonacciSeries b = new FibonacciSeries
            {
                Maximum = 10,
            };

            var result = (a / b).ToArray();

            long[] expected = new long[]
            {
                //1, 1, 2, 3, 5, 8
                1, 1, 1, 1, 1, 1,
            };

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
