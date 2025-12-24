using LevelUpDev.Domain.Common;

namespace LevelUpDev.Domain.Interfaces;

/// <summary>
/// Generic repository interface for Cosmos DB operations.
/// </summary>
/// <typeparam name="TEntity">Entity type that inherits from BaseEntity.</typeparam>
public interface ICosmosRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Gets an entity by its ID and partition key.
    /// </summary>
    Task<QueryResult<TEntity>> GetByIdAsync(
        string id,
        string partitionKey,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    Task<QueryResult<TEntity>> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    Task<QueryResult<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Upserts an entity (create or update).
    /// </summary>
    Task<QueryResult<TEntity>> UpsertAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity by its ID and partition key.
    /// </summary>
    Task<Result> DeleteAsync(
        string id,
        string partitionKey,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Queries entities with a SQL query.
    /// </summary>
    Task<QueryResult<IReadOnlyList<TEntity>>> QueryAsync(
        string query,
        string? partitionKey = null,
        Dictionary<string, object>? parameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of entities.
    /// </summary>
    Task<QueryResult<PagedList<TEntity>>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? partitionKey = null,
        string? continuationToken = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities in a partition.
    /// </summary>
    Task<QueryResult<IReadOnlyList<TEntity>>> GetAllByPartitionKeyAsync(
        string partitionKey,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if an entity exists.
    /// </summary>
    Task<Result<bool>> ExistsAsync(
        string id,
        string partitionKey,
        CancellationToken cancellationToken = default);
}
