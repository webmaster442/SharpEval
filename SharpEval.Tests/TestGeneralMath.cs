using SharpEval.Core.Maths;

namespace SharpEval.Tests
{
    [TestFixture]
    internal class TestGeneralMath
    {
        [Test]
        public void TestMapLong()
        {
            long result = GeneralMath.Map(512L, 0L, 1023L, 0L, 255L);
            Assert.That(result, Is.EqualTo(127));
        }

        [Test]
        public void TestMapDouble()
        {
            double result = GeneralMath.Map(1.0, 0, 1, 1, 2);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void TestLerp()
        {
            double result = GeneralMath.Lerp(10, 20, 0.3);
            Assert.That(result, Is.EqualTo(13));
        }

        [TestCase(54, 24, 6)]
        [TestCase(105, 45, 15)]
        [TestCase(-54, 24, 6)]
        [TestCase(105, -45, 15)]
        [TestCase(-123456789, 987654321, 9)]
        [TestCase(123456789, -987654321, 9)]
        public void TestGcd(long a, long b, long expected)
        {
            long result = GeneralMath.Gcd(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(11, true)]
        [TestCase(158573, true)]
        [TestCase(11568391, true)]
        [TestCase(1108571977, true)]
        [TestCase(118908574237, true)]
        [TestCase(118908574236, false)]
        public void TestIsPrime(long n, bool expected)
        {
            bool result = GeneralMath.IsPrime(n);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
