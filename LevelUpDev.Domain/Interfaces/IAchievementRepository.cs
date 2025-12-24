using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Domain.Interfaces;

/// <summary>
/// Repository interface for Achievement entity operations.
/// </summary>
public interface IAchievementRepository : ICosmosRepository<Achievement>
{
    /// <summary>
    /// Gets all achievements for a user.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Achievement>>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets unlocked achievements for a user.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Achievement>>> GetUnlockedByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific achievement for a user by type.
    /// </summary>
    Task<QueryResult<Achievement>> GetByUserAndTypeAsync(
        string userId,
        AchievementType achievementType,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets users who have unlocked a specific achievement.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Achievement>>> GetByAchievementTypeAsync(
        AchievementType achievementType,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets achievements by rarity.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Achievement>>> GetByRarityAsync(
        BadgeRarity rarity,
        CancellationToken cancellationToken = default);
}
