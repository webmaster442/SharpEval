using System.Text;

namespace SharpEval.Core.Maths;

internal static class NumberInfoProvider
{
    private static int[] GetBinarySpacing(object obj)
    {
        if (obj.GetType() == typeof(byte))
            return new[] { 8 };
        else if (obj.GetType() == typeof(short))
            return new[] { 1, 7, 8 };
        else if (obj.GetType() == typeof(ushort))
            return new[] { 8, 8 };
        else if (obj.GetType() == typeof(int))
            return new[] { 1, 7, 8, 8, 8 };
        else if (obj.GetType() == typeof(uint))
            return new[] { 8, 8, 8, 8 };
        else if (obj.GetType() == typeof(float))
            return new[] { 1, 8, 23 };
        else if (obj.GetType() == typeof(long))
            return new[] { 1, 7, 8, 8, 8, 8, 8, 8, 8 };
        else if (obj.GetType() == typeof(ulong))
            return new int[] { 8, 8, 8, 8, 8, 8, 8, 8 };
        else if (obj.GetType() == typeof(double))
            return new int[] { 1, 11, 52 };
        else
            return Array.Empty<int>();
    }

    private static byte[] GetBytes(object obj)
    {
        if (obj is byte)
            return new byte[] { (byte)obj };
        else if (obj is short)
            return BitConverter.GetBytes((short)obj);
        else if (obj is ushort)
            return BitConverter.GetBytes((ushort)obj);
        else if (obj is int)
            return BitConverter.GetBytes((int)obj);
        else if (obj is uint)
            return BitConverter.GetBytes((uint)obj);
        else if (obj is long)
            return BitConverter.GetBytes((long)obj);
        else if (obj is ulong)
            return BitConverter.GetBytes((ulong)obj);
        else if (obj is double)
            return BitConverter.GetBytes((double)obj);
        else if (obj is float)
            return BitConverter.GetBytes((float)obj);
        else
            return Array.Empty<byte>();
    }

    public static NumberInfoResult GetInfo(object number)
    {
        var data = GetBytes(number);
        Array.Reverse(data);
        var spacing = GetBinarySpacing(number);
        return new NumberInfoResult
        {
            Type = number.GetType().Name,
            Decimal = number.ToString() ?? throw new InvalidOperationException("ToString() failed"),
            Binary = FormatBinary(data, spacing),
            Hexadecimal = FormatHex(data),
            Bits = GetBits(data),
        };
    }

    private static int GetBits(byte[] data)
    {
        return data.Length * 8;
    }

    private static string FormatHex(byte[] data)
    {
        var chunks = data.Select(x => Convert.ToString(x, 16).PadLeft(2, '0'));
        return string.Join(' ', chunks);
    }

    private static string FormatBinary(byte[] data, int[] spacing)
    {
        StringBuilder sb = new();
        for (int i = 0; i < data.Length; i++)
            sb.Append(Convert.ToString(data[i], 2).PadLeft(8, '0'));

        StringBuilder sliced = new StringBuilder();
        string unsliced = sb.ToString();

        int start = 0;
        int end = 0;
        foreach (var space in spacing)
        {
            end = space;
            sliced.Append(unsliced.Substring(start, end));
            sliced.Append(' ');
            start += space;
        }
        return sliced.ToString();

    }
}
