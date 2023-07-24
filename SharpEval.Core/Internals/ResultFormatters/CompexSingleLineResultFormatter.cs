using System.Diagnostics;
using System.Globalization;
using System.Numerics;

using SharpEval.Core.Maths;

namespace SharpEval.Core.Internals.ResultFormatters;

internal sealed class CompexSingleLineResultFormatter : SingleLineResultFormatter
{
    public override string GetString(object? o, AngleSystem angleSystem)
    {
        if (o is not Complex complex)
            throw new UnreachableException("type should be Complex");

        return $"{complex.Real.ToString(CultureInfo.InvariantCulture)} + {complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i\r\nr = {complex.Magnitude.ToString(CultureInfo.InvariantCulture)} φ = {GetPhase(complex.Phase, angleSystem).ToString(CultureInfo.InvariantCulture)}";
    }

    private static double GetPhase(double phase, AngleSystem angleSystem)
        => Trigonometry.GetDegrees(phase, angleSystem);

    public override bool IsTypeMatch(object? o)
    {
        return o is Complex;
    }
}
