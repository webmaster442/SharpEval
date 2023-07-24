namespace SharpEval.Tests;

[TestFixture]
public class TestFraction
{
    [Test]
    public void TestConstructorException()
    {
        Assert.Throws<DivideByZeroException>(() => new Fraction(1, 0));
    }

    [TestCase(1, 2, "1/2")]
    [TestCase(2, 4, "1/2")]
    [TestCase(-1, 2, "-1/2")]
    [TestCase(1, -2, "-1/2")]
    [TestCase(-1, -2, "1/2")]
    public void TestConstructor(long numerator, long denominator, string expected)
    {
        Fraction fraction = new Fraction(numerator, denominator);
        Assert.That(fraction.ToString(), Is.EqualTo(expected));
    }

    [TestCase("1", "1")]
    [TestCase("-1", "-1")]
    [TestCase("1/2", "1/2")]
    [TestCase("-1/2", "-1/2")]
    [TestCase("1/-2", "-1/2")]
    public void TestParseGood(string input, string expected)
    {
        Fraction f = Fraction.Parse(input, CultureInfo.InvariantCulture);
        Assert.That(f.ToString(), Is.EqualTo(expected));
    }

    [TestCase("1/2", true)]
    [TestCase("1", true)]
    [TestCase("1.0", false)]
    [TestCase("1.0/", false)]
    [TestCase("2.0/0", false)]
    [TestCase("/2", false)]
    [TestCase("2/0", false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    public void TestTryParse(string input, bool expected)
    {
        bool result = Fraction.TryParse(input, CultureInfo.InvariantCulture, out _);
        Assert.That(result, Is.EqualTo(expected));
    }
}
