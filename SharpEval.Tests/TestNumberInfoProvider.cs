namespace SharpEval.Tests;

[TestFixture]
public class TestNumberInfoProvider
{
    [TestCase(short.MinValue, 16, "80 00")]
    [TestCase(int.MinValue, 32, "80 00 00 00")]
    [TestCase(byte.MaxValue, 8, "ff")]
    [TestCase(ushort.MaxValue, 16, "ff ff")]
    [TestCase(uint.MaxValue, 32, "ff ff ff ff")]
    [TestCase(ulong.MaxValue, 64, "ff ff ff ff ff ff ff ff")]
    [TestCase(double.MinValue, 64, "ff ef ff ff ff ff ff ff")]
    [TestCase(double.MaxValue, 64, "7f ef ff ff ff ff ff ff")]
    [TestCase(float.MaxValue, 32, "7f 7f ff ff")]
    [TestCase(float.MinValue, 32, "ff 7f ff ff")]
    public void Test(object number, int bits, string hex)
    {
        var result = NumberInfoProvider.GetInfo(number);
        Assert.Multiple(() =>
        {
            Assert.That(result.Hexadecimal, Is.EqualTo(hex));
            Assert.That(result.Bits, Is.EqualTo(bits));
        });
    }
}
