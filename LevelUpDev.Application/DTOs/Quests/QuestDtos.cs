namespace LevelUpDev.Application.DTOs.Quests;

/// <summary>
/// DTO for quest world map.
/// </summary>
public record QuestWorldMapDto(
    List<QuestDto> Quests,
    int TotalLevelsCompleted,
    int TotalLevels,
    double OverallProgress
);

/// <summary>
/// Alias for QuestWorldMapDto.
/// </summary>
public record WorldMapDto(
    List<QuestDto> Quests,
    int TotalLevelsCompleted,
    int TotalLevels,
    double OverallProgress
);

/// <summary>
/// DTO for a quest.
/// </summary>
public record QuestDto(
    string QuestType,
    string Name,
    string Description,
    int TotalLevels,
    int CurrentLevel,
    double ProgressPercentage,
    bool IsCompleted,
    List<ThemeZoneDto> ThemeZones
);

/// <summary>
/// DTO for quest progress (compatible with Stats version).
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
/// DTO for a theme zone within a quest.
/// </summary>
public record ThemeZoneDto(
    string Name,
    string Icon,
    int StartLevel,
    int EndLevel,
    bool IsUnlocked,
    bool IsCompleted,
    bool IsCurrent,
    int ProblemsCompleted,
    int ProblemsRequired
);

/// <summary>
/// DTO for quest level details.
/// </summary>
public record QuestLevelDto(
    string QuestType,
    int Level,
    string ThemeZone,
    string Name,
    string? Description,
    List<QuestProblemDto> Problems,
    int ProblemsCompleted,
    int ProblemsRequired,
    bool IsCompleted,
    bool IsUnlocked
);

/// <summary>
/// DTO for a problem in a quest level.
/// </summary>
public record QuestProblemDto(
    string LeetCodeProblemId,
    string Title,
    string Url,
    string Difficulty,
    bool IsCompleted,
    DateTime? CompletedAt
);
