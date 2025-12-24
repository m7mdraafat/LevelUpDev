using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;
using LevelUpDev.Domain.Enums;
using LevelUpDev.Domain.Interfaces;

namespace LevelUpDev.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for Achievement entity.
/// </summary>
public class AchievementRepository : CosmosRepositoryBase<Achievement>, IAchievementRepository
{
    public AchievementRepository(Container container, ILogger<AchievementRepository> logger)
        : base(container, logger)
    {
    }

    public async Task<QueryResult<IReadOnlyList<Achievement>>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<IReadOnlyList<Achievement>>.Failure(
                Error.Validation("UserId", "User ID cannot be empty"));
        }

        return await GetAllByPartitionKeyAsync(userId, cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<Achievement>>> GetUnlockedByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<IReadOnlyList<Achievement>>.Failure(
                Error.Validation("UserId", "User ID cannot be empty"));
        }

        return await QueryAsync(
            "SELECT * FROM c WHERE c.userId = @userId AND c.isUnlocked = true ORDER BY c.unlockedAt DESC",
            partitionKey: userId,
            parameters: new Dictionary<string, object> { { "userId", userId } },
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<Achievement>> GetByUserAndTypeAsync(
        string userId,
        AchievementType achievementType,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return QueryResult<Achievement>.Failure(
                Error.Validation("UserId", "User ID cannot be empty"));
        }

        var result = await QueryAsync(
            "SELECT * FROM c WHERE c.userId = @userId AND c.achievementType = @type",
            partitionKey: userId,
            parameters: new Dictionary<string, object>
            {
                { "userId", userId },
                { "type", (int)achievementType }
            },
            cancellationToken: cancellationToken);

        if (result.IsFailure)
        {
            return QueryResult<Achievement>.Failure(result.Error);
        }

        var achievement = result.Value.FirstOrDefault();
        if (achievement is null)
        {
            return QueryResult<Achievement>.Failure(
                Error.NotFound("Achievement", $"{userId}:{achievementType}"));
        }

        return QueryResult<Achievement>.Success(achievement, result.RequestCharge);
    }

    public async Task<QueryResult<IReadOnlyList<Achievement>>> GetByAchievementTypeAsync(
        AchievementType achievementType,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            "SELECT * FROM c WHERE c.achievementType = @type AND c.isUnlocked = true",
            parameters: new Dictionary<string, object> { { "type", (int)achievementType } },
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<Achievement>>> GetByRarityAsync(
        BadgeRarity rarity,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            "SELECT * FROM c WHERE c.rarity = @rarity AND c.isUnlocked = true",
            parameters: new Dictionary<string, object> { { "rarity", (int)rarity } },
            cancellationToken: cancellationToken);
    }
}
