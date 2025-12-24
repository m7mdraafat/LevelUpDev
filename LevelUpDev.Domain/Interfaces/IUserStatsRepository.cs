using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;

namespace LevelUpDev.Domain.Interfaces;

/// <summary>
/// Repository interface for UserStats entity operations.
/// </summary>
public interface IUserStatsRepository : ICosmosRepository<UserStats>
{
    /// <summary>
    /// Gets stats by user ID.
    /// </summary>
    Task<QueryResult<UserStats>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all user stats ordered by total solved (for leaderboard).
    /// </summary>
    Task<QueryResult<IReadOnlyList<UserStats>>> GetTopByTotalSolvedAsync(
        int limit = 100,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all user stats ordered by current streak (for leaderboard).
    /// </summary>
    Task<QueryResult<IReadOnlyList<UserStats>>> GetTopByStreakAsync(
        int limit = 100,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all user stats ordered by weekly problems solved (for leaderboard).
    /// </summary>
    Task<QueryResult<IReadOnlyList<UserStats>>> GetTopByWeeklySolvedAsync(
        int limit = 100,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all user stats ordered by hard problems solved (for leaderboard).
    /// </summary>
    Task<QueryResult<IReadOnlyList<UserStats>>> GetTopByHardSolvedAsync(
        int limit = 100,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets users who need sync (last synced > threshold).
    /// </summary>
    Task<QueryResult<IReadOnlyList<UserStats>>> GetStaleStatsAsync(
        TimeSpan threshold,
        CancellationToken cancellationToken = default);
}
