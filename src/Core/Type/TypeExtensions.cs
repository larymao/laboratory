using System;
using System.Collections.Generic;

namespace Lary.Laboratory.Core;

/// <summary>
/// Provides extension methods for <see cref="Type"/>.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Gets the inheritance levels of the given type.
    /// </summary>
    /// <param name="type">The type to query.</param>
    /// <returns>The inheritance levels of the given type.</returns>
    public static IEnumerable<Type> InheritanceLevels(this Type type)
    {
        while (type != null)
        {
            yield return type;
            type = type.BaseType;
        }
    }
}
