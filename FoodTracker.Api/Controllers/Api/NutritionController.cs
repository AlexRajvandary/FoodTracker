using System.Security.Claims;
using FoodTracker.Api.Contracts;
using FoodTracker.Api.Extensions;
using FoodTracker.Application.Features.Nutrition;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracker.Api.Controllers.Api;

[ApiController]
[Route("api/nutrition")]
[Authorize]
public class NutritionController : ControllerBase
{
    private readonly IMediator _mediator;

    public NutritionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("food-entries")]
    public async Task<IActionResult> ListFoodEntries(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] DateOnly? date,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var result = await _mediator
            .Send(
                new ListFoodEntriesQuery
                {
                    UserId = userId,
                    FromUtc = NormalizeQueryDate(from),
                    ToUtc = NormalizeQueryDate(to),
                    Date = date,
                },
                cancellationToken)
            .ConfigureAwait(false);
        return result.ToAuthActionResult();
    }

    [HttpPost("food-entries")]
    public async Task<IActionResult> CreateFoodEntry(
        [FromBody] CreateFoodEntryRequest body,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var command = new CreateFoodEntryCommand
        {
            UserId = userId,
            FoodItemId = body.FoodItemId,
            ConsumedAt = body.ConsumedAt,
            GramsConsumed = body.GramsConsumed,
            PortionCount = body.PortionCount,
            PortionNote = body.PortionNote,
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToAuthActionResult(dto => new ObjectResult(dto) { StatusCode = StatusCodes.Status201Created });
    }

    [HttpPatch("food-entries/{id:guid}")]
    public async Task<IActionResult> UpdateFoodEntry(
        [FromRoute] Guid id,
        [FromBody] UpdateFoodEntryRequest body,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var command = new UpdateFoodEntryCommand
        {
            UserId = userId,
            EntryId = id,
            GramsConsumed = body.GramsConsumed,
            PortionNote = body.PortionNote,
            ConsumedAt = body.ConsumedAt,
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToAuthActionResult();
    }

    [HttpDelete("food-entries/{id:guid}")]
    public async Task<IActionResult> DeleteFoodEntry([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var result = await _mediator
            .Send(new DeleteFoodEntryCommand { UserId = userId, EntryId = id }, cancellationToken)
            .ConfigureAwait(false);
        return result.ToAuthActionResult(() => new NoContentResult());
    }

    private bool TryGetUserId(out Guid userId) =>
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

    private static DateTime? NormalizeQueryDate(DateTime? value)
    {
        if (value is null)
        {
            return null;
        }

        var v = value.Value;
        return v.Kind switch
        {
            DateTimeKind.Utc => v,
            DateTimeKind.Local => v.ToUniversalTime(),
            _ => DateTime.SpecifyKind(v, DateTimeKind.Utc),
        };
    }
}
