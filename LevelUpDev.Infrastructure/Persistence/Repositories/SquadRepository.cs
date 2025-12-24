using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using LevelUpDev.Domain.Common;
using LevelUpDev.Domain.Entities;
using LevelUpDev.Domain.Interfaces;

namespace LevelUpDev.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for Squad entity.
/// </summary>
public class SquadRepository : CosmosRepositoryBase<Squad>, ISquadRepository
{
    public SquadRepository(Container container, ILogger<SquadRepository> logger)
        : base(container, logger)
    {
    }

    public async Task<QueryResult<IReadOnlyList<Squad>>> GetRecruitingSquadsAsync(
        int limit = 20,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c WHERE c.isRecruiting = true ORDER BY c.totalPoints DESC",
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<Squad>>> GetTopByPointsAsync(
        int limit = 20,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c ORDER BY c.totalPoints DESC",
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<Squad>>> GetTopByWeeklyPointsAsync(
        int limit = 20,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c ORDER BY c.weeklyPoints DESC",
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<Squad>>> SearchByNameAsync(
        string searchTerm,
        int limit = 10,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return QueryResult<IReadOnlyList<Squad>>.Failure(
                Error.Validation("SearchTerm", "Search term cannot be empty"));
        }

        var normalizedSearch = searchTerm.ToLowerInvariant();

        return await QueryAsync(
            $"SELECT TOP {limit} * FROM c WHERE CONTAINS(LOWER(c.name), @search)",
            parameters: new Dictionary<string, object> { { "search", normalizedSearch } },
            cancellationToken: cancellationToken);
    }

    public async Task<QueryResult<IReadOnlyList<Squad>>> GetByTagAsync(
        string tag,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(tag))
        {
            return QueryResult<IReadOnlyList<Squad>>.Failure(
                Error.Validation("Tag", "Tag cannot be empty"));
        }

        return await QueryAsync(
            "SELECT * FROM c WHERE ARRAY_CONTAINS(c.tags, @tag)",
            parameters: new Dictionary<string, object> { { "tag", tag } },
            cancellationToken: cancellationToken);
    }
}
