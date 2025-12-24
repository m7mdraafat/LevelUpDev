namespace LevelUpDev.Application.DTOs.Achievements;

/// <summary>
/// DTO for achievement.
/// </summary>
public record AchievementDto(
    string Id,
    string Name,
    string Description,
    string Icon,
    string Category,
    string Rarity,
    int Points,
    bool IsUnlocked,
    DateTime? UnlockedAt,
    int Progress,
    int RequiredProgress,
    double ProgressPercentage,
    string? UnlockCondition
);

/// <summary>
/// DTO for achievement list by category.
/// </summary>
public record AchievementCategoryDto(
    string Category,
    string CategoryName,
    string? Icon,
    List<AchievementDto> Achievements,
    int UnlockedCount,
    int TotalCount
);

/// <summary>
/// DTO for user's achievement summary.
/// </summary>
public record AchievementSummaryDto(
    int TotalAchievements,
    int UnlockedAchievements,
    int TotalPoints,
    int EarnedPoints,
    List<AchievementDto> RecentUnlocks,
    List<AchievementDto> AlmostComplete
);

/// <summary>
/// DTO for achievement unlock notification.
/// </summary>
public record AchievementUnlockDto(
    string AchievementId,
    string Name,
    string Description,
    string Icon,
    string Rarity,
    int Points,
    DateTime UnlockedAt
);
