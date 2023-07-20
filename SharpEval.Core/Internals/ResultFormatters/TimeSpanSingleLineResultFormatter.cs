using System.Globalization;

namespace SharpEval.Core.Internals.ResultFormatters
{
    internal class TimeSpanSingleLineResultFormatter : SingleLineResultFormatter
    {
        private const double AvgDaysPerMonth = 30.436875;
        private const double AvgDaysPerYear = 365.2425;

        public override string GetString(object? o, CultureInfo culture)
        {
            if (o is TimeSpan timeSpan)
            {
                var years = (int)(timeSpan.Days / AvgDaysPerYear);
                var months = (int)(timeSpan.Days / AvgDaysPerMonth);
                return $"{timeSpan.Days} days {timeSpan.Hours} hours {timeSpan.Minutes} minutes {timeSpan.Seconds} seconds\r\n"
                    + $"Years (Aproximated): {years}\r\n"
                    + $"Months (Aproximated): {months}";
            }
            return string.Empty;
        }

        public override bool IsTypeMatch(object? o)
        {
            return o is TimeSpan;
        }
    }
}
