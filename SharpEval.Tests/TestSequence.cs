using SharpEval.Core.Maths.Sequences;

namespace SharpEval.Tests
{
    [TestFixture]
    internal class TestSequence
    {
        [Test]
        public void TestAtithmetic()
        {
            ArithmeticSequence squence = new ArithmeticSequence(4)
            {
                Maximum = 30,
                Minimum = 3,
            };

            var result = squence.ToArray();

            long[] expected = new long[]
            {
                3, 7, 11, 15, 19, 23, 27
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void TestPrimes()
        {
            PrimeSequence primes = new PrimeSequence
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
            FibonacciSequence fibonacci = new FibonacciSequence
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
            FibonacciSequence a = new FibonacciSequence
            {
                Maximum = 100,
            };
            FibonacciSequence b = new FibonacciSequence
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
            FibonacciSequence a = new FibonacciSequence
            {
                Maximum = 100,
            };
            FibonacciSequence b = new FibonacciSequence
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
            FibonacciSequence a = new FibonacciSequence
            {
                Maximum = 100,
            };
            FibonacciSequence b = new FibonacciSequence
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
            FibonacciSequence a = new FibonacciSequence
            {
                Maximum = 100,
            };
            FibonacciSequence b = new FibonacciSequence
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
