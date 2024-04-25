using System;

namespace Lary.Laboratory.Core.Math;

/// <summary>
/// Specifies the digits of a decimal data.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FractionAttribute"/> class.
/// </remarks>
/// <param name="digits"></param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class FractionAttribute(int digits = 2) : Attribute
{

    /// <summary>
    /// The digits of current decimal data.
    /// </summary>
    public int Digits { get; set; } = digits;
}
