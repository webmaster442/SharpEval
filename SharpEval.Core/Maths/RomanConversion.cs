using System.Text;

namespace SharpEval.Core.Maths;
internal static class RomanConversion
{
    private readonly static Dictionary<int, string> RomanLiterals = new()
    {
        { 1000, "M" },
        { 900, "CM" },
        { 500, "D" },
        { 400, "CD" },
        { 100, "C" },
        { 90, "XC" },
        { 50, "L" },
        { 40, "XL" },
        { 10, "X" },
        { 9, "IX" },
        { 5, "V" },
        { 4, "IV" },
        { 1, "I" },
    };

    public static string ConvertToRoman(long num)
    {
        if (num > 4999 || num < -4999)
            throw new InvalidOperationException($"value must be between -4999 and 4999");

        StringBuilder result = new();
        if (num < 0)
        {
            result.Append('-');
            num = Math.Abs(num);
        }
        foreach (var literal in RomanLiterals) 
        {
            if (num <= 0) break;
            while (num >= literal.Key)
            {
                result.Append(literal.Value);
                num -= literal.Key;
            }
        }
        return result.ToString();
    }
}
