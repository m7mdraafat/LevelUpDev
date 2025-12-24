using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Domain.Interfaces;

/// <summary>
/// Repository interface for Notification entity operations.
/// </summary>
public interface INotificationRepository : ICosmosRepository<Notification>
{
    /// <summary>
    /// Gets all notifications for a user.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Notification>>> GetByUserIdAsync(
        string userId,
        int limit = 50,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets unread notifications for a user.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Notification>>> GetUnreadByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets notification count for a user.
    /// </summary>
    Task<QueryResult<int>> GetUnreadCountAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks all notifications as read for a user.
    /// </summary>
    Task<Result> MarkAllAsReadAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets notifications by type for a user.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Notification>>> GetByTypeAsync(
        string userId,
        NotificationType type,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes expired notifications.
    /// </summary>
    Task<Result> DeleteExpiredAsync(
        CancellationToken cancellationToken = default);
}
