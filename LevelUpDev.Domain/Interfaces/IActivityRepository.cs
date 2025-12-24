using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;

namespace LevelUpDev.Domain.Interfaces;

/// <summary>
/// Repository interface for Activity entity operations.
/// </summary>
public interface IActivityRepository : ICosmosRepository<Activity>
{
    /// <summary>
    /// Gets activities for a user.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Activity>>> GetByUserIdAsync(
        string userId,
        int limit = 50,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets activities for a specific date.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Activity>>> GetByDateAsync(
        DateOnly date,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets activities by date range.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Activity>>> GetByDateRangeAsync(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets community feed (recent activities from all users).
    /// </summary>
    Task<QueryResult<IReadOnlyList<Activity>>> GetCommunityFeedAsync(
        int limit = 50,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets activities by type.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Activity>>> GetByTypeAsync(
        ActivityType activityType,
        int limit = 50,
        CancellationToken cancellationToken = default);
}
