namespace LevelUpDev.Application.DTOs.Leaderboards;

/// <summary>
/// DTO for leaderboard response.
/// </summary>
public record LeaderboardDto(
    string Type,
    string Name,
    string Description,
    string Icon,
    string RefreshRate,
    DateTime LastUpdated,
    List<LeaderboardEntryDto> Entries,
    int TotalEntries
);

/// <summary>
/// DTO for leaderboard entry.
/// </summary>
public record LeaderboardEntryDto(
    int Rank,
    string UserId,
    string DisplayName,
    string? AvatarUrl,
    double Score,
    int? PreviousRank,
    double? PreviousScore,
    int RankChange,
    double ScoreChange,
    string? SquadId,
    string? SquadName
);

/// <summary>
/// DTO for user's position across all leaderboards.
/// </summary>
public record UserLeaderboardSummaryDto(
    string UserId,
    List<UserLeaderboardPositionDto> Positions
);

/// <summary>
/// DTO for user's position in a leaderboard.
/// </summary>
public record UserLeaderboardPositionDto(
    string LeaderboardType,
    string LeaderboardName,
    string Icon,
    int Rank,
    int TotalParticipants,
    double Score,
    int RankChange,
    List<LeaderboardEntryDto> SurroundingEntries
);
