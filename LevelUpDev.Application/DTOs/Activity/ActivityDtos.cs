namespace LevelUpDev.Application.DTOs.Activity;

/// <summary>
/// DTO for activity feed entry.
/// </summary>
public record ActivityDto(
    string Id,
    string UserId,
    string UserDisplayName,
    string? UserAvatarUrl,
    string ActivityType,
    string Title,
    string Description,
    DateTime OccurredAt,
    Dictionary<string, object>? Metadata
);

/// <summary>
/// DTO for activity feed response.
/// </summary>
public record ActivityFeedDto(
    List<ActivityDto> Activities,
    int TotalCount,
    string? ContinuationToken
);

/// <summary>
/// DTO for creating an activity (internal use).
/// </summary>
public record CreateActivityRequest(
    string UserId,
    string ActivityType,
    string Title,
    string Description,
    Dictionary<string, object>? Metadata
);
