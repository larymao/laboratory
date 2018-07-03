using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Core.Helpers
{
    /// <summary>
    ///     Provides methods for facilitating the use of <see cref="Type"/>.
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        ///     Checks whether a type is a simple type.
        /// </summary>
        /// <param name="type">
        ///     The type to check.
        /// </param>
        /// <returns>
        ///     True if the type is a simple type.
        /// </returns>
        public static bool IsSimple(Type type)
        {
            return type.IsPrimitive
                || type.Equals(typeof(string));
        }
    }
}
