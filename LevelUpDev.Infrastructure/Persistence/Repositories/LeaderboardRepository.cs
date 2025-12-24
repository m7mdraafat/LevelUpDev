using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;
using LevelUpDev.Domain.Enums;
using LevelUpDev.Domain.Interfaces;

namespace LevelUpDev.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for LeaderboardEntry entity.
/// </summary>
public class LeaderboardRepository : CosmosRepositoryBase<LeaderboardEntry>, ILeaderboardRepository
{
    public LeaderboardRepository(Container container, ILogger<LeaderboardRepository> logger)
        : base(container, logger)
    {
    }

    public async Task<QueryResult<IReadOnlyList<LeaderboardEntry>>> GetByTypeAsync(
        LeaderboardType type,
        int limit = 100,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c WHERE c.type = @type ORDER BY c.rank ASC",
            partitionKey: type.ToString(),
            parameters: new Dictionary<string, object> { { "type", (int)type } },
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<LeaderboardEntry>> GetUserEntryAsync(
        LeaderboardType type,
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<LeaderboardEntry>.Failure(
                Error.Validation("UserId", "User ID cannot be empty"));
        }

        var result = await QueryAsync(
            "SELECT * FROM c WHERE c.type = @type AND c.userId = @userId",
            partitionKey: type.ToString(),
            parameters: new Dictionary<string, object>
            {
                { "type", (int)type },
                { "userId", userId }
            },
            cancellationToken: cancellationToken);

        if (result.IsFailure)
        {
            return QueryResult<LeaderboardEntry>.Failure(result.Error);
        }

        var entry = result.Value.FirstOrDefault();
        if (entry is null)
        {
            return QueryResult<LeaderboardEntry>.Failure(
                Error.NotFound("LeaderboardEntry", $"{type}:{userId}"));
        }

        return QueryResult<LeaderboardEntry>.Success(entry, result.RequestCharge);
    }

    public async Task<QueryResult<IReadOnlyList<LeaderboardEntry>>> GetUserEntriesAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<IReadOnlyList<LeaderboardEntry>>.Failure(
                Error.Validation("UserId", "User ID cannot be empty"));
        }

        return await QueryAsync(
            "SELECT * FROM c WHERE c.userId = @userId",
            parameters: new Dictionary<string, object> { { "userId", userId } },
            cancellationToken: cancellationToken);
    }

    public async Task<Result> BatchUpsertAsync(
        IEnumerable<LeaderboardEntry> entries,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Group by partition key for efficient batch operations
            var groupedEntries = entries.GroupBy(e => e.PartitionKeyValue);

            foreach (var group in groupedEntries)
            {
                foreach (var entry in group)
                {
                    var result = await UpsertAsync(entry, cancellationToken);
                    if (result.IsFailure)
                    {
                        _logger.LogWarning(
                            "Failed to upsert leaderboard entry for user {UserId}: {Error}",
                            entry.UserId, result.Error.Description);
                    }
                }
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Batch upsert failed for leaderboard entries");
            return Result.Failure(Error.Database("Batch upsert failed"));
        }
    }

    public async Task<QueryResult<IReadOnlyList<LeaderboardEntry>>> GetEntriesAroundRankAsync(
        LeaderboardType type,
        int rank,
        int range = 5,
        CancellationToken cancellationToken = default)
    {
        var minRank = Math.Max(1, rank - range);
        var maxRank = rank + range;

        return await QueryAsync(
            "SELECT * FROM c WHERE c.type = @type AND c.rank >= @minRank AND c.rank <= @maxRank ORDER BY c.rank ASC",
            partitionKey: type.ToString(),
            parameters: new Dictionary<string, object>
            {
                { "type", (int)type },
                { "minRank", minRank },
                { "maxRank", maxRank }
            },
            cancellationToken: cancellationToken);
    }
}
