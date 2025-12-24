using LevelUpDev.Application.DTOs.Stats;
using LevelUpDev.Domain.Common;

namespace LevelUpDev.Application.Interfaces;

/// <summary>
/// Service interface for user statistics operations.
/// </summary>
public interface IUserStatsService
{
    /// <summary>
    /// Gets stats for a user.
    /// </summary>
    Task<Result<UserStatsDto>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets personal analytics dashboard for a user.
    /// </summary>
    Task<Result<PersonalAnalyticsDashboardDto>> GetDashboardAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Triggers a sync of user's LeetCode stats.
    /// </summary>
    Task<Result<UserStatsDto>> SyncLeetCodeStatsAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Uses a freeze token to protect streak.
    /// </summary>
    Task<Result> UseFreezeTokenAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Uses a streak shield.
    /// </summary>
    Task<Result> UseStreakShieldAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gifts a freeze token to another user.
    /// </summary>
    Task<Result> GiftFreezeTokenAsync(
        string fromUserId,
        string toUserId,
        CancellationToken cancellationToken = default);
}
