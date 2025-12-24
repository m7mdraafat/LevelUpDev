namespace LevelUpDev.Application.DTOs.Squads;

/// <summary>
/// DTO for squad details.
/// </summary>
public record SquadDto(
    string Id,
    string Name,
    string? Description,
    string? AvatarUrl,
    string CaptainUserId,
    string CaptainDisplayName,
    List<SquadMemberDto> Members,
    int MaxMembers,
    bool IsRecruiting,
    int TotalPoints,
    int WeeklyPoints,
    int Wins,
    int Losses,
    List<string> Tags,
    DateTime CreatedAt
);

/// <summary>
/// DTO for squad member.
/// </summary>
public record SquadMemberDto(
    string UserId,
    string DisplayName,
    string? AvatarUrl,
    string Title,
    int ContributedPoints,
    bool IsCaptain
);

/// <summary>
/// DTO for creating a squad.
/// </summary>
public record CreateSquadRequest(
    string Name,
    string? Description,
    string? AvatarUrl,
    List<string>? Tags
);

/// <summary>
/// DTO for updating a squad.
/// </summary>
public record UpdateSquadRequest(
    string? Name,
    string? Description,
    string? AvatarUrl,
    bool? IsRecruiting,
    List<string>? Tags
);

/// <summary>
/// DTO for squad search result.
/// </summary>
public record SquadSearchResultDto(
    string Id,
    string Name,
    string? AvatarUrl,
    int MemberCount,
    int MaxMembers,
    bool IsRecruiting,
    int TotalPoints,
    List<string> Tags
);

/// <summary>
/// DTO for squad leaderboard entry.
/// </summary>
public record SquadLeaderboardEntryDto(
    int Rank,
    string SquadId,
    string SquadName,
    string? AvatarUrl,
    int MemberCount,
    int TotalPoints,
    int WeeklyPoints,
    int Wins,
    int Losses,
    double WinRate
);

/// <summary>
/// DTO for squad battle.
/// </summary>
public record SquadBattleDto(
    string BattleId,
    SquadBattleParticipantDto Squad1,
    SquadBattleParticipantDto Squad2,
    DateTime StartTime,
    DateTime EndTime,
    string Status,
    string? WinnerSquadId
);

/// <summary>
/// DTO for squad battle participant.
/// </summary>
public record SquadBattleParticipantDto(
    string SquadId,
    string SquadName,
    int Score,
    int ProblemsCompleted
);
