namespace LevelUpDev.Application.DTOs.Challenges;

/// <summary>
/// DTO for daily challenge.
/// </summary>
public record DailyChallengeDto(
    string Id,
    DateOnly Date,
    string Title,
    string? Description,
    string? LeetCodeProblemId,
    string? LeetCodeProblemUrl,
    string Difficulty,
    int Points,
    int BonusPoints,
    int ParticipantCount,
    int CompletionCount,
    double CompletionRate,
    bool IsActive,
    bool HasUserCompleted,
    bool HasUserParticipated
);

/// <summary>
/// DTO for completing a challenge.
/// </summary>
public record CompleteChallengeRequest(
    string ChallengeId,
    string? SubmissionUrl,
    int? TimeTakenMinutes
);

/// <summary>
/// DTO for challenge leaderboard entry.
/// </summary>
public record ChallengeLeaderboardEntryDto(
    int Rank,
    string UserId,
    string DisplayName,
    string? AvatarUrl,
    int ChallengesCompleted,
    int TotalPoints,
    int CurrentStreak,
    DateTime LastCompletedAt
);

/// <summary>
/// DTO for challenge history.
/// </summary>
public record ChallengeHistoryDto(
    List<DailyChallengeDto> Challenges,
    int TotalCompleted,
    int TotalParticipated,
    double CompletionRate,
    int CurrentStreak
);

/// <summary>
/// DTO for community goal.
/// </summary>
public record CommunityGoalDto(
    string Id,
    DateOnly WeekStart,
    string Title,
    string Description,
    int TargetValue,
    int CurrentValue,
    double ProgressPercentage,
    bool IsCompleted,
    DateTime? CompletedAt,
    List<TopContributorDto> TopContributors
);

/// <summary>
/// DTO for top contributor to community goal.
/// </summary>
public record TopContributorDto(
    string UserId,
    string DisplayName,
    string? AvatarUrl,
    int Contribution
);

/// <summary>
/// DTO for creating a daily challenge (admin).
/// </summary>
public record CreateDailyChallengeRequest(
    DateOnly Date,
    string Title,
    string? Description,
    string? LeetCodeProblemId,
    string? LeetCodeProblemUrl,
    string Difficulty,
    int Points,
    int? BonusPoints
);

/// <summary>
/// DTO for creating a community goal (admin).
/// </summary>
public record CreateCommunityGoalRequest(
    DateOnly WeekStart,
    string Title,
    string Description,
    int TargetValue
);
