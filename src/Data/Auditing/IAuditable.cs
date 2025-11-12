namespace Lary.Laboratory.Data.Auditing;

/// <summary>
/// An entity that supports full auditing (creation, modification, and deletion tracking).
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// Gets or sets the creation time (UTC timestamp in seconds).
    /// </summary>
    long CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created this entity.
    /// </summary>
    string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the last modification time (UTC timestamp in seconds).
    /// Initially set to the creation time.
    /// </summary>
    long LastModifiedAt { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last modified this entity.
    /// Initially set to the creator.
    /// </summary>
    string? LastModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this entity is deleted (soft delete).
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the deletion time (UTC timestamp in seconds).
    /// </summary>
    long? DeletionAt { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who deleted this entity.
    /// </summary>
    string? DeletedBy { get; set; }
}
