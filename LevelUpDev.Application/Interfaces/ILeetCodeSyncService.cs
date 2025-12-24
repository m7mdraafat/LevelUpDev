using LevelUpDev.Application.DTOs.LeetCode;
using LevelUpDev.Domain.Common;

namespace LevelUpDev.Application.Interfaces;

/// <summary>
/// Service interface for LeetCode API integration.
/// </summary>
public interface ILeetCodeSyncService
{
    /// <summary>
    /// Fetches and syncs stats for a user from LeetCode API.
    /// </summary>
    Task<Result<LeetCodeSyncResultDto>> SyncUserStatsAsync(
        string leetCodeUsername,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches user profile from LeetCode.
    /// </summary>
    Task<Result<LeetCodeUserProfileDto>> GetUserProfileAsync(
        string leetCodeUsername,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches problem stats from LeetCode.
    /// </summary>
    Task<Result<LeetCodeProblemStatsDto>> GetProblemStatsAsync(
        string leetCodeUsername,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches contest stats from LeetCode.
    /// </summary>
    Task<Result<LeetCodeContestStatsDto>> GetContestStatsAsync(
        string leetCodeUsername,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches streak info from LeetCode.
    /// </summary>
    Task<Result<LeetCodeStreakDto>> GetStreakInfoAsync(
        string leetCodeUsername,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates a LeetCode username exists.
    /// </summary>
    Task<Result<bool>> ValidateUsernameAsync(
        string leetCodeUsername,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Syncs all stale user stats (background job).
    /// </summary>
    Task<Result> SyncAllStaleAsync(
        TimeSpan threshold,
        CancellationToken cancellationToken = default);
}
