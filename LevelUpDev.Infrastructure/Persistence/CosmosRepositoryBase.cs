using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using LevelUpDev.Domain.Common;

namespace LevelUpDev.Infrastructure.Persistence;

/// <summary>
/// Base repository implementation for Cosmos DB operations.
/// Implements retry logic, proper error handling, and RU tracking.
/// </summary>
/// <typeparam name="TEntity">Entity type that inherits from BaseEntity.</typeparam>
public abstract class CosmosRepositoryBase<TEntity> where TEntity : BaseEntity
{
    protected readonly Container _container;
    protected readonly ILogger _logger;

    protected CosmosRepositoryBase(Container container, ILogger logger)
    {
        _container = container;
        _logger = logger;
    }

    /// <summary>
    /// Gets an entity by ID and partition key.
    /// </summary>
    public async Task<QueryResult<TEntity>> GetByIdAsync(
        string id,
        string partitionKey,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return QueryResult<TEntity>.Failure(Error.Validation("Id", "ID cannot be empty"));
        }

        try
        {
            var response = await _container.ReadItemAsync<TEntity>(
                id,
                new PartitionKey(partitionKey),
                cancellationToken: cancellationToken);

            _logger.LogDebug(
                "Read item {Id} from {Container}. RU: {RequestCharge}",
                id, _container.Id, response.RequestCharge);

            return QueryResult<TEntity>.Success(response.Resource, response.RequestCharge);
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return QueryResult<TEntity>.Failure(
                Error.NotFound(typeof(TEntity).Name, id));
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex,
                "Cosmos DB error reading {EntityType} with ID {Id}. Status: {StatusCode}",
                typeof(TEntity).Name, id, ex.StatusCode);
            return QueryResult<TEntity>.Failure(Error.Database(ex.Message));
        }
    }

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    public async Task<QueryResult<TEntity>> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            return QueryResult<TEntity>.Failure(Error.NullValue);
        }

        try
        {
            entity.CreatedAt = DateTime.UtcNow;

            var response = await _container.CreateItemAsync(
                entity,
                new PartitionKey(entity.PartitionKeyValue),
                cancellationToken: cancellationToken);

            _logger.LogInformation(
                "Created {EntityType} with ID {Id}. RU: {RequestCharge}",
                typeof(TEntity).Name, entity.Id, response.RequestCharge);

            return QueryResult<TEntity>.Success(response.Resource, response.RequestCharge);
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
        {
            return QueryResult<TEntity>.Failure(
                Error.Conflict(typeof(TEntity).Name, $"Entity with ID '{entity.Id}' already exists"));
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex,
                "Cosmos DB error creating {EntityType}. Status: {StatusCode}",
                typeof(TEntity).Name, ex.StatusCode);
            return QueryResult<TEntity>.Failure(Error.Database(ex.Message));
        }
    }

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    public async Task<QueryResult<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            return QueryResult<TEntity>.Failure(Error.NullValue);
        }

        try
        {
            entity.UpdatedAt = DateTime.UtcNow;

            var options = new ItemRequestOptions();
            
            // Use optimistic concurrency with ETag if available
            if (!string.IsNullOrEmpty(entity.ETag))
            {
                options.IfMatchEtag = entity.ETag;
            }

            var response = await _container.ReplaceItemAsync(
                entity,
                entity.Id,
                new PartitionKey(entity.PartitionKeyValue),
                options,
                cancellationToken);

            _logger.LogInformation(
                "Updated {EntityType} with ID {Id}. RU: {RequestCharge}",
                typeof(TEntity).Name, entity.Id, response.RequestCharge);

            return QueryResult<TEntity>.Success(response.Resource, response.RequestCharge);
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return QueryResult<TEntity>.Failure(
                Error.NotFound(typeof(TEntity).Name, entity.Id));
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.PreconditionFailed)
        {
            return QueryResult<TEntity>.Failure(
                Error.Conflict(typeof(TEntity).Name, "Entity was modified by another process"));
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex,
                "Cosmos DB error updating {EntityType} with ID {Id}. Status: {StatusCode}",
                typeof(TEntity).Name, entity.Id, ex.StatusCode);
            return QueryResult<TEntity>.Failure(Error.Database(ex.Message));
        }
    }

    /// <summary>
    /// Upserts an entity (create or update).
    /// </summary>
    public async Task<QueryResult<TEntity>> UpsertAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            return QueryResult<TEntity>.Failure(Error.NullValue);
        }

        try
        {
            entity.UpdatedAt = DateTime.UtcNow;

            var response = await _container.UpsertItemAsync(
                entity,
                new PartitionKey(entity.PartitionKeyValue),
                cancellationToken: cancellationToken);

            _logger.LogInformation(
                "Upserted {EntityType} with ID {Id}. RU: {RequestCharge}",
                typeof(TEntity).Name, entity.Id, response.RequestCharge);

            return QueryResult<TEntity>.Success(response.Resource, response.RequestCharge);
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex,
                "Cosmos DB error upserting {EntityType}. Status: {StatusCode}",
                typeof(TEntity).Name, ex.StatusCode);
            return QueryResult<TEntity>.Failure(Error.Database(ex.Message));
        }
    }

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    public async Task<Result> DeleteAsync(
        string id,
        string partitionKey,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return Result.Failure(Error.Validation("Id", "ID cannot be empty"));
        }

        try
        {
            var response = await _container.DeleteItemAsync<TEntity>(
                id,
                new PartitionKey(partitionKey),
                cancellationToken: cancellationToken);

            _logger.LogInformation(
                "Deleted {EntityType} with ID {Id}. RU: {RequestCharge}",
                typeof(TEntity).Name, id, response.RequestCharge);

            return Result.Success();
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return Result.Failure(Error.NotFound(typeof(TEntity).Name, id));
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex,
                "Cosmos DB error deleting {EntityType} with ID {Id}. Status: {StatusCode}",
                typeof(TEntity).Name, id, ex.StatusCode);
            return Result.Failure(Error.Database(ex.Message));
        }
    }

    /// <summary>
    /// Queries entities with a SQL query.
    /// </summary>
    public async Task<QueryResult<IReadOnlyList<TEntity>>> QueryAsync(
        string query,
        string? partitionKey = null,
        Dictionary<string, object>? parameters = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryDefinition = new QueryDefinition(query);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    queryDefinition = queryDefinition.WithParameter($"@{param.Key}", param.Value);
                }
            }

            var queryOptions = new QueryRequestOptions();
            if (!string.IsNullOrEmpty(partitionKey))
            {
                queryOptions.PartitionKey = new PartitionKey(partitionKey);
            }

            var results = new List<TEntity>();
            double totalRequestCharge = 0;

            using var iterator = _container.GetItemQueryIterator<TEntity>(queryDefinition, requestOptions: queryOptions);

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync(cancellationToken);
                results.AddRange(response);
                totalRequestCharge += response.RequestCharge;
            }

            _logger.LogDebug(
                "Query returned {Count} items from {Container}. Total RU: {RequestCharge}",
                results.Count, _container.Id, totalRequestCharge);

            return QueryResult<IReadOnlyList<TEntity>>.Success(results, totalRequestCharge);
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex,
                "Cosmos DB query error in {Container}. Status: {StatusCode}",
                _container.Id, ex.StatusCode);
            return QueryResult<IReadOnlyList<TEntity>>.Failure(Error.Database(ex.Message));
        }
    }

    /// <summary>
    /// Gets a paginated list of entities.
    /// </summary>
    public async Task<QueryResult<PagedList<TEntity>>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? partitionKey = null,
        string? continuationToken = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryOptions = new QueryRequestOptions
            {
                MaxItemCount = pageSize
            };

            if (!string.IsNullOrEmpty(partitionKey))
            {
                queryOptions.PartitionKey = new PartitionKey(partitionKey);
            }

            var query = new QueryDefinition("SELECT * FROM c ORDER BY c.createdAt DESC");

            var results = new List<TEntity>();
            double totalRequestCharge = 0;
            string? newContinuationToken = null;

            using var iterator = _container.GetItemQueryIterator<TEntity>(
                query,
                continuationToken,
                queryOptions);

            if (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync(cancellationToken);
                results.AddRange(response);
                totalRequestCharge = response.RequestCharge;
                newContinuationToken = response.ContinuationToken;
            }

            var pagedList = new PagedList<TEntity>(
                results,
                pageNumber,
                pageSize,
                results.Count, // Note: Cosmos DB doesn't support count efficiently
                newContinuationToken);

            return QueryResult<PagedList<TEntity>>.Success(pagedList, totalRequestCharge, newContinuationToken);
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex,
                "Cosmos DB paged query error in {Container}. Status: {StatusCode}",
                _container.Id, ex.StatusCode);
            return QueryResult<PagedList<TEntity>>.Failure(Error.Database(ex.Message));
        }
    }

    /// <summary>
    /// Gets all entities in a partition.
    /// </summary>
    public async Task<QueryResult<IReadOnlyList<TEntity>>> GetAllByPartitionKeyAsync(
        string partitionKey,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            "SELECT * FROM c ORDER BY c.createdAt DESC",
            partitionKey,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Checks if an entity exists.
    /// </summary>
    public async Task<Result<bool>> ExistsAsync(
        string id,
        string partitionKey,
        CancellationToken cancellationToken = default)
    {
        var result = await GetByIdAsync(id, partitionKey, cancellationToken);
        return Result.Success(result.IsSuccess);
    }
}
