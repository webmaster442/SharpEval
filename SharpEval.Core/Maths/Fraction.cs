using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace SharpEval.Core.Maths;

/// <summary>
/// Represents a fractional number
/// </summary>
public struct Fraction :
    IEquatable<Fraction>,
    IComparable<Fraction>,
    IParsable<Fraction>,
    IAdditionOperators<Fraction, Fraction, Fraction>,
    ISubtractionOperators<Fraction, Fraction, Fraction>,
    IDivisionOperators<Fraction, Fraction, Fraction>,
    IMultiplyOperators<Fraction, Fraction, Fraction>,
    IModulusOperators<Fraction, Fraction, Fraction>,
    IComparisonOperators<Fraction, Fraction, bool>,
    IEqualityOperators<Fraction, Fraction, bool>,
    IAdditiveIdentity<Fraction, Fraction>,
    IMultiplicativeIdentity<Fraction, Fraction>
{

    /// <summary>
    /// The number above the line in a vulgar fraction showing how many of the parts indicated by the denominator are taken
    /// </summary>
    public long Numerator { get; private set; }

    /// <summary>
    /// The bottom number in a fraction that shows the number of equal parts an item is divided into.
    /// </summary>
    public long Denominator { get; private set; }

    /// <inheritdoc/>
    public static Fraction AdditiveIdentity => new(0, 1);

    /// <inheritdoc/>
    public static Fraction MultiplicativeIdentity => new(1, 1);

    /// <summary>
    /// Creates a new instance a fractional number
    /// </summary>
    public Fraction()
    {
        Numerator = 0;
        Denominator = 1;
    }
    /// <summary>
    /// Creates a new instance a fractional number
    /// </summary>
    /// <param name="numerator">the numerator</param>
    /// <param name="denominator">the denominator</param>
    /// <exception cref="DivideByZeroException">when the denominator is 0</exception>
    public Fraction(long numerator, long denominator)
    {
        if (denominator == 0)
            throw new DivideByZeroException($"0 can't be denominator");

        Numerator = numerator;
        Denominator = denominator;
        Simplify();
    }

    private void Simplify()
    {
        if (Denominator < 0)
        {
            Numerator = -Numerator;
            Denominator = -Denominator;
        }
        long gcd = GeneralMath.Gcd(Numerator, Denominator);
        Numerator /= gcd;
        Denominator /= gcd;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        if (Denominator == 1)
            return Numerator.ToString();

        return $"{Numerator}/{Denominator}";
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Fraction fraction && Equals(fraction);
    }

    /// <inheritdoc/>
    public bool Equals(Fraction other)
    {
        return Numerator == other.Numerator &&
               Denominator == other.Denominator;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Numerator, Denominator);
    }

    /// <inheritdoc/>
    public int CompareTo(Fraction other)
    {
        long n1 = Numerator * other.Denominator;
        long n2 = other.Numerator * Denominator;
        return n1.CompareTo(n2);
    }

    /// <inheritdoc/>
    public static Fraction Parse(string s, IFormatProvider? provider)
    {
        try
        {
            long numerator;
            if (s.Contains('/'))
            {
                string[] parts = s.Split('/');
                numerator = long.Parse(parts[0], provider);
                long denominator = long.Parse(parts[1], provider);
                return new Fraction(numerator, denominator);
            }
            numerator = long.Parse(s, provider);
            return new Fraction(numerator, 1L);
        }
        catch (Exception ex)
        {
            throw new FormatException("Invalid format", ex);
        }
    }

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
    {
        try
        {
            if (string.IsNullOrEmpty(s))
            {
                result = default;
                return false;
            }

            result = Parse(s, provider);
            return true;
        }
        catch (FormatException)
        {
            result = default;
            return false;
        }

    }

    /// <inheritdoc/>
    public static bool operator ==(Fraction left, Fraction right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc/>
    public static bool operator !=(Fraction left, Fraction right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public static Fraction operator +(Fraction left, Fraction right)
    {
        long lcm = GeneralMath.Lcm(left.Denominator, right.Denominator);
        long factorLeft = lcm / left.Denominator;
        long factorRigt = lcm / right.Denominator;
        long numerator = left.Numerator * factorLeft + right.Numerator * factorRigt;
        return new Fraction(numerator, lcm);
    }

    /// <inheritdoc/>
    public static Fraction operator -(Fraction left, Fraction right)
    {
        long lcm = GeneralMath.Lcm(left.Denominator, right.Denominator);
        long factorLeft = lcm / left.Denominator;
        long factorRigt = lcm / right.Denominator;
        long numerator = left.Numerator * factorLeft - right.Numerator * factorRigt;
        return new Fraction(numerator, lcm);
    }

    /// <inheritdoc/>
    public static Fraction operator /(Fraction left, Fraction right)
    {
        long numerator = left.Numerator * right.Denominator;
        long denominator = left.Denominator * right.Numerator;
        return new Fraction(numerator, denominator);
    }

    /// <inheritdoc/>
    public static Fraction operator *(Fraction left, Fraction right)
    {
        long numerator = left.Numerator * right.Numerator;
        long denominator = left.Denominator * right.Denominator;
        return new Fraction(numerator, denominator);
    }

    /// <inheritdoc/>
    public static Fraction operator %(Fraction left, Fraction right)
    {
        long quotient = left.Numerator * right.Denominator / left.Denominator;
        Fraction integerPart = new Fraction(quotient, 1);
        return left - integerPart * right;
    }

    /// <inheritdoc/>
    public static bool operator <(Fraction left, Fraction right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <inheritdoc/>
    public static bool operator <=(Fraction left, Fraction right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <inheritdoc/>
    public static bool operator >(Fraction left, Fraction right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <inheritdoc/>
    public static bool operator >=(Fraction left, Fraction right)
    {
        return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Create a fraction from a long value
    /// </summary>
    /// <param name="number">number</param>
    public static implicit operator Fraction(long number)
    {
        return new Fraction(number, 1);
    }
}
