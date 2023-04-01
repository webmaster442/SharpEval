using System.Numerics;

using SharpEval.Core.Maths;

namespace SharpEval.Core.Internals;

public sealed class Globals
{
    private readonly ISettingsProvider _settingsProvider;

    internal Globals(ISettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
    }

    public static double Pi => Math.PI;
    public static double E => Math.E;

    public static double Abs(double number) => Math.Abs(number);

    public static T Reciproc<T>(T number) where T : IDivisionOperators<T, T, T>, IMultiplicativeIdentity<T, T>
    {
        return T.MultiplicativeIdentity / number;
    }

    public static double Ceiling(double number) => Math.Ceiling(number);
    public static double Floor(double number) => Math.Floor(number);
    public static double Ln(double number) => Math.Log(number);
    public static double Log(double number, double @base) => Math.Log(number, @base);
    public static double Log10(double number) => Math.Log10(number);
    public static double Pow(double number, double power) => Math.Pow(number, power);
    public static double Sign(double number) => Math.Sign(number);
    public static double Sqrt(double number) => Math.Sqrt(number);

    public long Factorial(byte number) 
        => GeneralMath.Factorial(number);

    public double Prefix(double number, Si prefix)
        => GeneralMath.Prefix(number, prefix);

    public double Sin(double number)
        => Trigonometry.Sin(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    public double Cos(double number)
        => Trigonometry.Cos(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    public double Tan(double number)
        => Trigonometry.Tan(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    public double ArcSin(double number)
    => Trigonometry.ArcSin(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    public double ArcCos(double number)
        => Trigonometry.ArcCos(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    public double ArcTan(double number)
        => Trigonometry.ArcTan(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    public static double Map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        => GeneralMath.Map(value, fromLow, fromHigh, toLow, toHigh);

    public static long Map(long value, long fromLow, long fromHigh, long toLow, long toHigh)
        => GeneralMath.Map(value, fromLow, fromHigh, toLow, toHigh);

    public static double Lerp(double x0, double x1, double alpha)
        => GeneralMath.Lerp(x0, x1, alpha);

    public static long Gcd(long a, long b)
        => GeneralMath.Gcd(a, b);

    public static long Lcm(long a, long b)
        => GeneralMath.Lcm(a, b);

    public static Fraction Fraction(long a, long b)
        => new(a, b);

    public static string ToBin(long number)
        => Convert.ToString(number, 2);

    public static string ToHex(long number)
        => Convert.ToString(number, 16);

    public static string ToOct(long number)
        => Convert.ToString(number, 8);

    public static long FromBin(string number)
        => Convert.ToInt64(number, 2);

    public static long FromOct(string number)
        => Convert.ToInt64(number, 8);

    public static long FromHex(string number)
        => Convert.ToInt64(number, 16);

    public static DataSet<T> DataSet<T>(params T[] numbers) where T : INumber<T> 
        => new DataSet<T>(numbers);
}
