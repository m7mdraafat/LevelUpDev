using Microsoft.AspNetCore.Mvc;
using LevelUpDev.Application.DTOs.Common;

namespace LevelUpDev.Api.Controllers;

/// <summary>
/// Authentication and identity endpoints.
/// </summary>
public class AuthController : BaseController
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get current authenticated user info from GitHub.
    /// Useful for debugging and verifying authentication.
    /// </summary>
    [HttpGet("me")]
    [ProducesResponseType(typeof(ApiResponse<AuthUserInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<ApiResponse<AuthUserInfo>> GetCurrentAuthUser()
    {
        var gitHubId = GetGitHubUserId();
        var gitHubUsername = GetGitHubUsername();

        if (string.IsNullOrEmpty(gitHubId))
        {
            return Unauthorized(ApiResponse<AuthUserInfo>.Fail("Not authenticated. Please sign in with GitHub."));
        }

        var userInfo = new AuthUserInfo(
            GitHubId: gitHubId,
            GitHubUsername: gitHubUsername,
            Claims: GetEasyAuthClaims(),
            IsAuthenticated: true
        );

        _logger.LogInformation(
            "Auth info requested for GitHub user {GitHubUsername} (ID: {GitHubId})",
            gitHubUsername,
            gitHubId);

        return Success(userInfo);
    }

    /// <summary>
    /// Check authentication status (anonymous access allowed).
    /// </summary>
    [HttpGet("status")]
    [ProducesResponseType(typeof(ApiResponse<AuthStatus>), StatusCodes.Status200OK)]
    public ActionResult<ApiResponse<AuthStatus>> GetAuthStatus()
    {
        var isAuthenticated = IsAuthenticated();
        
        var status = new AuthStatus(
            IsAuthenticated: isAuthenticated,
            GitHubId: isAuthenticated ? GetGitHubUserId() : null,
            GitHubUsername: isAuthenticated ? GetGitHubUsername() : null,
            LoginUrl: "/.auth/login/github",
            LogoutUrl: "/.auth/logout"
        );

        return Success(status);
    }

    /// <summary>
    /// Debug endpoint to see all headers (dev only).
    /// </summary>
    [HttpGet("debug/headers")]
    [ProducesResponseType(typeof(ApiResponse<Dictionary<string, string>>), StatusCodes.Status200OK)]
    public ActionResult<ApiResponse<Dictionary<string, string>>> GetHeaders()
    {
        // Only allow in development
        var env = HttpContext.RequestServices.GetRequiredService<IHostEnvironment>();
        if (!env.IsDevelopment())
        {
            return NotFound();
        }

        var headers = Request.Headers
            .ToDictionary(h => h.Key, h => h.Value.ToString());

        return Success(headers);
    }
}

/// <summary>
/// Authenticated user information from GitHub.
/// </summary>
public record AuthUserInfo(
    string GitHubId,
    string? GitHubUsername,
    Dictionary<string, string>? Claims,
    bool IsAuthenticated
);

/// <summary>
/// Authentication status response.
/// </summary>
public record AuthStatus(
    bool IsAuthenticated,
    string? GitHubId,
    string? GitHubUsername,
    string LoginUrl,
    string LogoutUrl
);
