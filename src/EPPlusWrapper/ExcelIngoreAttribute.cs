namespace Lary.Laboratory.EPPlusWrapper;

/// <summary>
/// Instructs the <see cref="ExcelHelper"/>  not to parse public read/write property value.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ExcelIngoreAttribute : Attribute
{
}
