using System.Globalization;
using System.Numerics;

using SharpEval.Core.Maths;
using SharpEval.Core.Maths.Sequences;

namespace SharpEval.Core.Internals;

/// <summary>
/// Global functions avaliable in the expressions
/// </summary>
public sealed class Globals
{
    private readonly ISettingsProvider _settingsProvider;
    private readonly ApiAdapter _apiAdapter;
    private readonly UnitConversion _unitConversion;
    private readonly Plotter _plotter;

    internal Random RandomGenerator { get; set; }

    internal Globals(ISettingsProvider settingsProvider, ApiAdapter apiAdapter)
    {
        _settingsProvider = settingsProvider;
        _apiAdapter = apiAdapter;
        _plotter = new Plotter();
        _unitConversion = new UnitConversion(CultureInfo.InvariantCulture);
        RandomGenerator = new Random();
    }

    /// <summary>
    /// Exit program
    /// </summary>
    public static void Exit()
    {
        Environment.Exit(0);
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
    /// Returns the natural (base e) logarithm of a complex number.
    /// </summary>
    /// <param name="number">The number whose logarithm is to be found.</param>
    /// <returns>the natural (base e) logarithm of the number</returns>
    public static Complex Ln(Complex number)
        => System.Numerics.Complex.Log(number);

    /// <summary>
    /// Returns the logarithm of a specified number in a specified base.
    /// </summary>
    /// <param name="number">The number whose logarithm is to be found</param>
    /// <param name="base">The base of the logarithm</param>
    /// <returns>the logarithm of the number in the specified base</returns>
    public static double Log(double number, double @base) => Math.Log(number, @base);

    /// <summary>
    /// Returns the logarithm of a complex number in a specified base.
    /// </summary>
    /// <param name="number">The number whose logarithm is to be found</param>
    /// <param name="base">The base of the logarithm</param>
    /// <returns>the logarithm of the number in the specified base</returns>
    public static Complex Log(Complex number, double @base)
        => System.Numerics.Complex.Log(number, @base);

    /// <summary>
    /// Returns the base 10 logarithm of a specified number.
    /// </summary>
    /// <param name="number">A number whose logarithm is to be found.</param>
    /// <returns>the base 10 logarithm of the number</returns>
    public static double Log10(double number) => Math.Log10(number);

    /// <summary>
    /// Returns the base 10 logarithm of a complex number.
    /// </summary>
    /// <param name="number">A number whose logarithm is to be found.</param>
    /// <returns>the base 10 logarithm of the number</returns>
    public static Complex Log10(Complex number)
        => System.Numerics.Complex.Log10(number);

    /// <summary>
    /// Returns a specified number raised to the specified power.
    /// </summary>
    /// <param name="number">A double-precision floating-point number to be raised to a power</param>
    /// <param name="power">A double-precision floating-point number that specifies a power</param>
    /// <returns>The number raised to the power</returns>
    public static double Pow(double number, double power) => Math.Pow(number, power);

    /// <summary>
    /// Returns a complex number raised to the specified power.
    /// </summary>
    /// <param name="number">A double-precision floating-point number to be raised to a power</param>
    /// <param name="power">A double-precision floating-point number that specifies a power</param>
    /// <returns>The number raised to the power</returns>
    public static Complex Pow(Complex number, double power)
        => System.Numerics.Complex.Pow(number, power);

    /// <summary>
    /// Returns an integer that indicates the sign of a double-precision floating-point number.
    /// </summary>
    /// <param name="number">A signed number.</param>
    /// <returns>
    /// A number that indicates the sign of value.
    /// -1 –value is less than zero.
    /// 0 –value is equal to zero.
    /// 1 –value is greater than zero.
    /// </returns>
    public static double Sign(double number) => Math.Sign(number);

    /// <summary>
    /// Returns the square root of a specified number
    /// </summary>
    /// <param name="number">The number whose square root is to be found</param>
    /// <returns>The square root of a specified number</returns>
    public static double Sqrt(double number) => Math.Sqrt(number);

    /// <summary>
    /// Returns the square root of a complex number
    /// </summary>
    /// <param name="number">The number whose square root is to be found</param>
    /// <returns>The square root of a specified number</returns>
    public static Complex Sqrt(Complex number)
        => System.Numerics.Complex.Sqrt(number);

    /// <summary>
    /// Returns the factorial of a number.
    /// </summary>
    /// <param name="number">The number whose factorial is to be found</param>
    /// <returns>The factorial of the number.</returns>
    public static long Factorial(byte number)
        => GeneralMath.Factorial(number);

    /// <summary>
    /// Apply an Si Prefix to a number
    /// </summary>
    /// <param name="number">A number</param>
    /// <param name="prefix">Prefix to apply</param>
    /// <returns>The Si pefixed number</returns>
    /// <seealso cref="Si"/>
    public static double Prefix(double number, Si prefix)
        => GeneralMath.Prefix(number, prefix);

    /// <summary>
    /// Get the Si prefix of a value
    /// </summary>
    /// <param name="number">A number</param>
    /// <returns>value expressed in an SI prefix</returns>
    public static (double number, Si prefix) Prefix(double number)
        => GeneralMath.Prefix(number);

    /// <summary>
    /// Returns the sine of the specified angle.
    /// </summary>
    /// <param name="number">An angle, measured in the current angle system</param>
    /// <returns>the sine of the specified angle.</returns>
    /// <seealso cref="Settings"/>
    /// <seealso cref="AngleSystem"/>
    public double Sin(double number)
        => Trigonometry.Sin(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    /// <summary>
    /// Returns the cosine of the specified angle.
    /// </summary>
    /// <param name="number">An angle, measured in the current angle system</param>
    /// <returns>the cosine of the specified angle.</returns>
    /// <seealso cref="Settings"/>
    /// <seealso cref="AngleSystem"/>
    public double Cos(double number)
        => Trigonometry.Cos(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    /// <summary>
    /// Returns the tangent of the specified angle.
    /// </summary>
    /// <param name="number">An angle, measured in the current angle system</param>
    /// <returns>the tangent of the specified angle.</returns>
    /// <seealso cref="Settings"/>
    /// <seealso cref="AngleSystem"/>
    public double Tan(double number)
        => Trigonometry.Tan(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    /// <summary>
    /// Returns the angle whose sine is the specified number.
    /// </summary>
    /// <param name="number">A number representing a sine</param>
    /// <returns>An angle, measured in the current angle system</returns>
    /// <seealso cref="Settings"/>
    /// <seealso cref="AngleSystem"/>
    public double ArcSin(double number)
    => Trigonometry.ArcSin(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    /// <summary>
    /// Returns the angle whose cosine is the specified number.
    /// </summary>
    /// <param name="number">A number representing a cosine</param>
    /// <returns>An angle, measured in the current angle system</returns>
    /// <seealso cref="Settings"/>
    /// <seealso cref="AngleSystem"/>
    public double ArcCos(double number)
        => Trigonometry.ArcCos(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    /// <summary>
    /// Returns the angle whose tangent is the specified number.
    /// </summary>
    /// <param name="number">A number representing a tangent</param>
    /// <returns>An angle, measured in the current angle system</returns>
    /// <seealso cref="Settings"/>
    /// <seealso cref="AngleSystem"/>
    public double ArcTan(double number)
        => Trigonometry.ArcTan(number, _settingsProvider.GetSettings().CurrentAngleSystem);

    /// <summary>
    /// Re-maps a number from one range to another. That is, a value of fromLow would get mapped to toLow, a value of fromHigh to toHigh, values in-between to values in-between, etc.
    /// </summary>
    /// <param name="value">the number to map.</param>
    /// <param name="fromLow">the lower bound of the value’s current range.</param>
    /// <param name="fromHigh">the upper bound of the value’s current range.</param>
    /// <param name="toLow">the lower bound of the value’s target range.</param>
    /// <param name="toHigh">the upper bound of the value’s target range.</param>
    /// <returns>The mapped double value.</returns>
    public static double Map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        => GeneralMath.Map(value, fromLow, fromHigh, toLow, toHigh);

    /// <summary>
    /// Re-maps a number from one range to another. That is, a value of fromLow would get mapped to toLow, a value of fromHigh to toHigh, values in-between to values in-between, etc.
    /// </summary>
    /// <param name="value">the number to map.</param>
    /// <param name="fromLow">the lower bound of the value’s current range.</param>
    /// <param name="fromHigh">the upper bound of the value’s current range.</param>
    /// <param name="toLow">the lower bound of the value’s target range.</param>
    /// <param name="toHigh">the upper bound of the value’s target range.</param>
    /// <returns>The mapped long value.</returns>
    public static long Map(long value, long fromLow, long fromHigh, long toLow, long toHigh)
        => GeneralMath.Map(value, fromLow, fromHigh, toLow, toHigh);

    /// <summary>
    /// Linear interpolate between two values
    /// </summary>
    /// <param name="x0">Starting value</param>
    /// <param name="x1">End value</param>
    /// <param name="alpha">interpolation amount</param>
    /// <returns>A value between x0 and x1</returns>
    public static double Lerp(double x0, double x1, double alpha)
        => GeneralMath.Lerp(x0, x1, alpha);

    /// <summary>
    /// Returns the greatest common divisor of two numbers
    /// </summary>
    /// <param name="a">a number</param>
    /// <param name="b">a number</param>
    /// <returns>The greatest common divisor of the two inputs</returns>
    public static long Gcd(long a, long b)
        => GeneralMath.Gcd(a, b);

    /// <summary>
    /// Returns the least common multiple of two numbers
    /// </summary>
    /// <param name="a">a number</param>
    /// <param name="b">a number</param>
    /// <returns>The least common multiple of the two inputs</returns>
    public static long Lcm(long a, long b)
        => GeneralMath.Lcm(a, b);

    /// <summary>
    /// Creates a new Fractional number
    /// </summary>
    /// <param name="numerator">the numerator</param>
    /// <param name="denominator">the denominator</param>
    /// <returns>A fractional number</returns>
    public static Fraction Fraction(long numerator, long denominator)
        => new(numerator, denominator);

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
    /// Creates a complex number
    /// </summary>
    /// <param name="real">real part</param>
    /// <param name="imaginary">imaginary part</param>
    /// <returns>A complex number</returns>
    public static Complex Complex(double real, double imaginary)
    {
        return new Complex(real, imaginary);
    }

    /// <summary>
    /// Creates a complex number from a 2d vector
    /// </summary>
    /// <param name="vector">vector to use</param>
    /// <returns>a complex number</returns>
    public static Complex Complex(Vector2 vector)
    {
        return new Complex(vector.X, vector.Y);
    }

    /// <summary>
    /// Computes the reciprocal of a value.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="number">The numer</param>
    /// <returns>the reciprocal of the number</returns>
    public static T Reciprocal<T>(T number) where T : IDivisionOperators<T, T, T>, IMultiplicativeIdentity<T, T>
    {
        return T.MultiplicativeIdentity / number;
    }

    /// <summary>
    /// Computes the sum of a sequence numbers
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="items">A sequence of numbers</param>
    /// <returns>The sum of the numbers</returns>
    public static T Sum<T>(params T[] items) where T : INumber<T>
        => Statistics.Sum(items);

    /// <summary>
    /// Gets the maximum number from a sequence numbers
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="items">A sequence of numbers</param>
    /// <returns>The maximum of the numbers</returns>
    public static T Max<T>(params T[] items) where T : INumber<T>
        => Statistics.Max(items);

    /// <summary>
    /// Gets the Minimum number from a sequence numbers
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="items">A sequence of numbers</param>
    /// <returns>The minimum of the numbers</returns>
    public static T Min<T>(params T[] items) where T : INumber<T>
        => Statistics.Min(items);

    /// <summary>
    /// Returns the number of elements in a sequence
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="items">A sequence of numbers</param>
    /// <returns>Number of items</returns>
    public static int Count<T>(params T[] items)
    {
        return items.Length;
    }

    /// <summary>
    /// Computes the average of a sequence numbers
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="items">A sequence of numbers</param>
    /// <returns>the average of a sequence numbers</returns>
    public static double Average<T>(params T[] items) where T : INumber<T>
        => Statistics.Average(items);

    /// <summary>
    /// Computes the Geometric mean average of a sequence numbers
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="items">A sequence of numbers</param>
    /// <returns>the average of a sequence numbers</returns>
    public static double GeometricMean<T>(params T[] items) where T : INumber<T>
        => Statistics.GeometricMean(items);

    /// <summary>
    /// Computes the range of a sequence numbers.
    /// The range is the spread of the elements from the lowest to the highest value 
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="items">A sequence of numbers</param>
    /// <returns>the range of a sequence numbers</returns>
    public static T Range<T>(params T[] items) where T : INumber<T>
        => Max(items) - Min(items);

    /// <summary>
    /// Performs unit conversion
    /// </summary>
    /// <param name="value">Value to convert</param>
    /// <param name="from">source unit</param>
    /// <param name="to">target unit</param>
    /// <returns>value converted to target unit</returns>
    public double UnitConvert(double value, string from, string to)
        => _unitConversion.Convert(value, from, to);

    /// <summary>
    /// Returns an array with distinct items
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="items">an array of items</param>
    /// <returns>an array with distinct items</returns>
    public static T[] Distinct<T>(params T[] items) where T : IComparable<T>
        => Arrays.Distinct(items);

    /// <summary>
    /// Randomizes an array of items
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="items">an array of items</param>
    /// <returns>Array of items in randomized order</returns>
    public T[] Randomize<T>(params T[] items)
        => Arrays.Randomize(RandomGenerator, items);

    /// <summary>
    /// Pick a random entry from a given array
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="items">an array of items</param>
    /// <returns>A random item from the array</returns>
    public T RandomPick<T>(params T[] items)
        => Arrays.RandomPick(RandomGenerator, items);

    /// <summary>
    /// Returns the current date and time
    /// </summary>
    /// <returns>the current date and time</returns>
    public static DateTime Date()
        => DateTime.Now;

    /// <summary>
    /// Returns the current UTC date and time
    /// </summary>
    /// <returns>the current UTC date and time</returns>
    public static DateTime UtcDate()
        => DateTime.UtcNow;

    /// <summary>
    /// Cereate a date and time
    /// </summary>
    /// <param name="year">year</param>
    /// <param name="month">month</param>
    /// <param name="day">day</param>
    /// <param name="hour">hour</param>
    /// <param name="minute">minute</param>
    /// <param name="second">second</param>
    /// <returns>A date representing given values</returns>
    public static DateTime Date(int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        => new DateTime(year, month, day, hour, minute, second);

    /// <summary>
    /// Reset plot to defaults
    /// </summary>
    public void PlotReset()
        => _plotter.Reset();

    /// <summary>
    /// Plot a function
    /// </summary>
    /// <param name="function">Function to plot</param>
    /// <param name="start">start point for the plot</param>
    /// <param name="end">end point for the plot</param>
    /// <param name="step">step size</param>
    /// <param name="title">Title to associate to function</param>
    public void PlotFunction(PlotDelegate function, double start, double end, double step, string title)
        => _plotter.Function(f => function(f), start, end, step, title);

    /// <summary>
    /// Set plot title
    /// </summary>
    /// <param name="title">plot title to use</param>
    public void PlotTitle(string title)
        => _plotter.Title(title);

    /// <summary>
    /// Set plot background from a hex code
    /// Default is #ffffff
    /// </summary>
    /// <param name="hexColor">color to use. E.g: #c0c0c0</param>
    public void PlotBackground(string hexColor)
        => _plotter.Background(hexColor);

    /// <summary>
    /// Set plot area width and height in pixels
    /// </summary>
    /// <param name="width">Plot width</param>
    /// <param name="height">Plot height</param>
    public void PlotSize(int width, int height)
        => _plotter.Size(width, height);

    /// <summary>
    /// Print the plot
    /// </summary>
    /// <returns>An SVG Image</returns>
    public ISvgImage PlotPrint()
        => _plotter.Plot();

    /// <summary>
    /// Creates a Vector
    /// </summary>
    /// <param name="x">x value</param>
    /// <param name="y">y value</param>
    /// <returns>a new 2 dimensional vector</returns>
    public Vector2 Vector(float x, float y)
        => new Vector2(x, y);

    /// <summary>
    /// Creates a Vector from a complex number
    /// </summary>
    /// <param name="complex">Complex number</param>
    /// <returns>a new 2 dimensional vector</returns>
    public Vector2 Vector(Complex complex)
        => new Vector2((float)complex.Real, (float)complex.Imaginary);

    /// <summary>
    /// Creates a Vector
    /// </summary>
    /// <param name="x">x value</param>
    /// <param name="y">y value</param>
    /// <param name="z">z value</param>
    /// <returns>a new 3 dimensional vector</returns>
    public Vector3 Vector(float x, float y, float z)
        => new Vector3(x, y, z);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ammount"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public double Exchange(double ammount, string from, string to)
        => _apiAdapter.GetExchange(ammount, from, to);

    /// <summary>
    /// Creates a prime number sequence containing prime numbers between the minimum and maximum value
    /// </summary>
    /// <param name="minimum">starting number</param>
    /// <param name="maximum">end number</param>
    /// <returns>A sequence containing prime numbers between the minimum and maximum value</returns>
    public static PrimeSequence Primes(long minimum, long maximum)
        => new() { Minimum = minimum, Maximum = maximum };

    /// <summary>
    /// Creates a fibonacci number sequence containing prime numbers between the minimum and maximum value
    /// </summary>
    /// <param name="maximum">end number</param>
    /// <returns>A sequence containing fibonacci numbers between the minimum and maximum value</returns>
    public static FibonacciSequence Fibonacci(long maximum)
        => new() { Maximum = maximum };
}
