using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;
using LevelUpDev.Domain.Interfaces;

namespace LevelUpDev.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for Activity entity.
/// </summary>
public class ActivityRepository : CosmosRepositoryBase<Activity>, IActivityRepository
{
    public ActivityRepository(Container container, ILogger<ActivityRepository> logger)
        : base(container, logger)
    {
    }

    public async Task<QueryResult<IReadOnlyList<Activity>>> GetByUserIdAsync(
        string userId,
        int limit = 50,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<IReadOnlyList<Activity>>.Failure(
                Error.Validation("UserId", "User ID cannot be empty"));
        }

        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c WHERE c.userId = @userId ORDER BY c.createdAt DESC",
            parameters: new Dictionary<string, object> { { "userId", userId } },
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<Activity>>> GetByDateAsync(
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        var dateString = date.ToString("yyyy-MM-dd");

        return await QueryAsync(
            "SELECT * FROM c WHERE c.date = @date ORDER BY c.createdAt DESC",
            partitionKey: dateString,
            parameters: new Dictionary<string, object> { { "date", dateString } },
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<Activity>>> GetByDateRangeAsync(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            "SELECT * FROM c WHERE c.date >= @startDate AND c.date <= @endDate ORDER BY c.createdAt DESC",
            parameters: new Dictionary<string, object>
            {
                { "startDate", startDate.ToString("yyyy-MM-dd") },
                { "endDate", endDate.ToString("yyyy-MM-dd") }
            },
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<Activity>>> GetCommunityFeedAsync(
        int limit = 50,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c ORDER BY c.createdAt DESC",
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<Activity>>> GetByTypeAsync(
        ActivityType activityType,
        int limit = 50,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c WHERE c.activityType = @type ORDER BY c.createdAt DESC",
            parameters: new Dictionary<string, object> { { "type", (int)activityType } },
            cancellationToken: cancellationToken);
    }
}
