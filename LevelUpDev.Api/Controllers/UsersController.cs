using Microsoft.AspNetCore.Mvc;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Application.DTOs.Users;
using LevelUpDev.Application.Interfaces;

namespace LevelUpDev.Api.Controllers;

/// <summary>
/// User management endpoints.
/// </summary>
public class UsersController : BaseController
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    /// Get user by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetById(
        string id,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetByIdAsync in IUserService
        // var result = await _userService.GetByIdAsync(id, cancellationToken);
        // if (result.IsFailure)
        //     throw new NotFoundException(result.Error.Description);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetById endpoint");
    }

    /// <summary>
    /// Get user profile by LeetCode username.
    /// </summary>
    [HttpGet("leetcode/{username}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetByLeetCodeUsername(
        string username,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetByLeetCodeUsernameAsync in IUserService
        // var result = await _userService.GetByLeetCodeUsernameAsync(username, cancellationToken);
        // if (result.IsFailure)
        //     throw new NotFoundException(result.Error.Description);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetByLeetCodeUsername endpoint");
    }

    /// <summary>
    /// Get current user profile.
    /// </summary>
    [HttpGet("me")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetCurrentUser(
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetCurrentUser endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // var result = await _userService.GetByIdAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetCurrentUser endpoint");
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<UserDto>>> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        // TODO: Implement RegisterAsync in IUserService
        // var result = await _userService.RegisterAsync(request, cancellationToken);
        // if (result.IsFailure)
        //     HandleFailure(result.Error);
        // return Created(result.Value, $"/api/users/{result.Value.Id}");
        
        throw new NotImplementedException("TODO: Implement Register endpoint");
    }

    /// <summary>
    /// Update user profile.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<UserDto>>> Update(
        string id,
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        // TODO: Implement UpdateAsync in IUserService
        // var result = await _userService.UpdateAsync(id, request, cancellationToken);
        // if (result.IsFailure)
        //     HandleFailure(result.Error);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement Update endpoint");
    }

    /// <summary>
    /// Search users by display name.
    /// </summary>
    [HttpGet("search")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<UserDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<UserDto>>>> Search(
        [FromQuery] string term,
        [FromQuery] int limit = 10,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement SearchAsync in IUserService
        // var result = await _userService.SearchAsync(term, limit, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement Search endpoint");
    }
}
