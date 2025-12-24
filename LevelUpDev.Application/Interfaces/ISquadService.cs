using LevelUpDev.Application.DTOs.Squads;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Domain.Common;

namespace LevelUpDev.Application.Interfaces;

/// <summary>
/// Service interface for squad operations.
/// </summary>
public interface ISquadService
{
    /// <summary>
    /// Gets a squad by ID.
    /// </summary>
    Task<Result<SquadDto>> GetByIdAsync(
        string squadId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new squad.
    /// </summary>
    Task<Result<SquadDto>> CreateAsync(
        string captainUserId,
        CreateSquadRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a squad.
    /// </summary>
    Task<Result<SquadDto>> UpdateAsync(
        string squadId,
        string requestingUserId,
        UpdateSquadRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Joins a squad.
    /// </summary>
    Task<Result<SquadDto>> JoinAsync(
        string squadId,
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Leaves a squad.
    /// </summary>
    Task<Result> LeaveAsync(
        string squadId,
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a member from a squad (captain only).
    /// </summary>
    Task<Result> RemoveMemberAsync(
        string squadId,
        string captainUserId,
        string memberUserId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Transfers captain role.
    /// </summary>
    Task<Result<SquadDto>> TransferCaptainAsync(
        string squadId,
        string currentCaptainId,
        string newCaptainId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for squads.
    /// </summary>
    Task<Result<List<SquadSearchResultDto>>> SearchAsync(
        string? searchTerm,
        string? tag,
        bool? isRecruiting,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets squad leaderboard.
    /// </summary>
    Task<Result<List<SquadLeaderboardEntryDto>>> GetLeaderboardAsync(
        bool weekly = false,
        int limit = 20,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a squad (captain only).
    /// </summary>
    Task<Result> DeleteAsync(
        string squadId,
        string captainUserId,
        CancellationToken cancellationToken = default);
}
