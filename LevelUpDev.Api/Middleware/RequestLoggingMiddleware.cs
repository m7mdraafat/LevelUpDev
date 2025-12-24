using System.Diagnostics;

namespace LevelUpDev.Api.Middleware;

/// <summary>
/// Middleware to log HTTP requests with structured logging.
/// Captures request/response details and timing information.
/// </summary>
public class RequestLoggingMiddleware : IMiddleware
{
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestId = context.TraceIdentifier;

        // Log request start
        _logger.LogInformation(
            "Request started: {Method} {Path} | RequestId: {RequestId} | User: {User}",
            context.Request.Method,
            context.Request.Path,
            requestId,
            context.User.Identity?.Name ?? "Anonymous");

        try
        {
            await next(context);

            stopwatch.Stop();

            // Log request completion
            _logger.LogInformation(
                "Request completed: {Method} {Path} | Status: {StatusCode} | Duration: {Duration}ms | RequestId: {RequestId}",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds,
                requestId);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(
                ex,
                "Request failed: {Method} {Path} | Duration: {Duration}ms | RequestId: {RequestId}",
                context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds,
                requestId);

            throw; // Re-throw to let GlobalExceptionHandler handle it
        }
    }
}
