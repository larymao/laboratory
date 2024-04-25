using System.Collections.Generic;
using System.Reflection;

namespace Lary.Laboratory.Core.Utils;

/// <summary>
/// Used for data comparison.
/// </summary>
public static class ObjectComparer
{
    /// <summary>
    /// Determines whether the specified two objects are equal. Comparing property by property.
    /// </summary>
    /// <typeparam name="T">The type of objects to be compared.</typeparam>
    /// <param name="obj1">The base object.</param>
    /// <param name="obj2">The object to compare to the base one.</param>
    /// <param name="ignores">The name of properties to be ignored during the comparison.</param>
    /// <returns><see langword="true"/> if the two object are equal; otherwise, <see langword="false"/>.</returns>
    public static bool PropertiesEqual<T>(T obj1, T obj2, params string[] ignores)
        where T : class
    {
        if (obj1 == null || obj2 == null)
        {
            return obj1 == obj2;
        }

        var type = typeof(T);
        var ignoreSet = new HashSet<string>(ignores);

        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!ignoreSet.Contains(prop.Name))
            {
                var val1 = type.GetProperty(prop.Name).GetValue(obj1, null);
                var val2 = type.GetProperty(prop.Name).GetValue(obj2, null);

                if (val1 != val2 && (val1 == null || !val1.Equals(val2)))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
