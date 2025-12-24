using Microsoft.AspNetCore.Mvc;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Application.DTOs.Challenges;
using LevelUpDev.Application.Interfaces;

namespace LevelUpDev.Api.Controllers;

/// <summary>
/// Daily challenges and community goals endpoints.
/// </summary>
public class ChallengesController : BaseController
{
    private readonly IChallengeService _challengeService;
    private readonly ILogger<ChallengesController> _logger;

    public ChallengesController(IChallengeService challengeService, ILogger<ChallengesController> logger)
    {
        _challengeService = challengeService;
        _logger = logger;
    }

    /// <summary>
    /// Get today's daily challenge.
    /// </summary>
    [HttpGet("daily")]
    [ProducesResponseType(typeof(ApiResponse<DailyChallengeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<DailyChallengeDto>>> GetTodaysChallenge(
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetTodaysChallengeAsync in IChallengeService
        // var result = await _challengeService.GetTodaysChallengeAsync(cancellationToken);
        // if (result.IsFailure)
        //     throw new NotFoundException(result.Error.Description);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetTodaysChallenge endpoint");
    }

    /// <summary>
    /// Get daily challenge history.
    /// </summary>
    [HttpGet("daily/history")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<DailyChallengeDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<DailyChallengeDto>>>> GetChallengeHistory(
        [FromQuery] int days = 30,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetChallengeHistoryAsync in IChallengeService
        // var endDate = DateOnly.FromDateTime(DateTime.UtcNow);
        // var startDate = endDate.AddDays(-days);
        // var result = await _challengeService.GetByDateRangeAsync(startDate, endDate, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetChallengeHistory endpoint");
    }

    /// <summary>
    /// Mark a daily challenge as completed.
    /// </summary>
    [HttpPost("daily/complete")]
    [ProducesResponseType(typeof(ApiResponse<DailyChallengeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<DailyChallengeDto>>> MarkCompleted(
        [FromBody] CompleteChallengeRequest request,
        CancellationToken cancellationToken)
    {
        // TODO: Implement MarkCompletedAsync in IChallengeService
        // var userId = GetCurrentUserId();
        // var result = await _challengeService.MarkCompletedAsync(request.ChallengeId, userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement MarkCompleted endpoint");
    }

    /// <summary>
    /// Get current community goal.
    /// </summary>
    [HttpGet("community")]
    [ProducesResponseType(typeof(ApiResponse<CommunityGoalDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CommunityGoalDto>>> GetCurrentCommunityGoal(
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetCurrentCommunityGoalAsync in IChallengeService
        // var result = await _challengeService.GetCurrentCommunityGoalAsync(cancellationToken);
        // if (result.IsFailure)
        //     throw new NotFoundException(result.Error.Description);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetCurrentCommunityGoal endpoint");
    }

    /// <summary>
    /// Get community goal history.
    /// </summary>
    [HttpGet("community/history")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CommunityGoalDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<CommunityGoalDto>>>> GetCommunityGoalHistory(
        [FromQuery] int weeks = 12,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetCommunityGoalHistoryAsync in IChallengeService
        // var result = await _challengeService.GetCommunityGoalHistoryAsync(weeks, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetCommunityGoalHistory endpoint");
    }

    /// <summary>
    /// Get daily challenge leaderboard.
    /// </summary>
    [HttpGet("daily/leaderboard")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ChallengeLeaderboardEntryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ChallengeLeaderboardEntryDto>>>> GetDailyLeaderboard(
        [FromQuery] int limit = 50,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetDailyLeaderboardAsync in IChallengeService
        // var result = await _challengeService.GetDailyLeaderboardAsync(limit, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetDailyLeaderboard endpoint");
    }
}
