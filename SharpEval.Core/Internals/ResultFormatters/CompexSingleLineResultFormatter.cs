using System.Numerics;

using SharpEval.Core.Maths;

namespace SharpEval.Core.Internals.ResultFormatters
{
    internal sealed class CompexSingleLineResultFormatter : SingleLineResultFormatter
    {
        public override string GetString(object? o, AngleSystem angleSystem)
        {
            if (o is not Complex complex)
                return string.Empty;

            return $"{complex.Real} + {complex.Imaginary}i\r\nr = {complex.Magnitude} φ = {GetPhase(complex.Phase, angleSystem)}";
        }

        private static double GetPhase(double phase, AngleSystem angleSystem)
            => Trigonometry.GetDegrees(phase, angleSystem);

        public override bool IsTypeMatch(object? o)
        {
            return o is Complex;
        }
    }
}
