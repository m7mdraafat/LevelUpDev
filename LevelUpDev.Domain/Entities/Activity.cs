using System.Text.Json.Serialization;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Domain.Entities;

/// <summary>
/// Represents a user activity/event.
/// Partition Key: /date (ISO format YYYY-MM-DD, optimized for date-based queries)
/// </summary>
public class Activity : BaseEntity
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("activityType")]
    public ActivityType ActivityType { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonIgnore]
    public override string PartitionKeyValue => Date.ToString("yyyy-MM-dd");
}

/// <summary>
/// Types of activities that can be tracked.
/// </summary>
public enum ActivityType
{
    ProblemSolved,
    StreakMaintained,
    StreakLost,
    QuestLevelCompleted,
    QuestCompleted,
    BadgeEarned,
    ChallengeCompleted,
    SquadJoined,
    SquadBattleWon,
    MentorHelped,
    ContestParticipated
}
