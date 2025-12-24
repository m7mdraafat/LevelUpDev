namespace LevelUpDev.Domain.Common;

/// <summary>
/// Represents the result of a Cosmos DB query operation.
/// Includes pagination info and request charge for monitoring.
/// </summary>
public class QueryResult<TValue> : Result<TValue>
{
    public double RequestCharge { get; }
    public string? ContinuationToken { get; }
    public int? TotalCount { get; }

    private QueryResult(
        TValue value,
        bool isSuccess,
        Error error,
        double requestCharge = 0,
        string? continuationToken = null,
        int? totalCount = null)
        : base(value, isSuccess, error)
    {
        RequestCharge = requestCharge;
        ContinuationToken = continuationToken;
        TotalCount = totalCount;
    }

    public static QueryResult<TValue> Success(
        TValue value,
        double requestCharge,
        string? continuationToken = null,
        int? totalCount = null) =>
        new(value, true, Error.None, requestCharge, continuationToken, totalCount);

    public static new QueryResult<TValue> Failure(Error error) =>
        new(default!, false, error);
}

/// <summary>
/// Represents a paginated list of items from Cosmos DB.
/// </summary>
public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    public string? ContinuationToken { get; }

    public PagedList(
        IReadOnlyList<T> items,
        int pageNumber,
        int pageSize,
        int totalCount,
        string? continuationToken = null)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        ContinuationToken = continuationToken;
    }
}
