namespace LevelUpDev.Domain.Common;

/// <summary>
/// Represents a domain error with a code and description.
/// </summary>
public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "A null value was provided");

    public static Error NotFound(string entityName, string id) =>
        new($"{entityName}.NotFound", $"{entityName} with ID '{id}' was not found");

    public static Error Validation(string propertyName, string message) =>
        new($"Validation.{propertyName}", message);

    public static Error Conflict(string entityName, string message) =>
        new($"{entityName}.Conflict", message);

    public static Error Unauthorized(string message = "Unauthorized access") =>
        new("Auth.Unauthorized", message);

    public static Error Forbidden(string message = "Access denied") =>
        new("Auth.Forbidden", message);

    public static Error Database(string message) =>
        new("Database.Error", message);

    public static Error External(string service, string message) =>
        new($"External.{service}", message);
}
