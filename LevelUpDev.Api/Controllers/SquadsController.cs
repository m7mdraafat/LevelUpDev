using Microsoft.AspNetCore.Mvc;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Application.DTOs.Squads;
using LevelUpDev.Application.Interfaces;

namespace LevelUpDev.Api.Controllers;

/// <summary>
/// Squad management endpoints.
/// </summary>
public class SquadsController : BaseController
{
    private readonly ISquadService _squadService;
    private readonly ILogger<SquadsController> _logger;

    public SquadsController(ISquadService squadService, ILogger<SquadsController> logger)
    {
        _squadService = squadService;
        _logger = logger;
    }

    /// <summary>
    /// Get all squads with optional filtering.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<SquadDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<SquadDto>>>> GetAll(
        [FromQuery] bool? recruiting = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetAllAsync in ISquadService
        // var result = await _squadService.GetAllAsync(recruiting, page, pageSize, cancellationToken);
        // return Success(result.Value.Items, result.Value.Page, result.Value.PageSize, result.Value.TotalCount);
        
        throw new NotImplementedException("TODO: Implement GetAll endpoint");
    }

    /// <summary>
    /// Get squad by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<SquadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<SquadDto>>> GetById(
        string id,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetByIdAsync in ISquadService
        // var result = await _squadService.GetByIdAsync(id, cancellationToken);
        // if (result.IsFailure)
        //     throw new NotFoundException(result.Error.Description);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetById endpoint");
    }

    /// <summary>
    /// Create a new squad.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<SquadDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<SquadDto>>> Create(
        [FromBody] CreateSquadRequest request,
        CancellationToken cancellationToken)
    {
        // TODO: Implement CreateAsync in ISquadService
        // var userId = GetCurrentUserId();
        // var result = await _squadService.CreateAsync(userId, request, cancellationToken);
        // if (result.IsFailure)
        //     HandleFailure(result.Error);
        // return Created(result.Value, $"/api/squads/{result.Value.Id}");
        
        throw new NotImplementedException("TODO: Implement Create endpoint");
    }

    /// <summary>
    /// Update squad details.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<SquadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<SquadDto>>> Update(
        string id,
        [FromBody] UpdateSquadRequest request,
        CancellationToken cancellationToken)
    {
        // TODO: Implement UpdateAsync in ISquadService
        // var userId = GetCurrentUserId();
        // var result = await _squadService.UpdateAsync(id, userId, request, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement Update endpoint");
    }

    /// <summary>
    /// Join a squad.
    /// </summary>
    [HttpPost("{id}/join")]
    [ProducesResponseType(typeof(ApiResponse<SquadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<SquadDto>>> Join(
        string id,
        CancellationToken cancellationToken)
    {
        // TODO: Implement JoinAsync in ISquadService
        // var userId = GetCurrentUserId();
        // var result = await _squadService.JoinAsync(id, userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement Join endpoint");
    }

    /// <summary>
    /// Leave a squad.
    /// </summary>
    [HttpPost("{id}/leave")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Leave(
        string id,
        CancellationToken cancellationToken)
    {
        // TODO: Implement LeaveAsync in ISquadService
        // var userId = GetCurrentUserId();
        // var result = await _squadService.LeaveAsync(id, userId, cancellationToken);
        // return NoContent();
        
        throw new NotImplementedException("TODO: Implement Leave endpoint");
    }

    /// <summary>
    /// Get squad members.
    /// </summary>
    [HttpGet("{id}/members")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<SquadMemberDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<SquadMemberDto>>>> GetMembers(
        string id,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetMembersAsync in ISquadService
        // var result = await _squadService.GetMembersAsync(id, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetMembers endpoint");
    }

    /// <summary>
    /// Get top squads by points.
    /// </summary>
    [HttpGet("top")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<SquadDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<SquadDto>>>> GetTopSquads(
        [FromQuery] int limit = 20,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetTopSquadsAsync in ISquadService
        // var result = await _squadService.GetTopSquadsAsync(limit, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetTopSquads endpoint");
    }

    /// <summary>
    /// Search squads by name.
    /// </summary>
    [HttpGet("search")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<SquadDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<SquadDto>>>> Search(
        [FromQuery] string term,
        [FromQuery] int limit = 10,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement SearchAsync in ISquadService
        // var result = await _squadService.SearchAsync(term, limit, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement Search endpoint");
    }
}
