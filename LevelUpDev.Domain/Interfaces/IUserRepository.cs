using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;

namespace LevelUpDev.Domain.Interfaces;

/// <summary>
/// Repository interface for User entity operations.
/// </summary>
public interface IUserRepository : ICosmosRepository<User>
{
    /// <summary>
    /// Gets a user by their GitHub ID.
    /// </summary>
    Task<QueryResult<User>> GetByGitHubIdAsync(
        string gitHubId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user by their LeetCode username.
    /// </summary>
    Task<QueryResult<User>> GetByLeetCodeUsernameAsync(
        string leetCodeUsername,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all active users.
    /// </summary>
    Task<QueryResult<IReadOnlyList<User>>> GetActiveUsersAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets users by squad ID.
    /// </summary>
    Task<QueryResult<IReadOnlyList<User>>> GetBySquadIdAsync(
        string squadId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches users by display name.
    /// </summary>
    Task<QueryResult<IReadOnlyList<User>>> SearchByDisplayNameAsync(
        string searchTerm,
        int limit = 10,
        CancellationToken cancellationToken = default);
}
