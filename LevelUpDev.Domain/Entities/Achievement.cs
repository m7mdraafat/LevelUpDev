using System.Text.Json.Serialization;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Domain.Entities;

/// <summary>
/// Represents a user's achievement/badge.
/// Partition Key: /userId (optimized for user-based queries)
/// </summary>
public class Achievement : BaseEntity
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("achievementType")]
    public AchievementType AchievementType { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("rarity")]
    public BadgeRarity Rarity { get; set; }

    [JsonPropertyName("isUnlocked")]
    public bool IsUnlocked { get; set; }

    [JsonPropertyName("unlockedAt")]
    public DateTime? UnlockedAt { get; set; }

    [JsonPropertyName("progress")]
    public int Progress { get; set; }

    [JsonPropertyName("requiredProgress")]
    public int RequiredProgress { get; set; }

    [JsonIgnore]
    public override string PartitionKeyValue => UserId;

    public double ProgressPercentage => RequiredProgress > 0
        ? Math.Round((double)Progress / RequiredProgress * 100, 2)
        : 0;
}

/// <summary>
/// Badge definitions (static data).
/// </summary>
public static class BadgeDefinitions
{
    public static readonly Dictionary<AchievementType, (string Name, string Description, string Icon, BadgeRarity Rarity, int RequiredProgress)> Badges = new()
    {
        { AchievementType.FirstSteps, ("First Steps", "Complete Quest Level 1", "🌱", BadgeRarity.Common, 1) },
        { AchievementType.OnFire, ("On Fire", "7-day streak", "🔥", BadgeRarity.Common, 7) },
        { AchievementType.Lightning, ("Lightning", "30-day streak", "⚡", BadgeRarity.Rare, 30) },
        { AchievementType.Unstoppable, ("Unstoppable", "100-day streak", "🌋", BadgeRarity.Epic, 100) },
        { AchievementType.QuestMaster, ("Quest Master", "Complete any Quest", "👑", BadgeRarity.Legendary, 1) },
        { AchievementType.CommunityChampion, ("Community Champion", "#1 on any leaderboard", "🏆", BadgeRarity.Legendary, 1) },
        { AchievementType.DsaSage, ("DSA Sage", "Complete all 35 DSA levels", "🌟", BadgeRarity.Mythic, 35) },
        { AchievementType.Polyglot, ("Polyglot", "Complete all 4 Quests", "💎", BadgeRarity.Mythic, 4) },
        { AchievementType.Mentor, ("Mentor", "Help 10 members", "🤝", BadgeRarity.Special, 10) },
        { AchievementType.Sharpshooter, ("Sharpshooter", "100% daily challenge completion for a month", "🎯", BadgeRarity.Epic, 30) }
    };
}
