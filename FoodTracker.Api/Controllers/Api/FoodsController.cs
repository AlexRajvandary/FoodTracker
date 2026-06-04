using System.Security.Claims;
using FoodTracker.Api.Contracts;
using FoodTracker.Api.Extensions;
using FoodTracker.Application.Features.Nutrition;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracker.Api.Controllers.Api;

[ApiController]
[Route("api/foods")]
[Authorize]
public class FoodsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FoodsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("catalog")]
    [Authorize(Roles = "admin, user")]
    [ProducesResponseType(typeof(IReadOnlyList<FoodItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Catalog([FromQuery] string? query, [FromQuery] string? category, CancellationToken cancellationToken)
    {
        var result = await _mediator
            .Send(new ListFoodCatalogQuery { Query = query , Category = category }, cancellationToken)
            .ConfigureAwait(false);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(FoodItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] CreateFoodItemRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }

        var command = new CreateFoodItemCommand
        {
            UserId = userId,
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            CaloriesPer100g = request.CaloriesPer100g,
            ProteinsPer100g = request.ProteinsPer100g,
            FatsPer100g = request.FatsPer100g,
            CarbsPer100g = request.CarbsPer100g,
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToAuthActionResult(Ok);
    }

    [HttpDelete("{foodItemId:guid}")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid foodItemId, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }

        var command = new DeleteFoodItemCommand
        {
            UserId = userId,
            FoodItemId = foodItemId
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);

        return result.ToAuthActionResult(NoContent);
    }

    [HttpPatch("{foodItemId:guid}")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(FoodItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(Guid foodItemId, [FromBody] PatchFoodItemRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }

        if (request == null) return BadRequest();

        var command = new PatchFoodItemCommand
        {
            UserId = userId,
            FoodItemId = foodItemId,
            Name = request.Name,
            Description = request.Description,
            CaloriesPer100g = request.CaloriesPer100g,
            ProteinsPer100g = request.ProteinsPer100g,
            FatsPer100g = request.FatsPer100g,
            CarbsPer100g = request.CarbsPer100g,
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToAuthActionResult(Ok);
    }

    [HttpPut("{foodItemId:guid}")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(FoodItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid foodItemId, [FromBody] UpdateFoodItemRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }

        var command = new UpdateFoodItemCommand
        {
            UserId = userId,
            FoodItemId = foodItemId,
            Name = request.Name,
            Description = request.Description,
            CaloriesPer100g = request.CaloriesPer100g,
            ProteinsPer100g = request.ProteinsPer100g,
            FatsPer100g = request.FatsPer100g,
            CarbsPer100g = request.CarbsPer100g,
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);

        return result.ToAuthActionResult(Ok);
    }
}