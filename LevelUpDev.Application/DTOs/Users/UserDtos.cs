namespace LevelUpDev.Application.DTOs.Users;

/// <summary>
/// DTO for user profile response.
/// </summary>
public record UserProfileDto(
    string Id,
    string GitHubUsername,
    string LeetCodeUsername,
    string DisplayName,
    string? AvatarUrl,
    string Role,
    string Title,
    string ProfileTheme,
    string? SquadId,
    string? SquadName,
    List<BadgeDto> ShowcaseBadges,
    bool IsActive,
    DateTime LastActiveAt,
    DateTime CreatedAt
);

/// <summary>
/// Alias for UserProfileDto for API compatibility.
/// </summary>
public record UserDto(
    string Id,
    string GitHubUsername,
    string LeetCodeUsername,
    string DisplayName,
    string? AvatarUrl,
    string Role,
    string Title,
    string ProfileTheme,
    string? SquadId,
    string? SquadName,
    List<BadgeDto>? ShowcaseBadges,
    bool IsActive,
    DateTime LastActiveAt,
    DateTime CreatedAt
);

/// <summary>
/// DTO for badge display.
/// </summary>
public record BadgeDto(
    string Id,
    string Name,
    string Description,
    string Icon,
    string Rarity,
    bool IsUnlocked,
    DateTime? UnlockedAt
);

/// <summary>
/// DTO for user registration request.
/// </summary>
public record RegisterUserRequest(
    string GitHubId,
    string GitHubUsername,
    string LeetCodeUsername,
    string DisplayName,
    string? Email,
    string? AvatarUrl
);

/// <summary>
/// DTO for updating user profile.
/// </summary>
public record UpdateUserProfileRequest(
    string? DisplayName,
    string? LeetCodeUsername,
    string? AvatarUrl,
    string? ProfileTheme,
    List<string>? ShowcaseBadgeIds
);

/// <summary>
/// Alias for UpdateUserProfileRequest for API compatibility.
/// </summary>
public record UpdateUserRequest(
    string? DisplayName,
    string? LeetCodeUsername,
    string? AvatarUrl,
    string? ProfileTheme,
    List<string>? ShowcaseBadgeIds
);

/// <summary>
/// DTO for user settings.
/// </summary>
public record UserSettingsDto(
    bool EnablePushNotifications,
    bool EnableEmailNotifications,
    string StreakReminderTime,
    string Timezone,
    bool ShowOnLeaderboard
);

/// <summary>
/// DTO for updating user settings.
/// </summary>
public record UpdateUserSettingsRequest(
    bool? EnablePushNotifications,
    bool? EnableEmailNotifications,
    string? StreakReminderTime,
    string? Timezone,
    bool? ShowOnLeaderboard
);

/// <summary>
/// DTO for user search result.
/// </summary>
public record UserSearchResultDto(
    string Id,
    string DisplayName,
    string LeetCodeUsername,
    string? AvatarUrl,
    string Title
);
