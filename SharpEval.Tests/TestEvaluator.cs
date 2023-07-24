namespace SharpEval.Tests;

[TestFixture]
public class TestEvaluator
{
    private Evaluator _sut;
    private Mock<ISettingsProvider> _settingProviderMock;
    private Mock<IApiClient> _apiClientMock;
    private Settings _settings;

    [SetUp]
    public void Setup()
    {
        _settings = new Settings
        {
            CurrentAngleSystem = AngleSystem.Deg,
        };
        _settingProviderMock = new Mock<ISettingsProvider>(MockBehavior.Strict);
        _apiClientMock = new Mock<IApiClient>(MockBehavior.Strict);
        _settingProviderMock.Setup(x => x.GetSettings()).Returns(_settings);
        _sut = new Evaluator(_settingProviderMock.Object, _apiClientMock.Object);
        _sut.SetRandomSeed(4);
    }

    private static string? TestFormat(object? result)
    {
        if (result is IEnumerable<int> enumerable)
        {
            return string.Join(", ", enumerable);
        }
        else if (result is IFormattable formattable)
        {
            return formattable.ToString("", CultureInfo.InvariantCulture);
        }
        return result?.ToString();
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
    [TestCase("Reciprocal(2.0)", "0.5")]
    [TestCase("Reciprocal(0.5)", "2")]
    [TestCase("Reciprocal(Fraction(3,1))", "1/3")]
    [TestCase("Reciprocal(Fraction(1,3))", "3")]
    [TestCase("FromHex(\"ff\")", "255")]
    [TestCase("FromHex(\"FF\")", "255")]
    [TestCase("ToHex(255)", "ff")]
    [TestCase("ToBin(15)", "1111")]
    [TestCase("FromBin(\"111\")", "7")]
    [TestCase("FromOct(\"17\")", "15")]
    [TestCase("ToOct(15)", "17")]
    [TestCase("Count(1, 2, 4)", "3")]
    [TestCase("Sum(1, 2, 3)", "6")]
    [TestCase("Min(1, 2, 4)", "1")]
    [TestCase("Max(1, 2, 4)", "4")]
    [TestCase("Average(1, 2, 3)", "2")]
    [TestCase("Range(1, 2, 3)", "2")]
    [TestCase("Prefix(5, Si.Milli)", "0.005")]
    [TestCase("Prefix(1000)", "(1, Kilo)")]
    [TestCase("Prefix(1)", "(1, None)")]
    [TestCase("Prefix(5, Si.Giga)", "5000000000")]
    [TestCase("Factorial(5)", "120")]
    [TestCase("Factorial(7)", "5040")]
    [TestCase("Fraction(1, 5)*4", "4/5")]
    [TestCase("UnitConvert(1, \"meter\", \"foot\")", "3.280839895013123")]
    [TestCase("UnitConvert(1, \"m\", \"ft\")", "3.280839895013123")]
    [TestCase("Distinct(1, 1, 2, 3, 3)", "1, 2, 3")]
    [TestCase("Randomize(1, 2, 3)", "3, 1, 2")]
    [TestCase("RandomPick(1, 2, 3)", "3")]
    [TestCase("Date(1914, 1, 1)", "01/01/1914 00:00:00")]
    [TestCase("Date(1914, 1, 1, 11, 52)", "01/01/1914 11:52:00")]
    [TestCase("Date(1914, 1, 1, 11, 33, 22)", "01/01/1914 11:33:22")]
    [TestCase("Lcm(5, 2)", "10")]
    public async Task EnsureThat_Evaluator_EvaluateAsync_ReturnsExpected(string input, string expected)
    {
        var result = await _sut.EvaluateAsync(input);
        if (!string.IsNullOrEmpty(result.Error))
        {
            Assert.Fail(result.Error);
        }
        Assert.That(TestFormat(result.ResultData), Is.EqualTo(expected));
    }

    [TestCase("UnitConvert(1, \"meter\", \"feet\")", "Unknown unit: feet")]
    [TestCase("UnitConvert(1, \"feet\", \"meter\")", "Unknown unit: feet")]
    [TestCase("UnitConvert(1, \"meter\", \"liter\")", "Can't convert from meter to liter")]
    public async Task EnsureThat_Evaluator_EvaluateAsync_ReturnsExpectedError(string input, string expected)
    {
        var result = await _sut.EvaluateAsync(input);
        if (string.IsNullOrEmpty(result.Error))
        {
            Assert.Fail("There was no issue");
        }
        Assert.That(result.Error, Is.EqualTo(expected));
    }

    [Test]
    public async Task EnsureThat_Evaluator_ResetWorks()
    {
        await _sut.EvaluateAsync("var foo = 42");
        _sut.Reset();
        Assert.That(_sut.Variables.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task EnsureThat_Evaluator_EvaluateAsync_CanCreateVariables()
    {
        await _sut.EvaluateAsync("var foo = 42");
        var result = await _sut.EvaluateAsync("foo");
        Assert.Multiple(() =>
        {
            Assert.That(result.ResultData, Is.EqualTo(42));
            Assert.That(_sut.Variables.Count, Is.EqualTo(1));
        });
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
