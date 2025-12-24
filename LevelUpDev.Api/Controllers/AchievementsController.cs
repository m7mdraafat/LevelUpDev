using Microsoft.AspNetCore.Mvc;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Application.DTOs.Achievements;
using LevelUpDev.Application.Interfaces;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Api.Controllers;

/// <summary>
/// Achievement and badge endpoints.
/// </summary>
public class AchievementsController : BaseController
{
    private readonly IAchievementService _achievementService;
    private readonly ILogger<AchievementsController> _logger;

    public AchievementsController(IAchievementService achievementService, ILogger<AchievementsController> logger)
    {
        _achievementService = achievementService;
        _logger = logger;
    }

    /// <summary>
    /// Get all achievements for a user.
    /// </summary>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<AchievementDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<AchievementDto>>>> GetUserAchievements(
        string userId,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetUserAchievementsAsync in IAchievementService
        // var result = await _achievementService.GetUserAchievementsAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetUserAchievements endpoint");
    }

    /// <summary>
    /// Get current user's achievements.
    /// </summary>
    [HttpGet("me")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<AchievementDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<AchievementDto>>>> GetMyAchievements(
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetMyAchievements endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // var result = await _achievementService.GetUserAchievementsAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetMyAchievements endpoint");
    }

    /// <summary>
    /// Get unlocked achievements for a user.
    /// </summary>
    [HttpGet("user/{userId}/unlocked")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<AchievementDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<AchievementDto>>>> GetUnlockedAchievements(
        string userId,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetUnlockedAchievementsAsync in IAchievementService
        // var result = await _achievementService.GetUnlockedAchievementsAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetUnlockedAchievements endpoint");
    }

    /// <summary>
    /// Get achievement summary for a user.
    /// </summary>
    [HttpGet("user/{userId}/summary")]
    [ProducesResponseType(typeof(ApiResponse<AchievementSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<AchievementSummaryDto>>> GetAchievementSummary(
        string userId,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetAchievementSummaryAsync in IAchievementService
        // var result = await _achievementService.GetAchievementSummaryAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetAchievementSummary endpoint");
    }

    /// <summary>
    /// Get all available achievement types.
    /// </summary>
    [HttpGet("types")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<AchievementTypeDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<AchievementTypeDto>>>> GetAchievementTypes(
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetAchievementTypesAsync in IAchievementService
        // var result = await _achievementService.GetAchievementTypesAsync(cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetAchievementTypes endpoint");
    }

    /// <summary>
    /// Get achievements by rarity.
    /// </summary>
    [HttpGet("rarity/{rarity}")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<AchievementDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<AchievementDto>>>> GetByRarity(
        BadgeRarity rarity,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetByRarityAsync in IAchievementService
        // var result = await _achievementService.GetByRarityAsync(rarity, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetByRarity endpoint");
    }
}

/// <summary>
/// Achievement summary DTO.
/// </summary>
public record AchievementSummaryDto(
    int TotalAchievements,
    int UnlockedCount,
    int LockedCount,
    double CompletionPercentage,
    IReadOnlyDictionary<BadgeRarity, int> ByRarity,
    AchievementDto? LatestUnlocked);

/// <summary>
/// Achievement type definition DTO.
/// </summary>
public record AchievementTypeDto(
    AchievementType Type,
    string Name,
    string Description,
    BadgeRarity Rarity,
    string Icon,
    int RequiredValue);
