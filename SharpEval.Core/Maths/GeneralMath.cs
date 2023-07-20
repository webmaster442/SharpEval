namespace SharpEval.Core.Maths
{
    internal static class GeneralMath
    {
        public static double Map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        public static long Map(long value, long fromLow, long fromHigh, long toLow, long toHigh)
        {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        public static double Lerp(double x0, double x1, double alpha)
        {
            return x0 * (1 - alpha) + x1 * alpha;
        }

        public static long Lcm(long a, long b)
        {
            return (a * b) / Gcd(a, b);
        }

        public static long Gcd(long a, long b)
        {
            if (a == 0 || b == 0)
            {
                return Math.Abs(a) + Math.Abs(b);
            }
            a = Math.Abs(a);
            b = Math.Abs(b);
            int shift = 0;
            while (((a | b) & 1) == 0)
            {
                a >>= 1;
                b >>= 1;
                shift++;
            }
            while ((a & 1) == 0)
            {
                a >>= 1;
            }
            do
            {
                while ((b & 1) == 0)
                {
                    b >>= 1;
                }
                if (a > b)
                {
                    long temp = a;
                    a = b;
                    b = temp;
                }
                b -= a;
            } while (b != 0);
            return a << shift;
        }

        public static bool IsPrime(long n)
        {
            if (n <= 1)
            {
                return false;
            }
            else if (n <= 3)
            {
                return true;
            }
            else if (n % 2 == 0 || n % 3 == 0)
            {
                return false;
            }

            long r = 4;
            while (r * r <= n)
            {
                if (n % r == 0)
                {
                    return false;
                }
                r++;
            }
            return true;
        }

        public static double Prefix(double value, Si prefix)
        {
            double multiplier = Math.Pow(10, (int)prefix);
            return value * multiplier;
        }

        public static (double number, Si prefix) Prefix(double value)
        {
            var table = Enum.GetValues<Si>()
                .Select(x => new
                {
                    Value = (int)x,
                    Name = x.ToString()
                })
                .OrderByDescending(x => x.Value);

            foreach (var entry in table)
            {
                double multiplier = Math.Pow(10, entry.Value);
                if (value < multiplier) continue;

                double number = value / multiplier;
                Si prefix = Enum.Parse<Si>(entry.Name);

                return (number, prefix);
            }

            return (value, Si.None);
        }

        public static long Factorial(int n)
        {
            long value = 1;
            for (long i = n; i > 0; i--)
            {
                value *= i;
            }
            return value;
        }
    }
}
