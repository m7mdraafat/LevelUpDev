using LevelUpDev.Application.DTOs.Quests;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Application.Interfaces;

/// <summary>
/// Service interface for quest operations.
/// </summary>
public interface IQuestService
{
    /// <summary>
    /// Gets the quest world map for a user.
    /// </summary>
    Task<Result<QuestWorldMapDto>> GetWorldMapAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets details for a specific quest.
    /// </summary>
    Task<Result<QuestDto>> GetQuestAsync(
        string userId,
        QuestType questType,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets details for a specific quest level.
    /// </summary>
    Task<Result<QuestLevelDto>> GetQuestLevelAsync(
        string userId,
        QuestType questType,
        int level,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates quest progress after syncing LeetCode stats.
    /// </summary>
    Task<Result> UpdateProgressAsync(
        string userId,
        CancellationToken cancellationToken = default);
}
