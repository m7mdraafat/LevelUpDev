using Microsoft.AspNetCore.Mvc;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Application.DTOs.Activity;
using LevelUpDev.Application.Interfaces;

namespace LevelUpDev.Api.Controllers;

/// <summary>
/// Community activity feed endpoints.
/// </summary>
public class ActivityController : BaseController
{
    private readonly ILogger<ActivityController> _logger;

    public ActivityController(ILogger<ActivityController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get community activity feed.
    /// </summary>
    [HttpGet("feed")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ActivityDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ActivityDto>>>> GetCommunityFeed(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetCommunityFeedAsync in IActivityService
        // var result = await _activityService.GetCommunityFeedAsync(page, pageSize, cancellationToken);
        // return Success(result.Value.Items, page, pageSize, result.Value.TotalCount);
        
        throw new NotImplementedException("TODO: Implement GetCommunityFeed endpoint");
    }

    /// <summary>
    /// Get activity for a specific user.
    /// </summary>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ActivityDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ActivityDto>>>> GetUserActivity(
        string userId,
        [FromQuery] int limit = 50,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetUserActivityAsync in IActivityService
        // var result = await _activityService.GetUserActivityAsync(userId, limit, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetUserActivity endpoint");
    }

    /// <summary>
    /// Get today's activity.
    /// </summary>
    [HttpGet("today")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ActivityDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ActivityDto>>>> GetTodaysActivity(
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetTodaysActivityAsync in IActivityService
        // var result = await _activityService.GetTodaysActivityAsync(cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetTodaysActivity endpoint");
    }

    /// <summary>
    /// Get activity by date range.
    /// </summary>
    [HttpGet("range")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ActivityDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ActivityDto>>>> GetActivityByDateRange(
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetActivityByDateRangeAsync in IActivityService
        // var result = await _activityService.GetByDateRangeAsync(startDate, endDate, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetActivityByDateRange endpoint");
    }
}

/// <summary>
/// Activity DTO for feed display.
/// </summary>
public record ActivityDto(
    string Id,
    string UserId,
    string UserDisplayName,
    string? UserAvatarUrl,
    string ActivityType,
    string Description,
    Dictionary<string, object>? Metadata,
    DateTime CreatedAt);
