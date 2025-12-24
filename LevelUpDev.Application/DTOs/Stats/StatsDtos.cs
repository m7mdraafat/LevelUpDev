using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Application.DTOs.Stats;

/// <summary>
/// DTO for user statistics.
/// </summary>
public record UserStatsDto(
    string UserId,
    string LeetCodeUsername,
    DateTime LastSyncedAt,
    ProblemStatsDto ProblemStats,
    StreakStatsDto StreakStats,
    ContestStatsDto ContestStats,
    List<QuestProgressDto> QuestProgress,
    WeeklyStatsDto WeeklyStats,
    GamificationStatsDto GamificationStats
);

/// <summary>
/// DTO for problem statistics.
/// </summary>
public record ProblemStatsDto(
    int TotalSolved,
    int EasySolved,
    int MediumSolved,
    int HardSolved
);

/// <summary>
/// DTO for streak statistics.
/// </summary>
public record StreakStatsDto(
    int CurrentStreak,
    int MaxStreak,
    DateOnly? LastSubmissionDate,
    bool IsAtRisk
);

/// <summary>
/// DTO for streak information.
/// </summary>
public record StreakInfoDto(
    int CurrentStreak,
    int MaxStreak,
    DateOnly? LastSubmissionDate,
    bool IsAtRisk,
    int FreezeTokens,
    int StreakShields,
    List<DateOnly> RecentActivity
);

/// <summary>
/// DTO for contest statistics.
/// </summary>
public record ContestStatsDto(
    double ContestRating,
    int ContestsAttended,
    int? GlobalRanking
);

/// <summary>
/// DTO for quest progress.
/// </summary>
public record QuestProgressDto(
    string QuestType,
    int CurrentLevel,
    int TotalLevels,
    int ProblemsCompleted,
    int ProblemsRequired,
    double ProgressPercentage,
    bool IsCompleted,
    string? CurrentThemeZone
);

/// <summary>
/// DTO for weekly/monthly stats.
/// </summary>
public record WeeklyStatsDto(
    int ProblemsSolvedThisWeek,
    int ProblemsSolvedThisMonth
);

/// <summary>
/// DTO for weekly progress.
/// </summary>
public record WeeklyProgressDto(
    DateOnly WeekStart,
    DateOnly WeekEnd,
    int ProblemsSolved,
    int EasySolved,
    int MediumSolved,
    int HardSolved,
    int PointsEarned,
    List<DailyActivityDto> DailyBreakdown
);

/// <summary>
/// DTO for daily activity.
/// </summary>
public record DailyActivityDto(
    DateOnly Date,
    int ProblemsSolved,
    int PointsEarned,
    bool MaintainedStreak,
    List<string> CompletedChallenges
);

/// <summary>
/// DTO for gamification stats.
/// </summary>
public record GamificationStatsDto(
    int FreezeTokens,
    int StreakShields
);

/// <summary>
/// DTO for personal analytics dashboard.
/// </summary>
public record PersonalAnalyticsDashboardDto(
    UserStatsDto Stats,
    List<ActivitySummaryDto> RecentActivity,
    List<LeaderboardPositionDto> LeaderboardPositions,
    List<QuestProgressDto> QuestProgress,
    StreakAnalyticsDto StreakAnalytics,
    List<BadgeProgressDto> BadgeProgress
);

/// <summary>
/// DTO for activity summary.
/// </summary>
public record ActivitySummaryDto(
    DateTime Date,
    string ActivityType,
    string Description,
    int Points
);

/// <summary>
/// DTO for leaderboard position.
/// </summary>
public record LeaderboardPositionDto(
    string LeaderboardType,
    int Rank,
    int? PreviousRank,
    double Score,
    int RankChange
);

/// <summary>
/// DTO for streak analytics.
/// </summary>
public record StreakAnalyticsDto(
    int CurrentStreak,
    int MaxStreak,
    int TotalStreakDays,
    List<int> StreakHistory
);

/// <summary>
/// DTO for badge progress.
/// </summary>
public record BadgeProgressDto(
    string BadgeId,
    string Name,
    string Icon,
    string Rarity,
    int Progress,
    int RequiredProgress,
    double ProgressPercentage,
    bool IsUnlocked
);
