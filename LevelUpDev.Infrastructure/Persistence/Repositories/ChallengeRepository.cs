using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;
using LevelUpDev.Domain.Interfaces;

namespace LevelUpDev.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for DailyChallenge entity.
/// </summary>
public class ChallengeRepository : CosmosRepositoryBase<DailyChallenge>, IChallengeRepository
{
    public ChallengeRepository(Container container, ILogger<ChallengeRepository> logger)
        : base(container, logger)
    {
    }

    public async Task<QueryResult<DailyChallenge>> GetByDateAsync(
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        var dateString = date.ToString("yyyy-MM-dd");

        var result = await QueryAsync(
            "SELECT * FROM c WHERE c.date = @date",
            partitionKey: dateString,
            parameters: new Dictionary<string, object> { { "date", dateString } },
            cancellationToken: cancellationToken);

        if (result.IsFailure)
        {
            return QueryResult<DailyChallenge>.Failure(result.Error);
        }

        var challenge = result.Value.FirstOrDefault();
        if (challenge is null)
        {
            return QueryResult<DailyChallenge>.Failure(
                Error.NotFound("DailyChallenge", dateString));
        }

        return QueryResult<DailyChallenge>.Success(challenge, result.RequestCharge);
    }

    public async Task<QueryResult<DailyChallenge>> GetTodaysChallengeAsync(
        CancellationToken cancellationToken = default)
    {
        return await GetByDateAsync(DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<DailyChallenge>>> GetByDateRangeAsync(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            "SELECT * FROM c WHERE c.date >= @startDate AND c.date <= @endDate ORDER BY c.date DESC",
            parameters: new Dictionary<string, object>
            {
                { "startDate", startDate.ToString("yyyy-MM-dd") },
                { "endDate", endDate.ToString("yyyy-MM-dd") }
            },
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<DailyChallenge>>> GetActiveAsync(
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            "SELECT * FROM c WHERE c.isActive = true ORDER BY c.date DESC",
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<DailyChallenge>>> GetCompletedByUserAsync(
        string userId,
        int limit = 30,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<IReadOnlyList<DailyChallenge>>.Failure(
                Error.Validation("UserId", "User ID cannot be empty"));
        }

        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c WHERE ARRAY_CONTAINS(c.completedByIds, @userId) ORDER BY c.date DESC",
            parameters: new Dictionary<string, object> { { "userId", userId } },
            cancellationToken: cancellationToken);
    }
}

/// <summary>
/// Repository implementation for CommunityGoal entity.
/// </summary>
public class CommunityGoalRepository : CosmosRepositoryBase<CommunityGoal>, ICommunityGoalRepository
{
    public CommunityGoalRepository(Container container, ILogger<CommunityGoalRepository> logger)
        : base(container, logger)
    {
    }

    public async Task<QueryResult<CommunityGoal>> GetCurrentWeekGoalAsync(
        CancellationToken cancellationToken = default)
    {
        // Find the Monday of the current week
        var today = DateTime.UtcNow;
        var daysUntilMonday = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
        var monday = DateOnly.FromDateTime(today.AddDays(-daysUntilMonday));
        var mondayString = monday.ToString("yyyy-MM-dd");

        var result = await QueryAsync(
            "SELECT * FROM c WHERE c.weekStart = @weekStart",
            partitionKey: mondayString,
            parameters: new Dictionary<string, object> { { "weekStart", mondayString } },
            cancellationToken: cancellationToken);

        if (result.IsFailure)
        {
            return QueryResult<CommunityGoal>.Failure(result.Error);
        }

        var goal = result.Value.FirstOrDefault();
        if (goal is null)
        {
            return QueryResult<CommunityGoal>.Failure(
                Error.NotFound("CommunityGoal", mondayString));
        }

        return QueryResult<CommunityGoal>.Success(goal, result.RequestCharge);
    }

    public async Task<QueryResult<IReadOnlyList<CommunityGoal>>> GetByDateRangeAsync(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            "SELECT * FROM c WHERE c.weekStart >= @startDate AND c.weekStart <= @endDate ORDER BY c.weekStart DESC",
            parameters: new Dictionary<string, object>
            {
                { "startDate", startDate.ToString("yyyy-MM-dd") },
                { "endDate", endDate.ToString("yyyy-MM-dd") }
            },
            cancellationToken: cancellationToken);
    }
}
