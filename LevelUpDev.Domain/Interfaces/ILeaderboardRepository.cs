using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;
using LevelUpDev.Domain.Enums;

namespace LevelUpDev.Domain.Interfaces;

/// <summary>
/// Repository interface for LeaderboardEntry entity operations.
/// </summary>
public interface ILeaderboardRepository : ICosmosRepository<LeaderboardEntry>
{
    /// <summary>
    /// Gets leaderboard entries by type.
    /// </summary>
    Task<QueryResult<IReadOnlyList<LeaderboardEntry>>> GetByTypeAsync(
        LeaderboardType type,
        int limit = 100,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user's entry in a specific leaderboard.
    /// </summary>
    Task<QueryResult<LeaderboardEntry>> GetUserEntryAsync(
        LeaderboardType type,
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all leaderboard entries for a user.
    /// </summary>
    Task<QueryResult<IReadOnlyList<LeaderboardEntry>>> GetUserEntriesAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Batch upsert leaderboard entries (for recalculation).
    /// </summary>
    Task<Result> BatchUpsertAsync(
        IEnumerable<LeaderboardEntry> entries,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets entries around a user's rank (for context).
    /// </summary>
    Task<QueryResult<IReadOnlyList<LeaderboardEntry>>> GetEntriesAroundRankAsync(
        LeaderboardType type,
        int rank,
        int range = 5,
        CancellationToken cancellationToken = default);
}
