using LevelUpDev.Application.DTOs.Users;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Domain.Common;

namespace LevelUpDev.Application.Interfaces;

/// <summary>
/// Service interface for user operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    Task<Result<UserProfileDto>> GetByIdAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user by GitHub ID.
    /// </summary>
    Task<Result<UserProfileDto>> GetByGitHubIdAsync(
        string gitHubId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers a new user.
    /// </summary>
    Task<Result<UserProfileDto>> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a user's profile.
    /// </summary>
    Task<Result<UserProfileDto>> UpdateProfileAsync(
        string userId,
        UpdateUserProfileRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user's settings.
    /// </summary>
    Task<Result<UserSettingsDto>> GetSettingsAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a user's settings.
    /// </summary>
    Task<Result<UserSettingsDto>> UpdateSettingsAsync(
        string userId,
        UpdateUserSettingsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches users by display name.
    /// </summary>
    Task<Result<List<UserSearchResultDto>>> SearchAsync(
        string searchTerm,
        int limit = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deactivates a user account.
    /// </summary>
    Task<Result> DeactivateAsync(
        string userId,
        CancellationToken cancellationToken = default);
}
