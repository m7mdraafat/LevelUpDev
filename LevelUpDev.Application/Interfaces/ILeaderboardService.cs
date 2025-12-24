using LevelUpDev.Application.DTOs.Leaderboards;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Application.Interfaces;

/// <summary>
/// Service interface for leaderboard operations.
/// </summary>
public interface ILeaderboardService
{
    /// <summary>
    /// Gets a specific leaderboard.
    /// </summary>
    Task<Result<LeaderboardDto>> GetLeaderboardAsync(
        LeaderboardType type,
        int limit = 100,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all leaderboards with top entries.
    /// </summary>
    Task<Result<List<LeaderboardDto>>> GetAllLeaderboardsAsync(
        int entriesPerBoard = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user's position across all leaderboards.
    /// </summary>
    Task<Result<UserLeaderboardSummaryDto>> GetUserSummaryAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user's position in a specific leaderboard with surrounding entries.
    /// </summary>
    Task<Result<UserLeaderboardPositionDto>> GetUserPositionAsync(
        string userId,
        LeaderboardType type,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Recalculates a specific leaderboard (background job).
    /// </summary>
    Task<Result> RecalculateLeaderboardAsync(
        LeaderboardType type,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Recalculates all leaderboards (background job).
    /// </summary>
    Task<Result> RecalculateAllLeaderboardsAsync(
        CancellationToken cancellationToken = default);
}
