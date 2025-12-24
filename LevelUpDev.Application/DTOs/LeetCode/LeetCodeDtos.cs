namespace LevelUpDev.Application.DTOs.LeetCode;

/// <summary>
/// DTO for LeetCode user profile from API.
/// </summary>
public record LeetCodeUserProfileDto(
    string Username,
    string? RealName,
    string? Avatar,
    int Ranking,
    int Reputation
);

/// <summary>
/// DTO for LeetCode problem solving stats from API.
/// </summary>
public record LeetCodeProblemStatsDto(
    int TotalSolved,
    int EasySolved,
    int MediumSolved,
    int HardSolved,
    int TotalProblems,
    int EasyTotal,
    int MediumTotal,
    int HardTotal
);

/// <summary>
/// DTO for LeetCode contest stats from API.
/// </summary>
public record LeetCodeContestStatsDto(
    double Rating,
    int GlobalRanking,
    int TotalParticipants,
    int ContestsAttended,
    double TopPercentage
);

/// <summary>
/// DTO for LeetCode submission stats from API.
/// </summary>
public record LeetCodeSubmissionStatsDto(
    int TotalSubmissions,
    int AcceptedSubmissions,
    Dictionary<string, int> SubmissionsByLanguage
);

/// <summary>
/// DTO for LeetCode streak info from API.
/// </summary>
public record LeetCodeStreakDto(
    int CurrentStreak,
    int MaxStreak,
    DateOnly? LastSubmissionDate,
    List<DateOnly> SubmissionCalendar
);

/// <summary>
/// DTO for complete LeetCode sync response.
/// </summary>
public record LeetCodeSyncResultDto(
    string Username,
    LeetCodeProblemStatsDto ProblemStats,
    LeetCodeContestStatsDto? ContestStats,
    LeetCodeStreakDto StreakInfo,
    DateTime SyncedAt
);
