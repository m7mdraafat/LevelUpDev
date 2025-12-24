using Microsoft.AspNetCore.Mvc;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Application.DTOs.Quests;
using LevelUpDev.Application.Interfaces;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Api.Controllers;

/// <summary>
/// Quest and progression endpoints.
/// </summary>
public class QuestsController : BaseController
{
    private readonly IQuestService _questService;
    private readonly ILogger<QuestsController> _logger;

    public QuestsController(IQuestService questService, ILogger<QuestsController> logger)
    {
        _questService = questService;
        _logger = logger;
    }

    /// <summary>
    /// Get all quests for a user.
    /// </summary>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<QuestProgressDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<QuestProgressDto>>>> GetUserQuests(
        string userId,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetUserQuestsAsync in IQuestService
        // var result = await _questService.GetUserQuestsAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetUserQuests endpoint");
    }

    /// <summary>
    /// Get current user's quests.
    /// </summary>
    [HttpGet("me")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<QuestProgressDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<QuestProgressDto>>>> GetMyQuests(
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetMyQuests endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // var result = await _questService.GetUserQuestsAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetMyQuests endpoint");
    }

    /// <summary>
    /// Get specific quest progress.
    /// </summary>
    [HttpGet("{type}/user/{userId}")]
    [ProducesResponseType(typeof(ApiResponse<QuestProgressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<QuestProgressDto>>> GetQuestProgress(
        QuestType type,
        string userId,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetQuestProgressAsync in IQuestService
        // var result = await _questService.GetQuestProgressAsync(type, userId, cancellationToken);
        // if (result.IsFailure)
        //     throw new NotFoundException(result.Error.Description);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetQuestProgress endpoint");
    }

    /// <summary>
    /// Get DSA quest details with theme zones.
    /// </summary>
    [HttpGet("dsa/zones")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ThemeZoneDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ThemeZoneDto>>>> GetDsaThemeZones(
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetDsaThemeZonesAsync in IQuestService
        // var result = await _questService.GetDsaThemeZonesAsync(cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetDsaThemeZones endpoint");
    }

    /// <summary>
    /// Get world map data for a user.
    /// </summary>
    [HttpGet("world-map/user/{userId}")]
    [ProducesResponseType(typeof(ApiResponse<WorldMapDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<WorldMapDto>>> GetWorldMap(
        string userId,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetWorldMapAsync in IQuestService
        // var result = await _questService.GetWorldMapAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetWorldMap endpoint");
    }

    /// <summary>
    /// Complete a quest level.
    /// </summary>
    [HttpPost("{type}/complete-level")]
    [ProducesResponseType(typeof(ApiResponse<QuestProgressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<QuestProgressDto>>> CompleteLevel(
        QuestType type,
        [FromBody] CompleteLevelRequest request,
        CancellationToken cancellationToken)
    {
        // TODO: Implement CompleteLevelAsync in IQuestService
        // var userId = GetCurrentUserId();
        // var result = await _questService.CompleteLevelAsync(type, userId, request, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement CompleteLevel endpoint");
    }
}

/// <summary>
/// Request to complete a level.
/// </summary>
public record CompleteLevelRequest(int Level);
