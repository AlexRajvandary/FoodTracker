using System.Security.Claims;
using FoodTracker.Api.Contracts;
using FoodTracker.Api.Extensions;
using FoodTracker.Application.DTOs;
using FoodTracker.Application.Features.Nutrition;
using FoodTracker.Domain.Common.Results;
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
    [ProducesResponseType(typeof(PagedList<ShortFoodItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Catalog([FromBody] FoodCatalogRequest request, CancellationToken cancellationToken)
    {
        if (request.Page < 1)
        {
            request.Page = 1;
        }

        if (request.PageSize < 1)
        {
            request.PageSize = 10;
        }

        var listFoodCatalogQuery = new ListFoodCatalogQuery
        {
            Brand = request.Brand,
            CategoryIds = request.CategoryIds,
            Page = request.Page,
            PageSize = request.PageSize,
            Query = request.Query,
            SortDescending = request.SortDescending,
            SortBy = request.SortBy
        };

        var result = await _mediator
            .Send(listFoodCatalogQuery, cancellationToken)
            .ConfigureAwait(false);

        return result.ToActionResult(Ok);
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
            Barcode = request.Barcode,
            Brand = request.Brand,
            CaloriesPer100g = request.CaloriesPer100g,
            CategoryIds = request.CategoryIds,
            CarbsPer100g = request.CarbsPer100g,
            Description = request.Description,
            ExternalId = request.ExternalId,
            FatsPer100g = request.FatsPer100g,
            FiberPer100g = request.FiberPer100g,
            ImageUrl = request.ImageUrl,
            Name = request.Name,
            NewCategoryNames = request.NewCategoryNames,
            ProteinsPer100g = request.ProteinsPer100g,
            SaltPer100g = request.SaltPer100g,
            SaturatedFatPer100g = request.SaturatedFatPer100g,
            ServingSizeGrams = request.ServingSizeGrams,
            SugarsPer100g = request.SugarsPer100g,
            UserId = userId
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToActionResult(Ok);
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
        return result.ToActionResult(NoContent);
    }

    [HttpGet("{foodItemId:guid}")]
    [Authorize(Roles = "admin, user")]
    [ProducesResponseType(typeof(FoodItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid foodItemId, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }

        var query = new GetFoodItemQuery
        {
            FoodItemId = foodItemId
        };

        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return result.ToActionResult(Ok);
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
            Barcode = request.Barcode,
            Brand = request.Brand,
            CaloriesPer100g = request.CaloriesPer100g,
            CategoryIds = request.CategoryIds,
            CarbsPer100g = request.CarbsPer100g,
            Description = request.Description,
            FatsPer100g = request.FatsPer100g,
            FoodItemId = foodItemId,
            FiberPer100g = request.FiberPer100g,
            ImageUrl = request.ImageUrl,
            Name = request.Name,
            NewCategoryNames = request.NewCategoryNames,
            ProteinsPer100g = request.ProteinsPer100g,
            SaltPer100g = request.SaltPer100g,
            SaturatedFatPer100g = request.SaturatedFatPer100g,
            ServingSizeGrams = request.ServingSizeGrams,
            SugarsPer100g = request.SugarsPer100g,
            UserId = userId,
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToActionResult(Ok);
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
            Barcode = request.Barcode,
            Brand = request.Brand,
            CaloriesPer100g = request.CaloriesPer100g,
            CategoryIds = request.CategoryIds,
            CarbsPer100g = request.CarbsPer100g,
            Description = request.Description,
            FatsPer100g = request.FatsPer100g,
            FiberPer100g = request.FiberPer100g,
            FoodItemId = foodItemId,
            ImageUrl = request.ImageUrl,
            Name = request.Name,
            NewCategoryNames = request.NewCategoryNames,
            ProteinsPer100g = request.ProteinsPer100g,
            SaltPer100g = request.SaltPer100g,
            SaturatedFatPer100g = request.SaturatedFatPer100g,
            ServingSizeGrams = request.ServingSizeGrams,
            SugarsPer100g = request.SugarsPer100g,
            UserId = userId
        };

        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToActionResult(Ok);
    }
}