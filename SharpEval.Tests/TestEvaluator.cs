﻿using Moq;

using SharpEval.Core.Internals;

namespace SharpEval.Tests;

[TestFixture]
internal class TestEvaluator
{
    private Evaluator _sut;
    private Mock<ISettingsProvider> _settingProviderMock;
    private Settings _settings;

    [SetUp]
    public void Setup()
    {
        _settings = new Settings
        {
            CurrentAngleSystem = AngleSystem.Deg,
        };
        _settingProviderMock = new Mock<ISettingsProvider>(MockBehavior.Strict);
        _settingProviderMock.Setup(x => x.GetSettings()).Returns(_settings);
        _sut = new Evaluator(_settingProviderMock.Object);
    }

    [TestCase("Pi", "3.141592653589793")]
    [TestCase("E", "2.718281828459045")]
    [TestCase("E*2", "5.43656365691809")]
    [TestCase("(2+2)*3", "12")]
    [TestCase("Abs(12)", "12")]
    [TestCase("Abs(-12)", "12")]
    [TestCase("Sign(45)", "1")]
    [TestCase("Sign(-45)", "-1")]
    [TestCase("Sin(90)", "1")]
    [TestCase("ArcSin(1)", "90")]
    [TestCase("Cos(90)", "0")]
    [TestCase("ArcCos(0)", "90")]
    [TestCase("Tan(45)", "1")]
    [TestCase("ArcTan(1)", "45")]
    [TestCase("Ln(E)", "1")]
    [TestCase("Log10(10)", "1")]
    [TestCase("Log(8, 2)", "3")]
    [TestCase("Pow(2, 2)", "4")]
    [TestCase("Sqrt(9)", "3")]
    [TestCase("Ceiling(1.9)", "2")]
    [TestCase("Floor(1.9)", "1")]
    [TestCase("Map(512L, 0L, 1023L, 0L, 255L)", "127")]
    [TestCase("Map(1.0, 0, 1, 1, 2)", "2")]
    [TestCase("Lerp(10, 20, 0.3)", "13")]
    [TestCase("Gcd(54, 24)", "6")]
    [TestCase("Fraction(1,2)+Fraction(1,4)", "3/4")]
    [TestCase("Fraction(1,2)-Fraction(1,4)", "1/4")]
    [TestCase("Fraction(1,2)*Fraction(1,4)", "1/8")]
    [TestCase("Fraction(1,2)/Fraction(1,4)", "2")]
    [TestCase("Fraction(1,2)%Fraction(1,4)", "0")]
    [TestCase("Fraction(1,8)<Fraction(1,4)", "True")]
    [TestCase("Fraction(1,8)<=Fraction(1,4)", "True")]
    [TestCase("Fraction(1,8)>Fraction(1,4)", "False")]
    [TestCase("Fraction(1,8)>=Fraction(1,4)", "False")]
    [TestCase("Fraction(2,4) == Fraction(1,2)", "True")]
    [TestCase("Reciproc(2.0)", "0.5")]
    [TestCase("Reciproc(0.5)", "2")]
    [TestCase("Reciproc(Fraction(3,1))", "1/3")]
    [TestCase("Reciproc(Fraction(1,3))", "3")]
    [TestCase("FromHex(\"ff\")", "255")]
    [TestCase("FromHex(\"FF\")", "255")]
    [TestCase("ToHex(255)", "ff")]
    [TestCase("ToBin(15)", "1111")]
    [TestCase("FromBin(\"111\")", "7")]
    [TestCase("FromOct(\"17\")", "15")]
    [TestCase("ToOct(15)", "17")]
    [TestCase("DataSet(1, 2, 4).Count()", "3")]
    [TestCase("DataSet(1, 2, 3).Sum()", "6")]
    [TestCase("DataSet(1, 2, 4).Min()", "1")]
    [TestCase("DataSet(1, 2, 4).Max()", "4")]
    [TestCase("DataSet(1, 2, 3).Average()", "2")]
    [TestCase("DataSet(1, 2, 3).Median()", "2")]
    [TestCase("DataSet(1, 2, 3).Mode()", "1")]
    [TestCase("DataSet(1, 2, 3).Range()", "2")]
    [TestCase("Prefix(5, Si.Milli)", "0.005")]
    [TestCase("Prefix(5, Si.Giga)", "5000000000")]
    [TestCase("Factorial(5)", "120")]
    [TestCase("Factorial(7)", "5040")]
    public async Task EnsureThat_Evaluator_EvaluateAsync_ReturnsExpected(string input, string expected)
    {
        (string error, string result) = await _sut.EvaluateAsync(input);
        if (!string.IsNullOrEmpty(error)) 
        {
            Assert.Fail(error);
        }
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public async Task EnsureThat_Evaluator_EvaluateAsync_CanCreateVariables()
    {
        await _sut.EvaluateAsync("var foo = 42");
        (string error, string result) = await _sut.EvaluateAsync("foo");
        Assert.That(result, Is.EqualTo("42"));
    }

    [Test]
    public async Task EnsureThat_Evaluator_Reset_ClearsState()
    {
        int variableCount = 0;
        await _sut.EvaluateAsync("var foo = 42");
        variableCount += 1;
        
        _sut.Reset();
        variableCount = _sut.Variables.Count;

        Assert.That(variableCount, Is.EqualTo(0));
    }

}
