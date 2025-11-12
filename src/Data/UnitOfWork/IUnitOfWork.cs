using Lary.Laboratory.Data.Domain;
using Lary.Laboratory.Data.Repositories;

namespace Lary.Laboratory.Data.UnitOfWork;

/// <summary>
/// Represents a unit of work pattern for managing transactions and coordinating repository operations.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets a repository for the specified aggregate root type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the aggregate root.</typeparam>
    /// <typeparam name="TKey">The type of the entity identifier.</typeparam>
    /// <returns>A repository instance.</returns>
    IRepository<TEntity, TKey> Repository<TEntity, TKey>()
        where TEntity : class, IAggregateRoot
        where TKey : notnull;

    /// <summary>
    /// Gets a repository for the specified aggregate root type with integer identifier.
    /// </summary>
    /// <typeparam name="TEntity">The type of the aggregate root.</typeparam>
    /// <returns>A repository instance.</returns>
    IRepository<TEntity> Repository<TEntity>()
        where TEntity : class, IAggregateRoot;

    /// <summary>
    /// Saves all changes made in this unit of work to the database.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current transaction.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the current transaction.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified action within a transaction.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified function within a transaction and returns the result.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="func">The function to execute.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The result of the function.</returns>
    Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken = default);
}

