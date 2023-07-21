using System.Globalization;
using System.Numerics;

namespace SharpEval.Core.Internals.ResultFormatters
{
    internal sealed class Vector2SingleLineFormatter : SingleLineResultFormatter
    {
        public override string GetString(object? o, AngleSystem angleSystem)
        {
            if (o is Vector2 vector2)
                return $"x: {vector2.X.ToString(CultureInfo.InvariantCulture)} y: {vector2.Y.ToString(CultureInfo.InvariantCulture)}";

            return string.Empty;
        }

        public override bool IsTypeMatch(object? o)
        {
            return o is Vector2;
        }
    }
}
