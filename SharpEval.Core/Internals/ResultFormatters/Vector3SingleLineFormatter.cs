using System.Diagnostics;
using System.Globalization;
using System.Numerics;

namespace SharpEval.Core.Internals.ResultFormatters
{
    internal sealed class Vector3SingleLineFormatter : SingleLineResultFormatter
    {
        public override string GetString(object? o, AngleSystem angleSystem)
        {
            if (o is Vector3 vector2)
                return $"x: {vector2.X.ToString(CultureInfo.InvariantCulture)} y: {vector2.Y.ToString(CultureInfo.InvariantCulture)} z: {vector2.Z.ToString(CultureInfo.InvariantCulture)}";

            throw new UnreachableException("type should be Vector3");
        }

        public override bool IsTypeMatch(object? o)
        {
            return o is Vector3;
        }
    }
}
