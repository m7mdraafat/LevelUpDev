using System.Text.Json.Serialization;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Domain.Entities;

/// <summary>
/// Represents a leaderboard entry.
/// Partition Key: /type (optimized for leaderboard-type queries)
/// </summary>
public class LeaderboardEntry : BaseEntity
{
    [JsonPropertyName("type")]
    public LeaderboardType Type { get; set; }

    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("avatarUrl")]
    public string? AvatarUrl { get; set; }

    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("previousRank")]
    public int? PreviousRank { get; set; }

    [JsonPropertyName("score")]
    public double Score { get; set; }

    [JsonPropertyName("previousScore")]
    public double? PreviousScore { get; set; }

    [JsonPropertyName("squadId")]
    public string? SquadId { get; set; }

    [JsonPropertyName("squadName")]
    public string? SquadName { get; set; }

    [JsonPropertyName("periodStart")]
    public DateTime PeriodStart { get; set; }

    [JsonPropertyName("periodEnd")]
    public DateTime? PeriodEnd { get; set; }

    [JsonIgnore]
    public override string PartitionKeyValue => Type.ToString();

    public int RankChange => PreviousRank.HasValue ? PreviousRank.Value - Rank : 0;
    public double ScoreChange => PreviousScore.HasValue ? Score - PreviousScore.Value : 0;
}

/// <summary>
/// Leaderboard metadata.
/// </summary>
public class LeaderboardMetadata
{
    public LeaderboardType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public LeaderboardRefreshRate RefreshRate { get; set; }
    public DateTime LastUpdated { get; set; }
}

/// <summary>
/// Static leaderboard definitions.
/// </summary>
public static class LeaderboardDefinitions
{
    public static readonly Dictionary<LeaderboardType, (string Name, string Description, string Icon, LeaderboardRefreshRate RefreshRate)> Leaderboards = new()
    {
        { LeaderboardType.QuestChampions, ("Quest Champions", "Quest level completion", "🥇", LeaderboardRefreshRate.RealTime) },
        { LeaderboardType.StreakKings, ("Streak Kings", "Current streak days", "🔥", LeaderboardRefreshRate.Daily) },
        { LeaderboardType.SpeedDemons, ("Speed Demons", "Problems solved this week", "⚡", LeaderboardRefreshRate.Weekly) },
        { LeaderboardType.HardCrushers, ("Hard Crushers", "Hard problems solved", "🧠", LeaderboardRefreshRate.Monthly) },
        { LeaderboardType.ContestWarriors, ("Contest Warriors", "Contest rating", "📊", LeaderboardRefreshRate.AfterContests) },
        { LeaderboardType.RisingStars, ("Rising Stars", "Most improved (% gain)", "🌟", LeaderboardRefreshRate.Weekly) }
    };
}
