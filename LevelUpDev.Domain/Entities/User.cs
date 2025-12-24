using System.Text.Json.Serialization;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Domain.Entities;

/// <summary>
/// Represents a user in the LevelUpDev community.
/// Partition Key: /id (each user is their own partition for scalability)
/// </summary>
public class User : BaseEntity
{
    [JsonPropertyName("githubId")]
    public string GitHubId { get; set; } = string.Empty;

    [JsonPropertyName("githubUsername")]
    public string GitHubUsername { get; set; } = string.Empty;

    [JsonPropertyName("leetCodeUsername")]
    public string LeetCodeUsername { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("avatarUrl")]
    public string? AvatarUrl { get; set; }

    [JsonPropertyName("role")]
    public UserRole Role { get; set; } = UserRole.Member;

    [JsonPropertyName("title")]
    public UserTitle Title { get; set; } = UserTitle.Newcomer;

    [JsonPropertyName("profileTheme")]
    public ProfileTheme ProfileTheme { get; set; } = ProfileTheme.Default;

    [JsonPropertyName("squadId")]
    public string? SquadId { get; set; }

    [JsonPropertyName("showcaseBadges")]
    public List<string> ShowcaseBadges { get; set; } = new(3); // Top 3 displayed

    [JsonPropertyName("settings")]
    public UserSettings Settings { get; set; } = new();

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; } = true;

    [JsonPropertyName("lastActiveAt")]
    public DateTime LastActiveAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public override string PartitionKeyValue => Id;
}

/// <summary>
/// User notification and display settings.
/// </summary>
public class UserSettings
{
    [JsonPropertyName("enablePushNotifications")]
    public bool EnablePushNotifications { get; set; } = true;

    [JsonPropertyName("enableEmailNotifications")]
    public bool EnableEmailNotifications { get; set; } = false;

    [JsonPropertyName("streakReminderTime")]
    public TimeOnly StreakReminderTime { get; set; } = new(18, 0); // 6 PM default

    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = "UTC";

    [JsonPropertyName("showOnLeaderboard")]
    public bool ShowOnLeaderboard { get; set; } = true;
}
