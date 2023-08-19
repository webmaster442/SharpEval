namespace SharpEval.Tests;
internal class TestRomanConversion
{
    [TestCase(3724, "MMMDCCXXIV")]
    [TestCase(4999, "MMMMCMXCIX")]
    [TestCase(-3724, "-MMMDCCXXIV")]
    [TestCase(-4999, "-MMMMCMXCIX")]
    [TestCase(0, "")]
    public void TestConvertToRoman(long input, string expected)
    {
        var result = RomanConversion.ConvertToRoman(input);
        Assert.That(result, Is.EqualTo(expected));
    }
}
