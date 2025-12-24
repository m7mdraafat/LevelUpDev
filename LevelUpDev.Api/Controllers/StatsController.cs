using Microsoft.AspNetCore.Mvc;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Application.DTOs.Stats;
using LevelUpDev.Application.Interfaces;

namespace LevelUpDev.Api.Controllers;

/// <summary>
/// User statistics and analytics endpoints.
/// </summary>
public class StatsController : BaseController
{
    private readonly IUserStatsService _statsService;
    private readonly ILogger<StatsController> _logger;

    public StatsController(IUserStatsService statsService, ILogger<StatsController> logger)
    {
        _statsService = statsService;
        _logger = logger;
    }

    /// <summary>
    /// Get stats for a specific user.
    /// </summary>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(ApiResponse<UserStatsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<UserStatsDto>>> GetByUserId(
        string userId,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetByUserIdAsync in IUserStatsService
        // var result = await _statsService.GetByUserIdAsync(userId, cancellationToken);
        // if (result.IsFailure)
        //     throw new NotFoundException(result.Error.Description);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetByUserId endpoint");
    }

    /// <summary>
    /// Get current user's stats.
    /// </summary>
    [HttpGet("me")]
    [ProducesResponseType(typeof(ApiResponse<UserStatsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<UserStatsDto>>> GetCurrentUserStats(
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetCurrentUserStats endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // var result = await _statsService.GetByUserIdAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetCurrentUserStats endpoint");
    }

    /// <summary>
    /// Get streak information for a user.
    /// </summary>
    [HttpGet("user/{userId}/streak")]
    [ProducesResponseType(typeof(ApiResponse<StreakInfoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<StreakInfoDto>>> GetStreakInfo(
        string userId,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetStreakInfoAsync in IUserStatsService
        // var result = await _statsService.GetStreakInfoAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetStreakInfo endpoint");
    }

    /// <summary>
    /// Get weekly progress for a user.
    /// </summary>
    [HttpGet("user/{userId}/weekly")]
    [ProducesResponseType(typeof(ApiResponse<WeeklyProgressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<WeeklyProgressDto>>> GetWeeklyProgress(
        string userId,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetWeeklyProgressAsync in IUserStatsService
        // var result = await _statsService.GetWeeklyProgressAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetWeeklyProgress endpoint");
    }

    /// <summary>
    /// Get activity heatmap data for a user.
    /// </summary>
    [HttpGet("user/{userId}/heatmap")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<DailyActivityDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<DailyActivityDto>>>> GetActivityHeatmap(
        string userId,
        [FromQuery] int days = 365,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetActivityHeatmapAsync in IUserStatsService
        // var result = await _statsService.GetActivityHeatmapAsync(userId, days, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetActivityHeatmap endpoint");
    }

    /// <summary>
    /// Manually trigger LeetCode sync for a user.
    /// </summary>
    [HttpPost("user/{userId}/sync")]
    [ProducesResponseType(typeof(ApiResponse<UserStatsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<UserStatsDto>>> SyncStats(
        string userId,
        CancellationToken cancellationToken)
    {
        // TODO: Implement SyncAsync in IUserStatsService
        // var result = await _statsService.SyncAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement SyncStats endpoint");
    }
}
