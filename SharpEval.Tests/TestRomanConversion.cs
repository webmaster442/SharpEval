namespace SharpEval.Tests;
internal class TestRomanConversion
{
    [TestCase(3724, "MMMDCCXXIV")]
    [TestCase(4999, "MMMMCMXCIX")]
    [TestCase(-3724, "-MMMDCCXXIV")]
    [TestCase(-4999, "-MMMMCMXCIX")]
    [TestCase(542, "DXLII")]
    [TestCase(0, "")]
    public void TestConvertToRoman(long input, string expected)
    {
        var result = RomanConversion.ConvertToRoman(input);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("MMMDCCXXIV", 3724)]
    [TestCase("-MMMMCMXCIX", -4999)]
    [TestCase("MMMMCMXCIX", 4999)]
    [TestCase("DXLII", 542)]
    [TestCase("", 0)]
    public void TestConvertFromRoman(string input, int expected)
    {
        var result = RomanConversion.ConvertFromRoman(input);
        Assert.That(result, Is.EqualTo(expected));
    }
}
