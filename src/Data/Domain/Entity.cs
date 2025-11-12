namespace Lary.Laboratory.Data.Domain;

/// <summary>
/// Base class for entities with typed identifier.
/// </summary>
/// <typeparam name="TKey">The type of the entity identifier.</typeparam>
public abstract class Entity<TKey> : IEquatable<Entity<TKey>>
    where TKey : notnull
{
    /// <summary>
    /// Gets or sets the entity identifier.
    /// </summary>
    public virtual TKey Id { get; protected set; } = default!;

    /// <summary>
    /// Checks if the entity is transient (not persisted yet).
    /// </summary>
    public virtual bool IsTransient()
    {
        return EqualityComparer<TKey>.Default.Equals(Id, default!);
    }

    /// <summary>
    /// Determines whether the specified entity is equal to the current entity.
    /// </summary>
    public virtual bool Equals(Entity<TKey>? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        if (IsTransient() || other.IsTransient())
            return false;

        return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current entity.
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is Entity<TKey> entity && Equals(entity);
    }

    /// <summary>
    /// Returns the hash code for this entity.
    /// </summary>
    public override int GetHashCode()
    {
        if (IsTransient())
            return base.GetHashCode();

        return Id.GetHashCode();
    }

    /// <summary>
    /// Determines whether two entities are equal.
    /// </summary>
    public static bool operator ==(Entity<TKey>? left, Entity<TKey>? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    /// <summary>
    /// Determines whether two entities are not equal.
    /// </summary>
    public static bool operator !=(Entity<TKey>? left, Entity<TKey>? right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Base class for entities with string identifier.
/// This is the recommended base class for most entities.
/// </summary>
public abstract class Entity : Entity<string>
{
}

