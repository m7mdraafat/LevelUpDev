using LevelUpDev.Application.DTOs.Challenges;
using LevelUpDev.Domain.Common;

namespace LevelUpDev.Application.Interfaces;

/// <summary>
/// Service interface for challenge operations.
/// </summary>
public interface IChallengeService
{
    /// <summary>
    /// Gets today's challenge.
    /// </summary>
    Task<Result<DailyChallengeDto>> GetTodaysChallengeAsync(
        string? userId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets challenge for a specific date.
    /// </summary>
    Task<Result<DailyChallengeDto>> GetByDateAsync(
        DateOnly date,
        string? userId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets challenge history for a user.
    /// </summary>
    Task<Result<ChallengeHistoryDto>> GetUserHistoryAsync(
        string userId,
        int days = 30,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks a user as participating in today's challenge.
    /// </summary>
    Task<Result> ParticipateAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks a user as completing today's challenge.
    /// </summary>
    Task<Result> CompleteAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new daily challenge (admin).
    /// </summary>
    Task<Result<DailyChallengeDto>> CreateAsync(
        CreateDailyChallengeRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets current community goal.
    /// </summary>
    Task<Result<CommunityGoalDto>> GetCurrentCommunityGoalAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a community goal (admin).
    /// </summary>
    Task<Result<CommunityGoalDto>> CreateCommunityGoalAsync(
        CreateCommunityGoalRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Contributes to community goal.
    /// </summary>
    Task<Result> ContributeToCommunityGoalAsync(
        string userId,
        int contribution,
        CancellationToken cancellationToken = default);
}
