using System;

namespace Lary.Laboratory.Office
{
    /// <summary>
    /// Instructs the <see cref="ExcelHelper"/> to always parse the member with the specified name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ExcelPropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelPropertyAttribute"/>.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public ExcelPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string PropertyName { get; set; } = default!;
    }
}
