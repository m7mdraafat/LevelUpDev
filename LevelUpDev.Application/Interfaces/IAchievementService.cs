using LevelUpDev.Application.DTOs.Users;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Application.Interfaces;

/// <summary>
/// Service interface for achievement/badge operations.
/// </summary>
public interface IAchievementService
{
    /// <summary>
    /// Gets all achievements for a user.
    /// </summary>
    Task<Result<List<BadgeDto>>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets unlocked achievements for a user.
    /// </summary>
    Task<Result<List<BadgeDto>>> GetUnlockedAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks and awards achievements for a user (background job).
    /// </summary>
    Task<Result<List<BadgeDto>>> CheckAndAwardAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Initializes achievement tracking for a new user.
    /// </summary>
    Task<Result> InitializeForUserAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates progress for a specific achievement type.
    /// </summary>
    Task<Result> UpdateProgressAsync(
        string userId,
        AchievementType achievementType,
        int progress,
        CancellationToken cancellationToken = default);
}
