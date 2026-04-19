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

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SearchFoodItemsQuery { Q = q }, cancellationToken).ConfigureAwait(false);
        return result.ToAuthActionResult();
    }

    [HttpGet("catalog")]
    public async Task<IActionResult> Catalog([FromQuery] string? q, [FromQuery] string? category, CancellationToken cancellationToken)
    {
        var result = await _mediator
            .Send(new ListFoodCatalogQuery { Q = q, Category = category }, cancellationToken)
            .ConfigureAwait(false);
        return result.ToAuthActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateFoodItem([FromBody] CreateFoodItemRequest body, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }

        var command = new CreateFoodItemCommand
        {
            UserId = userId,
            Name = body.Name,
            Description = body.Description,
            CaloriesPer100g = body.CaloriesPer100g,
            ProteinsPer100g = body.ProteinsPer100g,
            FatsPer100g = body.FatsPer100g,
            CarbsPer100g = body.CarbsPer100g,
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToAuthActionResult(dto => new ObjectResult(dto) { StatusCode = StatusCodes.Status202Accepted });
    }
}
