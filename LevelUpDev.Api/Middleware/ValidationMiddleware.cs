using FluentValidation;
using FluentValidation.Results;
using LevelUpDev.Application.Exceptions;
using LevelUpDev.Domain.Common;

namespace LevelUpDev.Api.Middleware;

/// <summary>
/// Middleware for automatic request validation using FluentValidation.
/// </summary>
public class ValidationMiddleware<TRequest> : IMiddleware where TRequest : class
{
    private readonly IValidator<TRequest> _validator;
    private readonly ILogger<ValidationMiddleware<TRequest>> _logger;

    public ValidationMiddleware(
        IValidator<TRequest> validator,
        ILogger<ValidationMiddleware<TRequest>> logger)
    {
        _validator = validator;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // This middleware is typically used via endpoint filters
        // For controller-based validation, use the ValidationFilter attribute
        await next(context);
    }
}

/// <summary>
/// Action filter for validating requests with FluentValidation.
/// </summary>
public class ValidationFilter<TRequest> : IEndpointFilter where TRequest : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var request = context.Arguments
            .OfType<TRequest>()
            .FirstOrDefault();

        if (request is null)
        {
            return await next(context);
        }

        var validator = context.HttpContext.RequestServices
            .GetService<IValidator<TRequest>>();

        if (validator is null)
        {
            return await next(context);
        }

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw CreateValidationException(validationResult);
        }

        return await next(context);
    }

    private static Application.Exceptions.ValidationException CreateValidationException(
        ValidationResult validationResult)
    {
        var errors = validationResult.Errors
            .Select(e => Error.Validation(e.PropertyName, e.ErrorMessage))
            .ToList();

        return new Application.Exceptions.ValidationException(errors);
    }
}
