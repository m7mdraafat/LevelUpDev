using Microsoft.AspNetCore.Mvc;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Application.DTOs.Leaderboards;
using LevelUpDev.Application.DTOs.Squads;
using LevelUpDev.Application.Interfaces;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Api.Controllers;

/// <summary>
/// Leaderboard endpoints for various ranking types.
/// </summary>
public class LeaderboardsController : BaseController
{
    private readonly ILeaderboardService _leaderboardService;
    private readonly ILogger<LeaderboardsController> _logger;

    public LeaderboardsController(ILeaderboardService leaderboardService, ILogger<LeaderboardsController> logger)
    {
        _leaderboardService = leaderboardService;
        _logger = logger;
    }

    /// <summary>
    /// Get leaderboard by type.
    /// </summary>
    [HttpGet("{type}")]
    [ProducesResponseType(typeof(ApiResponse<LeaderboardDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<LeaderboardDto>>> GetByType(
        LeaderboardType type,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetByTypeAsync in ILeaderboardService
        // var result = await _leaderboardService.GetByTypeAsync(type, page, pageSize, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetByType endpoint");
    }

    /// <summary>
    /// Get weekly leaderboard.
    /// </summary>
    [HttpGet("weekly")]
    [ProducesResponseType(typeof(ApiResponse<LeaderboardDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<LeaderboardDto>>> GetWeekly(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetWeeklyAsync in ILeaderboardService
        // var result = await _leaderboardService.GetByTypeAsync(LeaderboardType.WeeklySolved, page, pageSize, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetWeekly endpoint");
    }

    /// <summary>
    /// Get all-time leaderboard.
    /// </summary>
    [HttpGet("alltime")]
    [ProducesResponseType(typeof(ApiResponse<LeaderboardDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<LeaderboardDto>>> GetAllTime(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetAllTimeAsync in ILeaderboardService
        // var result = await _leaderboardService.GetByTypeAsync(LeaderboardType.TotalSolved, page, pageSize, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetAllTime endpoint");
    }

    /// <summary>
    /// Get streak leaderboard.
    /// </summary>
    [HttpGet("streaks")]
    [ProducesResponseType(typeof(ApiResponse<LeaderboardDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<LeaderboardDto>>> GetStreaks(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetStreaksAsync in ILeaderboardService
        // var result = await _leaderboardService.GetByTypeAsync(LeaderboardType.CurrentStreak, page, pageSize, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetStreaks endpoint");
    }

    /// <summary>
    /// Get current user's position in a leaderboard.
    /// </summary>
    [HttpGet("{type}/me")]
    [ProducesResponseType(typeof(ApiResponse<LeaderboardEntryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<LeaderboardEntryDto>>> GetMyPosition(
        LeaderboardType type,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetUserPositionAsync in ILeaderboardService
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // var result = await _leaderboardService.GetUserPositionAsync(type, userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetMyPosition endpoint");
    }

    /// <summary>
    /// Get leaderboard entries around a specific rank.
    /// </summary>
    [HttpGet("{type}/around/{rank}")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<LeaderboardEntryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<LeaderboardEntryDto>>>> GetAroundRank(
        LeaderboardType type,
        int rank,
        [FromQuery] int range = 5,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetAroundRankAsync in ILeaderboardService
        // var result = await _leaderboardService.GetAroundRankAsync(type, rank, range, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetAroundRank endpoint");
    }

    /// <summary>
    /// Get squad leaderboard.
    /// </summary>
    [HttpGet("squads")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<SquadLeaderboardEntryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<SquadLeaderboardEntryDto>>>> GetSquadLeaderboard(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetSquadLeaderboardAsync in ILeaderboardService
        // var result = await _leaderboardService.GetSquadLeaderboardAsync(page, pageSize, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetSquadLeaderboard endpoint");
    }
}
