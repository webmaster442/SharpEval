using System.Numerics;

using SharpEval.Core.Maths;

namespace SharpEval.Core.Internals;

/// <summary>
/// Global functions avaliable in the expressions
/// </summary>
public sealed class Globals
{
    private readonly ISettingsProvider _settingsProvider;

    internal Globals(ISettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
    }

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
    /// </summary>
    public static double Pi => Math.PI;

    /// <summary>
    /// Represents the natural logarithmic base, specified by the constant, e.
    /// </summary>
    public static double E => Math.E;

    /// <summary>
    /// Returns the absolute value of a double-precision floating-point number.
    /// </summary>
    /// <param name="number">A number that is greater than or equal to Double.MinValue, but less than or equal to Double.MaxValue.</param>
    /// <returns> A double-precision floating-point number, x, such that 0 ≤ x ≤ Double.MaxValue.</returns>
    public static double Abs(double number) => Math.Abs(number);

    public static T Reciproc<T>(T number) where T : IDivisionOperators<T, T, T>, IMultiplicativeIdentity<T, T>
    {
        return T.MultiplicativeIdentity / number;
    }

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified double-precision floating-point number.
    /// </summary>
    /// <param name="number">A double-precision floating-point number.</param>
    /// <returns>The smallest integral value that is greater than or equal to the number</returns>
    public static double Ceiling(double number) => Math.Ceiling(number);

    /// <summary>
    /// Returns the largest integral value less than or equal to the specified double-precision floating-point number.
    /// </summary>
    /// <param name="number">A double-precision floating-point number</param>
    /// <returns>The largest integral value less than or equal to the number</returns>
    public static double Floor(double number) => Math.Floor(number);

    /// <summary>
    /// Returns the natural (base e) logarithm of a specified number.
    /// </summary>
    /// <param name="number">The number whose logarithm is to be found.</param>
    /// <returns>the natural (base e) logarithm of the number</returns>
    public static double Ln(double number) => Math.Log(number);

    /// <summary>
    /// Returns the logarithm of a specified number in a specified base.
    /// </summary>
    /// <param name="number">The number whose logarithm is to be found</param>
    /// <param name="base">The base of the logarithm</param>
    /// <returns>the logarithm of the number in the specified base</returns>
    public static double Log(double number, double @base) => Math.Log(number, @base);

    /// <summary>
    /// Returns the base 10 logarithm of a specified number.
    /// </summary>
    /// <param name="number">A number whose logarithm is to be found.</param>
    /// <returns>the base 10 logarithm of the number</returns>
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

    /// <summary>
    /// Converts a long value to its binary representation
    /// </summary>
    /// <param name="number">the numnber to convert</param>
    /// <returns>the value as a string represented in binary</returns>
    public static string ToBin(long number)
        => Convert.ToString(number, 2);

    /// <summary>
    /// Converts a long value to its hexadecimal representation
    /// </summary>
    /// <param name="number">the numnber to convert</param>
    /// <returns>the value as a string represented in hexadecimal</returns>
    public static string ToHex(long number)
        => Convert.ToString(number, 16);

    /// <summary>
    /// Converts a long value to its octal representation
    /// </summary>
    /// <param name="number">the numnber to convert</param>
    /// <returns>the value as a string represented in octal</returns>
    public static string ToOct(long number)
        => Convert.ToString(number, 8);

    /// <summary>
    /// Convert a number from its binary representation to a long value
    /// </summary>
    /// <param name="number">The number represented in binary as a string</param>
    /// <returns>The number converted to long</returns>
    public static long FromBin(string number)
        => Convert.ToInt64(number, 2);

    /// <summary>
    /// Convert a number from its octal representation to a long value
    /// </summary>
    /// <param name="number">The number represented in octal as a string</param>
    /// <returns>The number converted to long</returns>
    public static long FromOct(string number)
        => Convert.ToInt64(number, 8);

    /// <summary>
    /// Convert a number from its hexadecimal representation to a long value
    /// </summary>
    /// <param name="number">The number represented in hexadecimal as a string</param>
    /// <returns>The number converted to long</returns>
    public static long FromHex(string number)
        => Convert.ToInt64(number, 16);

    /// <summary>
    /// Create a DataSet from numbers
    /// </summary>
    /// <typeparam name="T">number type</typeparam>
    /// <param name="numbers">Numbers</param>
    /// <returns>A DataSet</returns>
    /// <seealso cref="DataSet{T}(T[])"/>
    public static DataSet<T> DataSet<T>(params T[] numbers) where T : INumber<T> 
        => new DataSet<T>(numbers);
}
