namespace LevelUpDev.Application.DTOs.Notifications;

/// <summary>
/// DTO for notification.
/// </summary>
public record NotificationDto(
    string Id,
    string Type,
    string Title,
    string Message,
    string? Icon,
    string Priority,
    string? ActionUrl,
    bool IsRead,
    DateTime? ReadAt,
    DateTime CreatedAt,
    DateTime? ExpiresAt
);

/// <summary>
/// DTO for notification list.
/// </summary>
public record NotificationListDto(
    List<NotificationDto> Notifications,
    int UnreadCount,
    int TotalCount
);

/// <summary>
/// DTO for notification preferences.
/// </summary>
public record NotificationPreferencesDto(
    bool EnablePushNotifications,
    bool EnableEmailNotifications,
    bool EnableStreakReminders,
    string StreakReminderTime,
    bool EnableSquadNotifications,
    bool EnableAchievementNotifications,
    bool EnableChallengeNotifications,
    bool EnableLeaderboardNotifications,
    string Timezone
);

/// <summary>
/// DTO for updating notification preferences.
/// </summary>
public record UpdateNotificationPreferencesRequest(
    bool? EnablePushNotifications,
    bool? EnableEmailNotifications,
    bool? EnableStreakReminders,
    string? StreakReminderTime,
    bool? EnableSquadNotifications,
    bool? EnableAchievementNotifications,
    bool? EnableChallengeNotifications,
    bool? EnableLeaderboardNotifications,
    string? Timezone
);

/// <summary>
/// DTO for creating a notification (internal use).
/// </summary>
public record CreateNotificationRequest(
    string UserId,
    string Type,
    string Title,
    string Message,
    string? Icon,
    string? Priority,
    string? ActionUrl,
    DateTime? ExpiresAt,
    Dictionary<string, object>? Metadata
);

/// <summary>
/// DTO for marking notifications as read.
/// </summary>
public record MarkNotificationsReadRequest(
    List<string>? NotificationIds,
    bool MarkAll
);
