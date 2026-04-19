using FoodTracker.Application.Features.Auth;
using FoodTracker.Application.Features.Nutrition;
using FoodTracker.Domain.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracker.Api.Extensions;

public static class AuthResultExtensions
{
    public static IActionResult ToAuthActionResult<T>(this Result<T> result, Func<T, IActionResult>? onSuccess = null)
    {
        if (result.IsSuccess)
        {
            return onSuccess is not null ? onSuccess(result.Value) : new OkObjectResult(result.Value);
        }

        return MapFailure(result.Error!);
    }

    public static IActionResult ToAuthActionResult(this Result result, Func<IActionResult>? onSuccess = null)
    {
        if (result.IsSuccess)
        {
            return onSuccess is not null ? onSuccess() : new NoContentResult();
        }

        return MapFailure(result.Error!);
    }

    private static IActionResult MapFailure(Error error)
    {
        var body = new { message = error.Message };
        return error.Code switch
        {
            AuthErrorCodes.Unauthorized or AuthErrorCodes.TelegramInvalidInitData or AuthErrorCodes.TelegramMissingUser =>
                new UnauthorizedObjectResult(body),
            AuthErrorCodes.Conflict => new ConflictObjectResult(body),
            AuthErrorCodes.TelegramNotConfigured => new ObjectResult(body) { StatusCode = StatusCodes.Status503ServiceUnavailable },
            AuthErrorCodes.NotFound => new NotFoundObjectResult(body),
            FoodErrorCodes.FoodItemNotFound or FoodErrorCodes.EntryNotFound => new NotFoundObjectResult(body),
            FoodErrorCodes.InvalidAmount or FoodErrorCodes.InvalidPatch or FoodErrorCodes.InvalidCategory =>
                new BadRequestObjectResult(body),
            _ => new BadRequestObjectResult(body),
        };
    }
}
