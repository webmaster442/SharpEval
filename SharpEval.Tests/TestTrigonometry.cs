namespace SharpEval.Tests;

[TestFixture]
public class TestTrigonometry
{
    [TestCase(0, AngleSystem.Deg, 0)]
    [TestCase(90, AngleSystem.Deg, 1)]
    [TestCase(180, AngleSystem.Deg, 0)]
    [TestCase(270, AngleSystem.Deg, -1)]
    [TestCase(360, AngleSystem.Deg, 0)]
    [TestCase(0, AngleSystem.Grad, 0)]
    [TestCase(100, AngleSystem.Grad, 1)]
    [TestCase(200, AngleSystem.Grad, 0)]
    [TestCase(300, AngleSystem.Grad, -1)]
    [TestCase(400, AngleSystem.Grad, 0)]
    public void TestSin(double input, AngleSystem angleSystem, double expected)
    {
        var result = Trigonometry.Sin(input, angleSystem);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(0, 0, AngleSystem.Deg)]
    [TestCase(1, 90, AngleSystem.Deg)]
    [TestCase(-1, -90, AngleSystem.Deg)]
    [TestCase(0, 0, AngleSystem.Grad)]
    [TestCase(1, 100, AngleSystem.Grad)]
    [TestCase(-1, -100, AngleSystem.Grad)]
    public void TestArcSin(double input, double expected, AngleSystem angleSystem)
    {
        var result = Trigonometry.ArcSin(input, angleSystem);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(0, AngleSystem.Deg, 1)]
    [TestCase(90, AngleSystem.Deg, 0)]
    [TestCase(180, AngleSystem.Deg, -1)]
    [TestCase(270, AngleSystem.Deg, 0)]
    [TestCase(360, AngleSystem.Deg, 1)]
    [TestCase(0, AngleSystem.Grad, 1)]
    [TestCase(100, AngleSystem.Grad, 0)]
    [TestCase(200, AngleSystem.Grad, -1)]
    [TestCase(300, AngleSystem.Grad, 0)]
    [TestCase(400, AngleSystem.Grad, 1)]
    public void TestCos(double input, AngleSystem angleSystem, double expected)
    {
        var result = Trigonometry.Cos(input, angleSystem);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(1, 0, AngleSystem.Deg)]
    [TestCase(0, 90, AngleSystem.Deg)]
    [TestCase(-1, 180, AngleSystem.Deg)]
    [TestCase(1, 0, AngleSystem.Grad)]
    [TestCase(0, 100, AngleSystem.Grad)]
    [TestCase(-1, 200, AngleSystem.Grad)]
    public void TestArcCos(double input, double expected, AngleSystem angleSystem)
    {
        var result = Trigonometry.ArcCos(input, angleSystem);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(0, AngleSystem.Deg, 0)]
    [TestCase(45, AngleSystem.Deg, 1)]
    [TestCase(90, AngleSystem.Deg, double.PositiveInfinity)]
    [TestCase(180, AngleSystem.Deg, 0)]
    [TestCase(270, AngleSystem.Deg, double.NegativeInfinity)]
    [TestCase(360, AngleSystem.Deg, 0)]
    [TestCase(0, AngleSystem.Grad, 0)]
    [TestCase(50, AngleSystem.Grad, 1)]
    [TestCase(100, AngleSystem.Grad, double.PositiveInfinity)]
    [TestCase(200, AngleSystem.Grad, 0)]
    [TestCase(300, AngleSystem.Grad, double.NegativeInfinity)]
    [TestCase(400, AngleSystem.Grad, 0)]
    public void TestTan(double input, AngleSystem angleSystem, double expected)
    {
        var result = Trigonometry.Tan(input, angleSystem);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(0, 0, AngleSystem.Deg)]
    [TestCase(1, 45, AngleSystem.Deg)]
    [TestCase(double.PositiveInfinity, 90, AngleSystem.Deg)]
    [TestCase(double.NegativeInfinity, -90, AngleSystem.Deg)]
    [TestCase(0, 0, AngleSystem.Grad)]
    [TestCase(1, 50, AngleSystem.Grad)]
    [TestCase(double.PositiveInfinity, 100, AngleSystem.Grad)]
    [TestCase(double.NegativeInfinity, -100, AngleSystem.Grad)]
    public void TestArcTan(double input, double expected, AngleSystem angleSystem)
    {
        var result = Trigonometry.ArcTan(input, angleSystem);
        Assert.That(result, Is.EqualTo(expected));
    }
}
