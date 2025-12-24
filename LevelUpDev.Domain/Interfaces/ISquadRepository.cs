using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;

namespace LevelUpDev.Domain.Interfaces;

/// <summary>
/// Repository interface for Squad entity operations.
/// </summary>
public interface ISquadRepository : ICosmosRepository<Squad>
{
    /// <summary>
    /// Gets squads that are recruiting.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Squad>>> GetRecruitingSquadsAsync(
        int limit = 20,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets squads ordered by total points (for leaderboard).
    /// </summary>
    Task<QueryResult<IReadOnlyList<Squad>>> GetTopByPointsAsync(
        int limit = 20,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets squads ordered by weekly points (for weekly leaderboard).
    /// </summary>
    Task<QueryResult<IReadOnlyList<Squad>>> GetTopByWeeklyPointsAsync(
        int limit = 20,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches squads by name.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Squad>>> SearchByNameAsync(
        string searchTerm,
        int limit = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets squads by tag.
    /// </summary>
    Task<QueryResult<IReadOnlyList<Squad>>> GetByTagAsync(
        string tag,
        CancellationToken cancellationToken = default);
}
