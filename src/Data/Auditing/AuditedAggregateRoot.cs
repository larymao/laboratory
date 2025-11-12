namespace Lary.Laboratory.Data.Auditing;

using Lary.Laboratory.Data.Domain;

/// <summary>
/// Base class for aggregate roots with full auditing support.
/// Includes creation, modification, and soft delete tracking.
/// </summary>
/// <typeparam name="TKey">The type of the aggregate root identifier.</typeparam>
public abstract class AuditedAggregateRoot<TKey> : AggregateRoot<TKey>, IAuditable
    where TKey : notnull
{
    /// <inheritdoc />
    public long CreatedAt { get; set; }

    /// <inheritdoc />
    public string? CreatedBy { get; set; }

    /// <inheritdoc />
    public long LastModifiedAt { get; set; }

    /// <inheritdoc />
    public string? LastModifiedBy { get; set; }

    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public long? DeletionAt { get; set; }

    /// <inheritdoc />
    public string? DeletedBy { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditedAggregateRoot{TKey}"/> class.
    /// </summary>
    protected AuditedAggregateRoot()
    {
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        CreatedAt = now;
        LastModifiedAt = now;  // 初始化为创建时间
    }
}

/// <summary>
/// Base class for aggregate roots with full auditing support and string identifier.
/// This is the recommended base class for most aggregate roots.
/// </summary>
public abstract class AuditedAggregateRoot : AuditedAggregateRoot<string>
{
}


