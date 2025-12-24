using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;
using LevelUpDev.Domain.Interfaces;

namespace LevelUpDev.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for UserStats entity.
/// </summary>
public class UserStatsRepository : CosmosRepositoryBase<UserStats>, IUserStatsRepository
{
    public UserStatsRepository(Container container, ILogger<UserStatsRepository> logger)
        : base(container, logger)
    {
    }

    public async Task<QueryResult<UserStats>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<UserStats>.Failure(Error.Validation("UserId", "User ID cannot be empty"));
        }

        // Since partition key is userId, we can query efficiently within that partition
        var result = await QueryAsync(
            "SELECT * FROM c WHERE c.userId = @userId",
            partitionKey: userId,
            parameters: new Dictionary<string, object> { { "userId", userId } },
            cancellationToken: cancellationToken);

        if (result.IsFailure)
        {
            return QueryResult<UserStats>.Failure(result.Error);
        }

        var stats = result.Value.FirstOrDefault();
        if (stats is null)
        {
            return QueryResult<UserStats>.Failure(Error.NotFound("UserStats", userId));
        }

        return QueryResult<UserStats>.Success(stats, result.RequestCharge);
    }

    public async Task<QueryResult<IReadOnlyList<UserStats>>> GetTopByTotalSolvedAsync(
        int limit = 100,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c ORDER BY c.totalSolved DESC",
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<UserStats>>> GetTopByStreakAsync(
        int limit = 100,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c ORDER BY c.currentStreak DESC",
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<UserStats>>> GetTopByWeeklySolvedAsync(
        int limit = 100,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c ORDER BY c.problemsSolvedThisWeek DESC",
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<UserStats>>> GetTopByHardSolvedAsync(
        int limit = 100,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c ORDER BY c.hardSolved DESC",
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<UserStats>>> GetStaleStatsAsync(
        TimeSpan threshold,
        CancellationToken cancellationToken = default)
    {
        var cutoffTime = DateTime.UtcNow.Subtract(threshold);

        return await QueryAsync(
            "SELECT * FROM c WHERE c.lastSyncedAt < @cutoff",
            parameters: new Dictionary<string, object> { { "cutoff", cutoffTime } },
            cancellationToken: cancellationToken);
    }
}
