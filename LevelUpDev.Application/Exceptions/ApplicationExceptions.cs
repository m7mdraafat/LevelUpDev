using LevelUpDev.Domain.Common;

namespace LevelUpDev.Application.Exceptions;

/// <summary>
/// Base exception for application-level errors.
/// </summary>
public abstract class ApplicationException : Exception
{
    public Error Error { get; }

    protected ApplicationException(Error error)
        : base(error.Description)
    {
        Error = error;
    }

    protected ApplicationException(Error error, Exception innerException)
        : base(error.Description, innerException)
    {
        Error = error;
    }
}

/// <summary>
/// Exception thrown when a requested resource is not found.
/// </summary>
public class NotFoundException : ApplicationException
{
    public NotFoundException(string entityName, string id)
        : base(Error.NotFound(entityName, id))
    {
    }
}

/// <summary>
/// Exception thrown when validation fails.
/// </summary>
public class ValidationException : ApplicationException
{
    public IReadOnlyList<Error> Errors { get; }

    public ValidationException(IReadOnlyList<Error> errors)
        : base(errors.FirstOrDefault() ?? Error.Validation("Unknown", "Validation failed"))
    {
        Errors = errors;
    }

    public ValidationException(string propertyName, string message)
        : base(Error.Validation(propertyName, message))
    {
        Errors = new List<Error> { Error };
    }
}

/// <summary>
/// Exception thrown when a conflict occurs (e.g., duplicate resource).
/// </summary>
public class ConflictException : ApplicationException
{
    public ConflictException(string entityName, string message)
        : base(Error.Conflict(entityName, message))
    {
    }
}

/// <summary>
/// Exception thrown when access is unauthorized.
/// </summary>
public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message = "Unauthorized access")
        : base(Error.Unauthorized(message))
    {
    }
}

/// <summary>
/// Exception thrown when access is forbidden.
/// </summary>
public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message = "Access denied")
        : base(Error.Forbidden(message))
    {
    }
}

/// <summary>
/// Exception thrown when an external service fails.
/// </summary>
public class ExternalServiceException : ApplicationException
{
    public ExternalServiceException(string service, string message)
        : base(Error.External(service, message))
    {
    }

    public ExternalServiceException(string service, string message, Exception innerException)
        : base(Error.External(service, message), innerException)
    {
    }
}

/// <summary>
/// Exception thrown when a database operation fails.
/// </summary>
public class DatabaseException : ApplicationException
{
    public DatabaseException(string message)
        : base(Error.Database(message))
    {
    }

    public DatabaseException(string message, Exception innerException)
        : base(Error.Database(message), innerException)
    {
    }
}
