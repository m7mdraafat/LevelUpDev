using System.Text.Json.Serialization;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Domain.Entities;

/// <summary>
/// Represents a user's LeetCode statistics synced from LeetCode API.
/// Partition Key: /userId (optimized for user-based queries)
/// </summary>
public class UserStats : BaseEntity
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("leetCodeUsername")]
    public string LeetCodeUsername { get; set; } = string.Empty;

    [JsonPropertyName("lastSyncedAt")]
    public DateTime LastSyncedAt { get; set; } = DateTime.UtcNow;

    // Problem Statistics
    [JsonPropertyName("totalSolved")]
    public int TotalSolved { get; set; }

    [JsonPropertyName("easySolved")]
    public int EasySolved { get; set; }

    [JsonPropertyName("mediumSolved")]
    public int MediumSolved { get; set; }

    [JsonPropertyName("hardSolved")]
    public int HardSolved { get; set; }

    // Streak Information
    [JsonPropertyName("currentStreak")]
    public int CurrentStreak { get; set; }

    [JsonPropertyName("maxStreak")]
    public int MaxStreak { get; set; }

    [JsonPropertyName("lastSubmissionDate")]
    public DateOnly? LastSubmissionDate { get; set; }

    // Contest Information
    [JsonPropertyName("contestRating")]
    public double ContestRating { get; set; }

    [JsonPropertyName("contestsAttended")]
    public int ContestsAttended { get; set; }

    [JsonPropertyName("globalRanking")]
    public int? GlobalRanking { get; set; }

    // Quest Progress
    [JsonPropertyName("questProgress")]
    public Dictionary<QuestType, QuestProgress> QuestProgress { get; set; } = new();

    // Weekly/Monthly Stats
    [JsonPropertyName("problemsSolvedThisWeek")]
    public int ProblemsSolvedThisWeek { get; set; }

    [JsonPropertyName("problemsSolvedThisMonth")]
    public int ProblemsSolvedThisMonth { get; set; }

    // Freeze Tokens (Gamification)
    [JsonPropertyName("freezeTokens")]
    public int FreezeTokens { get; set; }

    [JsonPropertyName("streakShields")]
    public int StreakShields { get; set; }

    [JsonIgnore]
    public override string PartitionKeyValue => UserId;
}

/// <summary>
/// Progress for a specific quest.
/// </summary>
public class QuestProgress
{
    [JsonPropertyName("questType")]
    public QuestType QuestType { get; set; }

    [JsonPropertyName("currentLevel")]
    public int CurrentLevel { get; set; }

    [JsonPropertyName("totalLevels")]
    public int TotalLevels { get; set; }

    [JsonPropertyName("problemsCompleted")]
    public int ProblemsCompleted { get; set; }

    [JsonPropertyName("problemsRequired")]
    public int ProblemsRequired { get; set; }

    [JsonPropertyName("isCompleted")]
    public bool IsCompleted { get; set; }

    [JsonPropertyName("completedAt")]
    public DateTime? CompletedAt { get; set; }

    [JsonPropertyName("currentThemeZone")]
    public string? CurrentThemeZone { get; set; }

    public double ProgressPercentage => TotalLevels > 0
        ? Math.Round((double)CurrentLevel / TotalLevels * 100, 2)
        : 0;
}
