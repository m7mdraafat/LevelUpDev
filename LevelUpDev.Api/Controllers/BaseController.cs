using Microsoft.AspNetCore.Mvc;
using LevelUpDev.Application.DTOs.Common;
using System.Text;
using System.Text.Json;

namespace LevelUpDev.Api.Controllers;

/// <summary>
/// Base controller with common functionality.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Creates a success response with data.
    /// </summary>
    protected ActionResult<ApiResponse<T>> Success<T>(T data, string? message = null)
    {
        return Ok(ApiResponse<T>.Ok(data, message ?? "Success"));
    }

    /// <summary>
    /// Creates a success response with pagination.
    /// </summary>
    protected ActionResult<ApiResponse<IReadOnlyList<T>>> Success<T>(
        IReadOnlyList<T> data,
        int page,
        int pageSize,
        int totalCount,
        string? message = null)
    {
        return Ok(ApiResponse<IReadOnlyList<T>>.Ok(data, page, pageSize, totalCount, message ?? "Success"));
    }

    /// <summary>
    /// Creates a created response.
    /// </summary>
    protected ActionResult<ApiResponse<T>> Created<T>(T data, string location)
    {
        var response = ApiResponse<T>.Ok(data, "Created successfully");
        return base.Created(location, response);
    }

    /// <summary>
    /// Creates a no content response.
    /// </summary>
    protected new ActionResult NoContent()
    {
        return base.NoContent();
    }

    #region Azure Easy Auth - GitHub Identity

    /// <summary>
    /// Gets the GitHub User ID from Azure Easy Auth headers.
    /// This is the unique identifier for the user.
    /// </summary>
    protected string? GetGitHubUserId()
    {
        // Azure Easy Auth header
        if (Request.Headers.TryGetValue("X-MS-CLIENT-PRINCIPAL-ID", out var principalId))
        {
            return principalId.ToString();
        }

        // Fallback to claims (for local development with JWT)
        return User.FindFirst("sub")?.Value
            ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    }

    /// <summary>
    /// Gets the GitHub username from Azure Easy Auth headers.
    /// </summary>
    protected string? GetGitHubUsername()
    {
        // Azure Easy Auth header
        if (Request.Headers.TryGetValue("X-MS-CLIENT-PRINCIPAL-NAME", out var principalName))
        {
            return principalName.ToString();
        }

        // Fallback to claims
        return User.FindFirst("name")?.Value
            ?? User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
    }

    /// <summary>
    /// Gets the current user ID (alias for GetGitHubUserId for backward compatibility).
    /// </summary>
    protected string? GetCurrentUserId() => GetGitHubUserId();

    /// <summary>
    /// Checks if the current request is authenticated.
    /// </summary>
    protected bool IsAuthenticated()
    {
        return !string.IsNullOrEmpty(GetGitHubUserId());
    }

    /// <summary>
    /// Gets all claims from Azure Easy Auth principal.
    /// Useful for debugging and getting additional user info.
    /// </summary>
    protected Dictionary<string, string>? GetEasyAuthClaims()
    {
        if (!Request.Headers.TryGetValue("X-MS-CLIENT-PRINCIPAL", out var principalHeader))
        {
            return null;
        }

        try
        {
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(principalHeader.ToString()));
            var principal = JsonSerializer.Deserialize<EasyAuthPrincipal>(decoded);
            
            return principal?.Claims?
                .ToDictionary(c => c.Type, c => c.Value);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets a specific claim value from Azure Easy Auth.
    /// </summary>
    protected string? GetEasyAuthClaim(string claimType)
    {
        var claims = GetEasyAuthClaims();
        return claims?.GetValueOrDefault(claimType);
    }

    #endregion
}

/// <summary>
/// Model for deserializing Azure Easy Auth principal.
/// </summary>
internal class EasyAuthPrincipal
{
    public string? IdentityProvider { get; set; }
    public string? UserId { get; set; }
    public string? UserDetails { get; set; }
    public List<EasyAuthClaim>? Claims { get; set; }
}

/// <summary>
/// Model for Azure Easy Auth claim.
/// </summary>
internal class EasyAuthClaim
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
