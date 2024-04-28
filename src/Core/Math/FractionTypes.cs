namespace Lary.Laboratory.Core.Math;

/// <summary>
/// Fraction types.
/// </summary>
[Flags]
public enum FractionTypes
{
    /// <summary>
    /// <see cref="decimal"/> fraction.
    /// </summary>
    Decimal = 1,

    /// <summary>
    /// <see cref="double"/> fraction.
    /// </summary>
    Double = 2,

    /// <summary>
    /// <see cref="float"/> fraction.
    /// </summary>
    Single = 4,

    /// <summary>
    /// All fraction types.
    /// </summary>
    All = ~0
}
