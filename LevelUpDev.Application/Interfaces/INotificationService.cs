using LevelUpDev.Application.DTOs.Notifications;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Application.Interfaces;

/// <summary>
/// Service interface for notification operations.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Gets notifications for a user.
    /// </summary>
    Task<Result<NotificationListDto>> GetByUserIdAsync(
        string userId,
        int limit = 50,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets unread notification count for a user.
    /// </summary>
    Task<Result<int>> GetUnreadCountAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks notifications as read.
    /// </summary>
    Task<Result> MarkAsReadAsync(
        string userId,
        MarkNotificationsReadRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a notification to a user.
    /// </summary>
    Task<Result> SendAsync(
        CreateNotificationRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a notification to multiple users.
    /// </summary>
    Task<Result> SendBulkAsync(
        List<string> userIds,
        NotificationType type,
        string title,
        string message,
        string? actionUrl = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a notification.
    /// </summary>
    Task<Result> DeleteAsync(
        string userId,
        string notificationId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cleans up expired notifications (background job).
    /// </summary>
    Task<Result> CleanupExpiredAsync(
        CancellationToken cancellationToken = default);
}
