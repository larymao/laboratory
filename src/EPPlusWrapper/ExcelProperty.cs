namespace Lary.Laboratory.EPPlusWrapper;

internal class ExcelProperty
{
    private PropertyInfo? _propertyInfo;
    private string? _cellKey;

    public object? ValueProvider { get; set; }

    public PropertyInfo? PropertyInfo
    {
        get
        {
            return _propertyInfo;
        }
        set
        {
            _propertyInfo = value;

            _cellKey = GetCellKey();
        }
    }

    public string? CellKey => _cellKey;

    public int CellIndex { get; set; }

    public int CellLength { get; set; }

    public object? GetValue()
        => ValueProvider == null || PropertyInfo == null
            ? null
            : PropertyInfo.GetValue(ValueProvider);

    private string? GetCellKey()
    {
        if (_propertyInfo == null)
            return null;

        var excelPropertyAttrs = _propertyInfo.GetCustomAttributes<ExcelPropertyAttribute>();

        return excelPropertyAttrs.Any()
            ? excelPropertyAttrs.Single().PropertyName
            : _propertyInfo.Name;
    }
}
