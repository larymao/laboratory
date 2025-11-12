namespace Lary.Laboratory.Data.Domain;

/// <summary>
/// Base class for aggregate roots with typed identifier.
/// </summary>
/// <typeparam name="TKey">The type of the aggregate root identifier.</typeparam>
public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
    where TKey : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Gets the collection of domain events raised by this aggregate.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to be dispatched.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    protected virtual void AddDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent == null)
            throw new ArgumentNullException(nameof(domainEvent));
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Removes a domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to remove.</param>
    protected virtual void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    /// <summary>
    /// Clears all domain events.
    /// </summary>
    public virtual void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

/// <summary>
/// Base class for aggregate roots with string identifier.
/// This is the recommended base class for most aggregate roots.
/// </summary>
public abstract class AggregateRoot : AggregateRoot<string>
{
}

