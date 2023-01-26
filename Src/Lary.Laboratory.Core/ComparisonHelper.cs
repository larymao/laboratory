using System;

namespace Lary.Laboratory.Core
{
    /// <summary>
    /// Provides methods for data comparison.
    /// </summary>
    public static class ComparisonHelper
    {
        /// <summary>
        /// Converts <see cref="StringComparison"/> object to corresponding <see cref="StringComparer"/>.
        /// </summary>
        /// <param name="comparison"><see cref="StringComparison"/> object</param>
        /// <returns>Converted <see cref="StringComparer"/> object</returns>
        /// <exception cref="NotImplementedException">
        /// Thrown if the given <see cref="StringComparison"/> object is invalid.
        /// </exception>
        public static StringComparer ToComparer(this StringComparison comparison)
        {
            return comparison switch
            {
                StringComparison.CurrentCulture => StringComparer.CurrentCulture,
                StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
                StringComparison.InvariantCulture => StringComparer.InvariantCulture,
                StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
                StringComparison.Ordinal => StringComparer.Ordinal,
                StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
                _ => throw new NotImplementedException("Unknown StringComparison"),
            };
        }
    }
}
