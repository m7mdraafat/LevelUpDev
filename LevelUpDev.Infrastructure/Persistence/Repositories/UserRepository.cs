using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Interfaces;
using UserEntity = LevelUpDev.Domain.Entities.User;

namespace LevelUpDev.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for User entity.
/// </summary>
public class UserRepository : CosmosRepositoryBase<UserEntity>, IUserRepository
{
    public UserRepository(Container container, ILogger<UserRepository> logger)
        : base(container, logger)
    {
    }

    public async Task<QueryResult<UserEntity>> GetByGitHubIdAsync(
        string gitHubId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(gitHubId))
        {
            return QueryResult<UserEntity>.Failure(Error.Validation("GitHubId", "GitHub ID cannot be empty"));
        }

        var result = await QueryAsync(
            "SELECT * FROM c WHERE c.gitHubId = @gitHubId",
            parameters: new Dictionary<string, object> { { "gitHubId", gitHubId } },
            cancellationToken: cancellationToken);

        if (result.IsFailure)
        {
            return QueryResult<UserEntity>.Failure(result.Error);
        }

        var user = result.Value.FirstOrDefault();
        if (user is null)
        {
            return QueryResult<UserEntity>.Failure(Error.NotFound("User", $"GitHub:{gitHubId}"));
        }

        return QueryResult<UserEntity>.Success(user, result.RequestCharge);
    }

    public async Task<QueryResult<UserEntity>> GetByLeetCodeUsernameAsync(
        string leetCodeUsername,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(leetCodeUsername))
        {
            return QueryResult<UserEntity>.Failure(Error.Validation("LeetCodeUsername", "LeetCode username cannot be empty"));
        }

        var result = await QueryAsync(
            "SELECT * FROM c WHERE c.leetCodeUsername = @username",
            parameters: new Dictionary<string, object> { { "username", leetCodeUsername } },
            cancellationToken: cancellationToken);

        if (result.IsFailure)
        {
            return QueryResult<UserEntity>.Failure(result.Error);
        }

        var user = result.Value.FirstOrDefault();
        if (user is null)
        {
            return QueryResult<UserEntity>.Failure(Error.NotFound("User", $"LeetCode:{leetCodeUsername}"));
        }

        return QueryResult<UserEntity>.Success(user, result.RequestCharge);
    }

    public async Task<QueryResult<IReadOnlyList<UserEntity>>> GetActiveUsersAsync(
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            "SELECT * FROM c WHERE c.isActive = true ORDER BY c.lastActiveAt DESC",
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<UserEntity>>> GetBySquadIdAsync(
        string squadId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(squadId))
        {
            return QueryResult<IReadOnlyList<UserEntity>>.Failure(
                Error.Validation("SquadId", "Squad ID cannot be empty"));
        }

        return await QueryAsync(
            "SELECT * FROM c WHERE c.squadId = @squadId",
            parameters: new Dictionary<string, object> { { "squadId", squadId } },
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<UserEntity>>> SearchByDisplayNameAsync(
        string searchTerm,
        int limit = 10,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return QueryResult<IReadOnlyList<UserEntity>>.Failure(
                Error.Validation("SearchTerm", "Search term cannot be empty"));
        }

        var normalizedSearch = searchTerm.ToLowerInvariant();

        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c WHERE CONTAINS(LOWER(c.displayName), @search)",
            parameters: new Dictionary<string, object> { { "search", normalizedSearch } },
            cancellationToken: cancellationToken);
    }
}
