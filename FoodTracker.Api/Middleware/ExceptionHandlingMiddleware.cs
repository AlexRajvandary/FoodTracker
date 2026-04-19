using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracker.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var cancellationToken = context.RequestAborted;
        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (ValidationException exception)
        {
            await WriteValidationProblemAsync(context, exception, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception.");
            await WriteUnhandledProblemAsync(context, cancellationToken).ConfigureAwait(false);
        }
    }

    private static async Task WriteValidationProblemAsync(
        HttpContext context,
        ValidationException exception,
        CancellationToken cancellationToken)
    {
        var errors = exception.Errors
            .GroupBy(failure => string.IsNullOrEmpty(failure.PropertyName) ? "_" : failure.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(failure => failure.ErrorMessage).ToArray());

        var problemDetails = new ValidationProblemDetails(errors)
        {
            Title = "Validation failed",
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
    }

    private static async Task WriteUnhandledProblemAsync(HttpContext context, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An unexpected error occurred.",
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
    }
}