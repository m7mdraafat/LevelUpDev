using System.Text.Json.Serialization;
using LevelUpDev.Domain.Common;

namespace LevelUpDev.Domain.Entities;

/// <summary>
/// Represents a squad (team) for team competitions.
/// Partition Key: /id (each squad is its own partition)
/// </summary>
public class Squad : BaseEntity
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("avatarUrl")]
    public string? AvatarUrl { get; set; }

    [JsonPropertyName("captainUserId")]
    public string CaptainUserId { get; set; } = string.Empty;

    [JsonPropertyName("memberIds")]
    public List<string> MemberIds { get; set; } = new();

    [JsonPropertyName("maxMembers")]
    public int MaxMembers { get; set; } = 5;

    [JsonPropertyName("isRecruiting")]
    public bool IsRecruiting { get; set; } = true;

    [JsonPropertyName("totalPoints")]
    public int TotalPoints { get; set; }

    [JsonPropertyName("weeklyPoints")]
    public int WeeklyPoints { get; set; }

    [JsonPropertyName("wins")]
    public int Wins { get; set; }

    [JsonPropertyName("losses")]
    public int Losses { get; set; }

    [JsonPropertyName("currentBattleId")]
    public string? CurrentBattleId { get; set; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new(); // e.g., "Competitive", "Casual", "DSA Focus"

    [JsonIgnore]
    public override string PartitionKeyValue => Id;

    public int MemberCount => MemberIds.Count;
    public bool IsFull => MemberCount >= MaxMembers;
}
