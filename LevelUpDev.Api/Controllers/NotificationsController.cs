using Microsoft.AspNetCore.Mvc;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Application.DTOs.Notifications;
using LevelUpDev.Application.Interfaces;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Api.Controllers;

/// <summary>
/// Notification management endpoints.
/// </summary>
public class NotificationsController : BaseController
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<NotificationsController> _logger;

    public NotificationsController(INotificationService notificationService, ILogger<NotificationsController> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    /// <summary>
    /// Get notifications for the current user.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<NotificationDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<NotificationDto>>>> GetMyNotifications(
        [FromQuery] int limit = 50,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement GetMyNotifications endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // var result = await _notificationService.GetByUserIdAsync(userId, limit, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetMyNotifications endpoint");
    }

    /// <summary>
    /// Get unread notifications for the current user.
    /// </summary>
    [HttpGet("unread")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<NotificationDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<NotificationDto>>>> GetUnreadNotifications(
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetUnreadNotifications endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // var result = await _notificationService.GetUnreadByUserIdAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetUnreadNotifications endpoint");
    }

    /// <summary>
    /// Get unread notification count.
    /// </summary>
    [HttpGet("unread/count")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<int>>> GetUnreadCount(
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetUnreadCount endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // var result = await _notificationService.GetUnreadCountAsync(userId, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetUnreadCount endpoint");
    }

    /// <summary>
    /// Mark a notification as read.
    /// </summary>
    [HttpPost("{id}/read")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> MarkAsRead(
        string id,
        CancellationToken cancellationToken)
    {
        // TODO: Implement MarkAsRead endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // var result = await _notificationService.MarkAsReadAsync(id, userId, cancellationToken);
        // if (result.IsFailure)
        //     throw new NotFoundException(result.Error.Description);
        // return NoContent();
        
        throw new NotImplementedException("TODO: Implement MarkAsRead endpoint");
    }

    /// <summary>
    /// Mark all notifications as read.
    /// </summary>
    [HttpPost("read-all")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> MarkAllAsRead(
        CancellationToken cancellationToken)
    {
        // TODO: Implement MarkAllAsRead endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // await _notificationService.MarkAllAsReadAsync(userId, cancellationToken);
        // return NoContent();
        
        throw new NotImplementedException("TODO: Implement MarkAllAsRead endpoint");
    }

    /// <summary>
    /// Delete a notification.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(
        string id,
        CancellationToken cancellationToken)
    {
        // TODO: Implement Delete endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // var result = await _notificationService.DeleteAsync(id, userId, cancellationToken);
        // if (result.IsFailure)
        //     throw new NotFoundException(result.Error.Description);
        // return NoContent();
        
        throw new NotImplementedException("TODO: Implement Delete endpoint");
    }

    /// <summary>
    /// Get notifications by type.
    /// </summary>
    [HttpGet("type/{type}")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<NotificationDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<NotificationDto>>>> GetByType(
        NotificationType type,
        CancellationToken cancellationToken)
    {
        // TODO: Implement GetByType endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // var result = await _notificationService.GetByTypeAsync(userId, type, cancellationToken);
        // return Success(result.Value);
        
        throw new NotImplementedException("TODO: Implement GetByType endpoint");
    }

    /// <summary>
    /// Update notification preferences.
    /// </summary>
    [HttpPut("preferences")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UpdatePreferences(
        [FromBody] NotificationPreferencesDto preferences,
        CancellationToken cancellationToken)
    {
        // TODO: Implement UpdatePreferences endpoint
        // var userId = GetCurrentUserId();
        // if (string.IsNullOrEmpty(userId))
        //     throw new UnauthorizedException();
        // await _notificationService.UpdatePreferencesAsync(userId, preferences, cancellationToken);
        // return NoContent();
        
        throw new NotImplementedException("TODO: Implement UpdatePreferences endpoint");
    }
}
