﻿using System.Numerics;

namespace SharpEval.Core.Maths;

internal static class Statistics
{
    public static T Sum<T>(params T[] items) where T : INumber<T>
    {
        T result = T.Zero;
        foreach (var item in items)
        {
            result += item;
        }
        return result;
    }

    public static T Min<T>(params T[] items) where T : INumber<T>
    {
        T result = items[0];
        foreach (var item in items)
        {
            if (item < result)
                result = item;
        }
        return result;
    }

    public static T Max<T>(params T[] items) where T : INumber<T>
    {
        T result = items[0];
        foreach (var item in items)
        {
            if (item > result)
                result = item;
        }
        return result;
    }

    public static double Average<T>(params T[] items) where T : INumber<T>
    {
        return Convert.ToDouble(Sum(items)) / items.Length;
    }

    public static double GeometricMean<T>(params T[] items) where T: INumber<T>
    {
        T product = T.One;
        for (int i=0; i<items.Length; i++)
        {
            product *= items[i];
        }
        return Math.Pow(Convert.ToDouble(product), 1.0 / items.Length);
    }
}
