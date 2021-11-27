using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Core.Utils
{
    /// <summary>
    ///     Provides methods for performing a deep copy of an object.
    ///     Serialization is used to perform the copy.
    /// </summary>
    public static class CloneUtil
    {
        /// <summary>
        ///     Perform a deep copy of the object.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of object being copied.
        /// </typeparam>
        /// <param name="source">
        ///     The object instance to copy.
        /// </param>
        /// <returns>
        ///     The copied object.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the input source is null.
        /// </exception>
        public static T DeepClone<T>(T source)
            where T : class
        {
            var clone = default(T);

            // Don't serialize a null object, simply return the default for that object.
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else
            {
                var json = JsonConvert.SerializeObject(source);

                clone = JsonConvert.DeserializeObject<T>(json);
            }

            return clone;
        }
    }
}
