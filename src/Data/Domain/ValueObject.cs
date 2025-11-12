namespace Lary.Laboratory.Data.Domain;

/// <summary>
/// Marker interface for value objects.
/// Value objects are immutable and identified by their property values rather than an identifier.
/// </summary>
public interface IValueObject
{
}

/// <summary>
/// Base class for value objects.
/// Value objects are immutable and compared by their property values.
/// </summary>
public abstract class ValueObject : IValueObject, IEquatable<ValueObject>
{
    /// <summary>
    /// Gets the atomic values that define this value object for equality comparison.
    /// </summary>
    /// <returns>An enumerable of atomic values.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <summary>
    /// Determines whether the specified value object is equal to the current value object.
    /// </summary>
    public virtual bool Equals(ValueObject? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current value object.
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is ValueObject valueObject && Equals(valueObject);
    }

    /// <summary>
    /// Returns the hash code for this value object.
    /// </summary>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// Determines whether two value objects are equal.
    /// </summary>
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    /// <summary>
    /// Determines whether two value objects are not equal.
    /// </summary>
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Creates a shallow copy of this value object.
    /// </summary>
    protected ValueObject ShallowCopy()
    {
        return (ValueObject)MemberwiseClone();
    }
}

