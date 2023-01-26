using System.Text;

namespace Lary.Laboratory.Core
{
    /// <summary>
    /// Provides methods for <see cref="StringBuilder"/>
    /// </summary>
    public static class StringBuilderHelper
    {
        /// <summary>
        /// Appends a copy of the specified string to the given <see cref="StringBuilder"/> object based on a condition.
        /// </summary>
        /// <param name="builder">A <see cref="StringBuilder"/> object.</param>
        /// <param name="predicate">The condition to be tested.</param>
        /// <param name="value">The string to append.</param>
        /// <returns>
        /// A reference to the given <see cref="StringBuilder"/> object after the append operation has completed.
        /// </returns>
        public static StringBuilder AppendIf(this StringBuilder builder, bool predicate, string? value)
        {
            return predicate ? builder.Append(value) : builder;
        }

        /// <summary>
        /// Appends a copy of the specified string followed by the default line terminator to
        /// the given <see cref="StringBuilder"/> object based on a condition.
        /// </summary>
        /// <param name="builder">A <see cref="StringBuilder"/> object.</param>
        /// <param name="predicate">The condition to be tested.</param>
        /// <param name="value">The string to append.</param>
        /// <returns>
        /// A reference to the given <see cref="StringBuilder"/> object after the append operation has completed.
        /// </returns>
        public static StringBuilder AppendLineIf(this StringBuilder builder, bool predicate, string? value)
        {
            return predicate ? builder.AppendLine(value) : builder;
        }
    }
}
