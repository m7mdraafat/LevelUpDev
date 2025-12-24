namespace LevelUpDev.Domain.Enums;

/// <summary>
/// Types of notifications.
/// </summary>
public enum NotificationType
{
    StreakAtRisk,         // "Your streak is at risk!" - No submission by 6 PM
    LeaderboardChange,    // "Ahmed just passed you!"
    BadgeUnlocked,        // "New badge unlocked!"
    SquadNeedsYou,        // "Your squad needs you!" - Squad falling behind
    CommunityGoal,        // "Community goal: 89% complete!"
    QuestLevelUnlocked,   // "New Quest level unlocked!"
    SquadInvite,
    MentionInChat,
    DailyChallenge,
    WeeklyEvent
}

/// <summary>
/// Priority levels for notifications.
/// </summary>
public enum NotificationPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Urgent = 4
}
