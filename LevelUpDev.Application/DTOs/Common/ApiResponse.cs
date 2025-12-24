namespace LevelUpDev.Application.DTOs.Common;

/// <summary>
/// Standard API response wrapper.
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? Message { get; init; }
    public List<ApiError>? Errors { get; init; }
    public ApiMetadata? Metadata { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;

    public static ApiResponse<T> Ok(T data, string? message = null) =>
        new() { Success = true, Data = data, Message = message };

    public static ApiResponse<T> Ok(T data, ApiMetadata metadata, string? message = null) =>
        new() { Success = true, Data = data, Message = message, Metadata = metadata };

    public static ApiResponse<T> Ok(T data, int page, int pageSize, int totalCount, string? message = null)
    {
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        return new()
        {
            Success = true,
            Data = data,
            Message = message,
            Metadata = new ApiMetadata(
                new PaginationInfo(page, pageSize, totalCount, totalPages, page > 1, page < totalPages, null),
                null,
                null
            )
        };
    }

    public static ApiResponse<T> Fail(string error) =>
        new()
        {
            Success = false,
            Errors = new List<ApiError>
            {
                new() { Code = "ERROR", Message = error, Timestamp = DateTime.UtcNow }
            }
        };

    public static ApiResponse<T> Fail(ApiError error) =>
        new() { Success = false, Errors = new List<ApiError> { error } };

    public static ApiResponse<T> Fail(List<ApiError> errors) =>
        new() { Success = false, Errors = errors };
}

/// <summary>
/// API error details.
/// </summary>
public class ApiError
{
    public required string Code { get; init; }
    public required string Message { get; init; }
    public string? Details { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}

/// <summary>
/// API metadata for pagination and performance.
/// </summary>
public record ApiMetadata(
    PaginationInfo? Pagination,
    double? RequestCharge,
    long? ExecutionTimeMs
);

/// <summary>
/// Pagination information.
/// </summary>
public record PaginationInfo(
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasPreviousPage,
    bool HasNextPage,
    string? ContinuationToken
);

/// <summary>
/// Paginated request parameters.
/// </summary>
public record PaginatedRequest(
    int PageNumber = 1,
    int PageSize = 20,
    string? ContinuationToken = null
)
{
    public int PageNumber { get; init; } = Math.Max(1, PageNumber);
    public int PageSize { get; init; } = Math.Clamp(PageSize, 1, 100);
}

/// <summary>
/// Generic paginated response.
/// </summary>
public record PaginatedResponse<T>(
    List<T> Items,
    PaginationInfo Pagination
);
