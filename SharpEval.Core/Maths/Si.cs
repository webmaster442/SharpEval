using SharpEval.Core.Internals;

namespace SharpEval.Core.Maths;

/// <summary>
/// Represents Metric prefixes that can be used with the Prefix() method
/// </summary>
/// <seealso cref="Globals.Prefix(double, Si)"/>
public enum Si
{
    /// <summary>
    /// 10 to the power of -24
    /// </summary>
    Yocto = -24,
    /// <summary>
    /// 10 to the power of -21
    /// </summary>
    Zepto = -21,
    /// <summary>
    /// 10 to the power of -18
    /// </summary>
    Atto = -18,
    /// <summary>
    /// 10 to the power of -15
    /// </summary>
    Femto = -15,
    /// <summary>
    /// 10 to the power of -12
    /// </summary>
    Pico = -12,
    /// <summary>
    /// 10 to the power of -9
    /// </summary>
    Nano = -9,
    /// <summary>
    /// 10 to the power of -6
    /// </summary>
    Micro = -6,
    /// <summary>
    /// 10 to the power of -3
    /// </summary>
    Milli = -3,
    /// <summary>
    /// 10 to the power of -2
    /// </summary>
    Centi = -2,
    /// <summary>
    /// 10 to the power of -1
    /// </summary>
    Deci = -1,
    /// <summary>
    /// 10 to the power of 1
    /// </summary>
    Deca = 1,
    /// <summary>
    /// No power
    /// </summary>
    None = 0,
    /// <summary>
    /// 10 to the power of 2
    /// </summary>
    Hecto = 2,
    /// <summary>
    /// 10 to the power of 3
    /// </summary>
    Kilo = 3,
    /// <summary>
    /// 10 to the power of 6
    /// </summary>
    Mega = 6,
    /// <summary>
    /// 10 to the power of 9
    /// </summary>
    Giga = 9,
    /// <summary>
    /// 10 to the power of 12
    /// </summary>
    Terra = 12,
    /// <summary>
    /// 10 to the power of 15
    /// </summary>
    Peta = 15,
    /// <summary>
    /// 10 to the power of 18
    /// </summary>
    Exa = 18,
    /// <summary>
    /// 10 to the power of 21
    /// </summary>
    Zetta = 21,
    /// <summary>
    /// 10 to the power of 24
    /// </summary>
    Yotta = 24,
}
