namespace Lary.Laboratory.EPPlusWrapper;

/// <summary>
/// Instructs the <see cref="ExcelHelper"/> to always parse the member with the specified name.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ExcelPropertyAttribute"/>.
/// </remarks>
/// <param name="propertyName">Name of the property.</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ExcelPropertyAttribute(string propertyName) : Attribute
{

    /// <summary>
    /// Gets or sets the name of the property.
    /// </summary>
    public string PropertyName { get; set; } = propertyName;
}
