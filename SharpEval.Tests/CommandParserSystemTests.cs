using SharpEval.Tests.Internals;
using SharpEval.Tests.Properties;

namespace SharpEval.Tests;

[TestFixture]
public class CommandParserSystemTests
{
    private TestIO _testIO;
    private CommandParser _sut;
    private IApiClient _apiClientMock;

    [SetUp]
    public void Setup()
    {
        _testIO = new TestIO();
        _apiClientMock = Substitute.For<IApiClient>();
        _sut = new CommandParser(_testIO, _testIO, _apiClientMock);
    }

    [Test]
    public async Task TestPlot()
    {
        _testIO.SetInput("PlotSize(300, 300)",
                         "PlotTitle(\"Test\")",
                         "PlotBackground(\"#c0c0c0\")",
                         "PlotFunction(x => 1/x, 0, 1, 0.05, \"1/x\")",
                         "PlotPrint()");

        await _sut.RunAsync();
        var lastEvent = _testIO.Events.Pop();
        Assert.Multiple(() =>
        {
            Assert.That(lastEvent.EventType, Is.EqualTo(TestIO.EventType.Image));
            Assert.That(lastEvent.Result, Is.EqualTo(Resources.plot_svg));
        });
    }

    [TestCase("", "")]
    [TestCase("Vector(1, 3)", "x: 1 y: 3")]
    [TestCase("Vector(Complex(1, 3))", "x: 1 y: 3")]
    [TestCase("Vector(1, 3, 2)", "x: 1 y: 3 z: 2")]
    [TestCase("Complex(0, 1)", "0 + 1i\r\nr = 1 φ = 90")]
    [TestCase("Complex(Vector(0, 1))", "0 + 1i\r\nr = 1 φ = 90")]
    [TestCase("Date(2000, 1, 1) - Date(1995, 1, 1)", "1826 days 0 hours 0 minutes 0 seconds\r\nYears (Aproximated): 4\r\nMonths (Aproximated): 59")]
    [TestCase("Sqrt(Complex(4,3))", "2.1213203435596424 + 0.7071067811865476i\r\nr = 2.23606797749979 φ = 18.434948822922014")]
    [TestCase("Pow(Complex(4,3), 2)", "7.000000000000001 + 24i\r\nr = 25 φ = 73.73979529168804")]
    public async Task TestSingleLineResult(string input, string expected)
    {
        _testIO.SetInput(input);
        await _sut.RunAsync();
        var lastEvent = _testIO.Events.Pop();
        Assert.Multiple(() =>
        {
            Assert.That(lastEvent.EventType, Is.EqualTo(TestIO.EventType.Result));
            Assert.That(lastEvent.Result, Is.EqualTo(expected));
        });
    }

    [TestCase("Primes(1, 10)", 5)]
    [TestCase("Fibonacci(30)", 9)]
    public async Task TestMultiLineResult(string input, int expectedCount)
    {
        _testIO.SetInput(input);
        await _sut.RunAsync();
        var lastEvent = _testIO.Events.Pop();
        Assert.Multiple(() =>
        {
            Assert.That(lastEvent.EventType, Is.EqualTo(TestIO.EventType.Table));
            Assert.That(lastEvent.Result.Split(Environment.NewLine), Has.Length.EqualTo(expectedCount));
        });

    }

    [TestCase("3+", "(1,3): error CS1733: Expected expression")]
    [TestCase("Vector(1, 2) + Vector(1, 2, 3)", "(1,1): error CS0019: Operator '+' cannot be applied to operands of type 'Vector2' and 'Vector3'")]
    public async Task TestErrorResult(string input, string expected)
    {
        _testIO.SetInput(input);
        await _sut.RunAsync();
        var lastEvent = _testIO.Events.Pop();
        Assert.Multiple(() =>
        {
            Assert.That(lastEvent.EventType, Is.EqualTo(TestIO.EventType.Error));
            Assert.That(lastEvent.Result, Is.EqualTo(expected));
        });
    }

    [Test]
    public async Task TestEchoCommandOff()
    {
        _testIO.SetInput("$echo off", "3+2");
        await _sut.RunAsync();
        var lastEvent = _testIO.Events.Pop();
        Assert.Multiple(() =>
        {
            Assert.That(_testIO.HasEventType(TestIO.EventType.Echo), Is.False);
            Assert.That(lastEvent.Result, Is.EqualTo("5"));
        });
    }

    [Test]
    public async Task TestEchoCommandOn()
    {
        _testIO.SetInput("$echo on", "3+2");
        await _sut.RunAsync();
        var resultEvent = _testIO.Events.Pop();
        var echoEvent = _testIO.Events.Pop();
        Assert.Multiple(() =>
        {
            Assert.That(resultEvent.EventType, Is.EqualTo(TestIO.EventType.Result));
            Assert.That(resultEvent.Result, Is.EqualTo("5"));
            Assert.That(echoEvent.EventType, Is.EqualTo(TestIO.EventType.Echo));
            Assert.That(echoEvent.Result, Is.EqualTo("3+2"));
        });
    }

    [Test]
    public async Task TestModeCommand()
    {
        _testIO.SetInput("$mode rad");
        await _sut.RunAsync();
        Assert.That(_sut.Settings.CurrentAngleSystem, Is.EqualTo(AngleSystem.Rad));
    }

    [Test]
    public async Task TestResetCommand()
    {
        _testIO.SetInput("var x = 11", "$reset");
        await _sut.RunAsync();
        Assert.That(_sut.Variables, Is.Empty);
    }

    [Test]
    public async Task TestVarsCommand()
    {
        _testIO.SetInput("var x = 11", "$vars");
        await _sut.RunAsync();
        var result = _testIO.Events.Pop();
        Assert.Multiple(() =>
        {
            Assert.That(result.EventType, Is.EqualTo(TestIO.EventType.Result));
            Assert.That(result.Result, Is.EqualTo("x = 11 //Int32"));
        });
    }

}
