using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;
using LevelUpDev.Domain.Enums;
using LevelUpDev.Domain.Interfaces;

namespace LevelUpDev.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for Notification entity.
/// </summary>
public class NotificationRepository : CosmosRepositoryBase<Notification>, INotificationRepository
{
    public NotificationRepository(Container container, ILogger<NotificationRepository> logger)
        : base(container, logger)
    {
    }

    public async Task<QueryResult<IReadOnlyList<Notification>>> GetByUserIdAsync(
        string userId,
        int limit = 50,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<IReadOnlyList<Notification>>.Failure(
                Error.Validation("UserId", "User ID cannot be empty"));
        }

        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c WHERE c.userId = @userId ORDER BY c.createdAt DESC",
            partitionKey: userId,
            parameters: new Dictionary<string, object> { { "userId", userId } },
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<Notification>>> GetUnreadByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<IReadOnlyList<Notification>>.Failure(
                Error.Validation("UserId", "User ID cannot be empty"));
        }

        return await QueryAsync(
            "SELECT * FROM c WHERE c.userId = @userId AND c.isRead = false ORDER BY c.createdAt DESC",
            partitionKey: userId,
            parameters: new Dictionary<string, object> { { "userId", userId } },
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<int>> GetUnreadCountAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<int>.Failure(
                Error.Validation("UserId", "User ID cannot be empty"));
        }

        var result = await GetUnreadByUserIdAsync(userId, cancellationToken);
        
        if (result.IsFailure)
        {
            return QueryResult<int>.Failure(result.Error);
        }

        return QueryResult<int>.Success(result.Value.Count, result.RequestCharge);
    }

    public async Task<Result> MarkAllAsReadAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Result.Failure(Error.Validation("UserId", "User ID cannot be empty"));
        }

        var unreadResult = await GetUnreadByUserIdAsync(userId, cancellationToken);
        
        if (unreadResult.IsFailure)
        {
            return Result.Failure(unreadResult.Error);
        }

        foreach (var notification in unreadResult.Value)
        {
            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;
            
            var updateResult = await UpdateAsync(notification, cancellationToken);
            if (updateResult.IsFailure)
            {
                _logger.LogWarning(
                    "Failed to mark notification {NotificationId} as read: {Error}",
                    notification.Id, updateResult.Error.Description);
            }
        }

        return Result.Success();
    }

    public async Task<QueryResult<IReadOnlyList<Notification>>> GetByTypeAsync(
        string userId,
        NotificationType type,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<IReadOnlyList<Notification>>.Failure(
                Error.Validation("UserId", "User ID cannot be empty"));
        }

        return await QueryAsync(
            "SELECT * FROM c WHERE c.userId = @userId AND c.type = @type ORDER BY c.createdAt DESC",
            partitionKey: userId,
            parameters: new Dictionary<string, object>
            {
                { "userId", userId },
                { "type", (int)type }
            },
            cancellationToken: cancellationToken);
    }

    public async Task<Result> DeleteExpiredAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var now = DateTime.UtcNow;
            
            var expiredResult = await QueryAsync(
                "SELECT * FROM c WHERE c.expiresAt != null AND c.expiresAt < @now",
                parameters: new Dictionary<string, object> { { "now", now } },
                cancellationToken: cancellationToken);

            if (expiredResult.IsFailure)
            {
                return Result.Failure(expiredResult.Error);
            }

            foreach (var notification in expiredResult.Value)
            {
                var deleteResult = await DeleteAsync(
                    notification.Id,
                    notification.PartitionKeyValue,
                    cancellationToken);

                if (deleteResult.IsFailure)
                {
                    _logger.LogWarning(
                        "Failed to delete expired notification {NotificationId}: {Error}",
                        notification.Id, deleteResult.Error.Description);
                }
            }

            _logger.LogInformation("Deleted {Count} expired notifications", expiredResult.Value.Count);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete expired notifications");
            return Result.Failure(Error.Database("Failed to delete expired notifications"));
        }
    }
}
