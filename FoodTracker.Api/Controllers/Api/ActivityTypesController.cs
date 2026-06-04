using FoodTracker.Api.Contracts;
using FoodTracker.Api.Extensions;
using FoodTracker.Application.Features.Activities;
using FoodTracker.Application.Features.Nutrition;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodTracker.Api.Controllers.Api;

[ApiController]
[Route("api/activity-types")]
[Authorize]
public class ActivityTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivityTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("catalog")]
    [Authorize(Roles = "admin, user")]
    [ProducesResponseType(typeof(IReadOnlyList<ActivityTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Catalog([FromQuery] string? query, [FromQuery] string? category, CancellationToken cancellationToken)
    {
        var result = await _mediator
            .Send(new ListFoodCatalogQuery { Query = query, Category = category }, cancellationToken)
            .ConfigureAwait(false);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(FoodItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] CreateActivityTypeRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }

        var command = new CreateActivityTypeCommand
        {
            Name = request.Name,
            Description = request.Description,
            CaloriesPer100Reps = request.CaloriesPer100Reps,
            CaloriesPerHour = request.CaloriesPerHour,
            Category = request.Category
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToAuthActionResult(Ok);
    }

    [HttpDelete("{activityTypeId:guid}")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid activityTypeId, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }

        var command = new DeleteActivityTypeCommand
        {
            UserId = userId,
            ActivityTypeId = activityTypeId
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToAuthActionResult(NoContent);
    }

    [HttpPatch("{activityTypeId:guid}")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(ActivityTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(Guid activityTypeId, [FromBody] PatchActivityTypeRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }

        if (request == null) return BadRequest();

        var command = new PatchActivityTypeCommand
        {
            UserId = userId,
            ActivityTypeId = activityTypeId,
            Name = request.Name,
            Description = request.Description,
            CaloriesPer100Reps = request.CaloriesPer100Reps,
            CaloriesPerHour = request.CaloriesPerHour,
            Category = request.Category
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToAuthActionResult(Ok);
    }

    [HttpPut("{activityTypeId:guid}")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(ActivityTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid activityTypeId, [FromBody] UpdateActivityTypeRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }

        var command = new UpdateActivityTypeCommand
        {
            UserId = userId,
            ActivityTypeId = activityTypeId,
            Name = request.Name,
            Description = request.Description,
            CaloriesPer100Reps = request.CaloriesPer100Reps,
            CaloriesPerHour = request.CaloriesPerHour,
            Category = request.Category
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToAuthActionResult(Ok);
    }
}