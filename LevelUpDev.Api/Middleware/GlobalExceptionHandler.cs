using System.Net;
using System.Text.Json;
using LevelUpDev.Application.DTOs.Common;
using LevelUpDev.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LevelUpDev.Api.Middleware;

/// <summary>
/// Global exception handling middleware.
/// Catches all unhandled exceptions and returns consistent error responses.
/// </summary>
public class GlobalExceptionHandler : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _environment;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger,
        IHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, errorResponse) = exception switch
        {
            NotFoundException notFound => (
                HttpStatusCode.NotFound,
                CreateErrorResponse("Not Found", notFound.Message, "NOT_FOUND")),

            ValidationException validation => (
                HttpStatusCode.BadRequest,
                CreateValidationErrorResponse(validation)),

            ConflictException conflict => (
                HttpStatusCode.Conflict,
                CreateErrorResponse("Conflict", conflict.Message, "CONFLICT")),

            UnauthorizedException unauthorized => (
                HttpStatusCode.Unauthorized,
                CreateErrorResponse("Unauthorized", unauthorized.Message, "UNAUTHORIZED")),

            ForbiddenException forbidden => (
                HttpStatusCode.Forbidden,
                CreateErrorResponse("Forbidden", forbidden.Message, "FORBIDDEN")),

            ExternalServiceException external => (
                HttpStatusCode.BadGateway,
                CreateErrorResponse("External Service Error", external.Message, "EXTERNAL_SERVICE_ERROR")),

            DatabaseException database => (
                HttpStatusCode.ServiceUnavailable,
                CreateErrorResponse("Database Error", database.Message, "DATABASE_ERROR")),

            OperationCanceledException => (
                HttpStatusCode.RequestTimeout,
                CreateErrorResponse("Request Timeout", "The request was cancelled or timed out", "REQUEST_TIMEOUT")),

            _ => (
                HttpStatusCode.InternalServerError,
                CreateErrorResponse("Internal Server Error", GetExceptionMessage(exception), "INTERNAL_ERROR"))
        };

        LogException(exception, statusCode);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }

    private ApiResponse<object> CreateErrorResponse(string title, string detail, string code)
    {
        return ApiResponse<object>.Fail(new ApiError
        {
            Code = code,
            Message = title,
            Details = detail,
            Timestamp = DateTime.UtcNow
        });
    }

    private ApiResponse<object> CreateValidationErrorResponse(ValidationException validation)
    {
        var errors = validation.Errors
            .Select(e => new ApiError
            {
                Code = "VALIDATION_ERROR",
                Message = e.Code,
                Details = e.Description,
                Timestamp = DateTime.UtcNow
            })
            .ToList();

        return new ApiResponse<object>
        {
            Success = false,
            Message = "Validation failed",
            Errors = errors,
            Timestamp = DateTime.UtcNow
        };
    }

    private string GetExceptionMessage(Exception exception)
    {
        // Only show detailed error messages in development
        return _environment.IsDevelopment()
            ? exception.Message
            : "An unexpected error occurred. Please try again later.";
    }

    private void LogException(Exception exception, HttpStatusCode statusCode)
    {
        var logLevel = statusCode switch
        {
            HttpStatusCode.BadRequest => LogLevel.Warning,
            HttpStatusCode.NotFound => LogLevel.Warning,
            HttpStatusCode.Unauthorized => LogLevel.Warning,
            HttpStatusCode.Forbidden => LogLevel.Warning,
            HttpStatusCode.Conflict => LogLevel.Warning,
            _ => LogLevel.Error
        };

        _logger.Log(
            logLevel,
            exception,
            "Request failed with status {StatusCode}: {Message}",
            (int)statusCode,
            exception.Message);
    }
}
