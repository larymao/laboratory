using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Core
{
    /// <summary>
    /// Provides methods for dictionary.
    /// </summary>
    public static class DictionaryHelper
    {
        /// <summary>
        /// Converts the given dictionary to an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the target object.</typeparam>
        /// <param name="source">The dictionary to be converted.</param>
        /// <returns>An object of the given type.</returns>
        public static T ToObject<T>(this IDictionary<string, object?> source)
            where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType.GetProperty(item.Key).SetValue(someObject, item.Value, null);
            }

            return someObject;
        }
    }
}
