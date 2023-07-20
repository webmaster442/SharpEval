using System.Diagnostics;

namespace SharpEval.Core.Maths
{
    internal static class Trigonometry
    {
        private static double GetRadians(double angle, AngleSystem angleSystem)
        {
            switch (angleSystem)
            {
                case AngleSystem.Rad:
                    return angle;
                case AngleSystem.Deg:
                    angle %= 360;
                    angle = angle > 180 ? angle -= 360 : angle;
                    return (angle * Math.PI) / 180.0d;
                case AngleSystem.Grad:
                    angle %= 400;
                    angle = angle > 200 ? angle -= 400 : angle;
                    return (angle * Math.PI) / 200.0d;
                default:
                    throw new UnreachableException("Unknown enum value");
            }
        }

        private static double GetDegrees(double radians, AngleSystem angleSystem)
        {
            switch (angleSystem)
            {
                case AngleSystem.Rad:
                    return radians;
                case AngleSystem.Deg:
                    return radians * (180 / Math.PI); ;
                case AngleSystem.Grad:
                    return radians * (200 / Math.PI);
                default:
                    throw new UnreachableException("Unknown enum value");
            }
        }

        private static double Correct(double result)
        {
            if (Math.Abs(result) <= 1.3E-16d)
                return 0;

            if (Math.Abs(1 - result) <= 1E-12)
                return result > 0 ? 1 : -1;

            if (Math.Abs(result) >= 16331239353195370d)
            {
                return result > 0 ? double.PositiveInfinity : double.NegativeInfinity;
            }

            return result;
        }

        public static double Sin(double angle, AngleSystem angleSystem)
        {
            double radians = GetRadians(angle, angleSystem);
            double result = Math.Sin(radians);
            return Correct(result);
        }

        public static double ArcSin(double value, AngleSystem angleSystem)
        {
            double radians = Math.Asin(value);
            return GetDegrees(radians, angleSystem);
        }

        public static double Cos(double angle, AngleSystem angleSystem)
        {
            double radians = GetRadians(angle, angleSystem);
            double result = Math.Cos(radians);
            return Correct(result);
        }

        public static double ArcCos(double value, AngleSystem angleSystem)
        {
            double radians = Math.Acos(value);
            return GetDegrees(radians, angleSystem);
        }

        public static double Tan(double angle, AngleSystem angleSystem)
        {
            double radians = GetRadians(angle, angleSystem);
            double result = Math.Tan(radians);
            return Correct(result);
        }

        public static double ArcTan(double value, AngleSystem angleSystem)
        {
            double radians = Math.Atan(value);
            return GetDegrees(radians, angleSystem);
        }
    }
}
