﻿using System.Collections;
using System.Numerics;

namespace SharpEval.Core.Maths.Sequences;

/// <summary>
/// Represents a base class for number series
/// </summary>
public abstract class NumberSequenceBase : IEnumerable<long>,
    IAdditionOperators<NumberSequenceBase, NumberSequenceBase, IEnumerable<long>>,
    ISubtractionOperators<NumberSequenceBase, NumberSequenceBase, IEnumerable<long>>,
    IMultiplyOperators<NumberSequenceBase, NumberSequenceBase, IEnumerable<long>>,
    IDivisionOperators<NumberSequenceBase, NumberSequenceBase, IEnumerable<double>>
{

    /// <summary>
    /// Series minimimum value. Series items are generated between minimum  and maximum.
    /// </summary>
    public virtual long Minimum
    {
        get;
        set;
    }

    /// <summary>
    /// Series maximum value. Series items are generated between minimum  and maximum.
    /// </summary>
    public long Maximum { get; set; }

    /// <summary>
    /// Check if Range is valid
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws invalidoperation, when minimum is bigger, than maximum</exception>
    protected void CheckRange()
    {
        if (Minimum > Maximum)
            throw new InvalidOperationException("Series minimum is bigger than maximum value");
    }

    /// <inheritdoc/>
    public abstract IEnumerator<long> GetEnumerator();

    /// <inheritdoc/>
    public static IEnumerable<long> operator +(NumberSequenceBase left, NumberSequenceBase right)
    {
        foreach ((long First, long Second) in left.Zip(right))
        {
            yield return First + Second;
        }
    }

    /// <inheritdoc/>
    public static IEnumerable<long> operator -(NumberSequenceBase left, NumberSequenceBase right)
    {
        foreach ((long First, long Second) in left.Zip(right))
        {
            yield return First - Second;
        }
    }

    /// <inheritdoc/>
    public static IEnumerable<long> operator *(NumberSequenceBase left, NumberSequenceBase right)
    {
        foreach ((long First, long Second) in left.Zip(right))
        {
            yield return First * Second;
        }
    }

    /// <inheritdoc/>
    public static IEnumerable<double> operator /(NumberSequenceBase left, NumberSequenceBase right)
    {
        foreach ((long First, long Second) in left.Zip(right))
        {
            yield return First / Second;
        }
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
