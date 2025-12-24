using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;

namespace LevelUpDev.Domain.Interfaces;

/// <summary>
/// Repository interface for DailyChallenge entity operations.
/// </summary>
public interface IChallengeRepository : ICosmosRepository<DailyChallenge>
{
    /// <summary>
    /// Gets the challenge for a specific date.
    /// </summary>
    Task<QueryResult<DailyChallenge>> GetByDateAsync(
        DateOnly date,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets today's challenge.
    /// </summary>
    Task<QueryResult<DailyChallenge>> GetTodaysChallengeAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets challenges for a date range.
    /// </summary>
    Task<QueryResult<IReadOnlyList<DailyChallenge>>> GetByDateRangeAsync(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets active challenges.
    /// </summary>
    Task<QueryResult<IReadOnlyList<DailyChallenge>>> GetActiveAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets user's challenge completion history.
    /// </summary>
    Task<QueryResult<IReadOnlyList<DailyChallenge>>> GetCompletedByUserAsync(
        string userId,
        int limit = 30,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Repository interface for CommunityGoal entity operations.
/// </summary>
public interface ICommunityGoalRepository : ICosmosRepository<CommunityGoal>
{
    /// <summary>
    /// Gets the current week's goal.
    /// </summary>
    Task<QueryResult<CommunityGoal>> GetCurrentWeekGoalAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets goals by date range.
    /// </summary>
    Task<QueryResult<IReadOnlyList<CommunityGoal>>> GetByDateRangeAsync(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken = default);
}
