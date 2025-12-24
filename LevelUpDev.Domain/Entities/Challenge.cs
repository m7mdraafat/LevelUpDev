using System.Text.Json.Serialization;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Domain.Entities;

/// <summary>
/// Represents a daily community challenge.
/// Partition Key: /date (ISO format YYYY-MM-DD, optimized for date-based queries)
/// </summary>
public class DailyChallenge : BaseEntity
{
    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("leetCodeProblemId")]
    public string? LeetCodeProblemId { get; set; }

    [JsonPropertyName("leetCodeProblemUrl")]
    public string? LeetCodeProblemUrl { get; set; }

    [JsonPropertyName("difficulty")]
    public ChallengeDifficulty Difficulty { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("bonusPoints")]
    public int BonusPoints { get; set; } // For early completion

    [JsonPropertyName("participantIds")]
    public List<string> ParticipantIds { get; set; } = new();

    [JsonPropertyName("completedByIds")]
    public List<string> CompletedByIds { get; set; } = new();

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; } = true;

    [JsonIgnore]
    public override string PartitionKeyValue => Date.ToString("yyyy-MM-dd");

    public int ParticipantCount => ParticipantIds.Count;
    public int CompletionCount => CompletedByIds.Count;
    public double CompletionRate => ParticipantCount > 0
        ? Math.Round((double)CompletionCount / ParticipantCount * 100, 2)
        : 0;
}

/// <summary>
/// Community goal for weekly kickoff.
/// </summary>
public class CommunityGoal : BaseEntity
{
    [JsonPropertyName("weekStart")]
    public DateOnly WeekStart { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("targetValue")]
    public int TargetValue { get; set; }

    [JsonPropertyName("currentValue")]
    public int CurrentValue { get; set; }

    [JsonPropertyName("isCompleted")]
    public bool IsCompleted { get; set; }

    [JsonPropertyName("completedAt")]
    public DateTime? CompletedAt { get; set; }

    [JsonIgnore]
    public override string PartitionKeyValue => WeekStart.ToString("yyyy-MM-dd");

    public double ProgressPercentage => TargetValue > 0
        ? Math.Min(100, Math.Round((double)CurrentValue / TargetValue * 100, 2))
        : 0;
}
