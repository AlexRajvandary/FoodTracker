using FoodTracker.Api.Contracts;
using FoodTracker.Api.Extensions;
using FoodTracker.Application.DTOs;
using FoodTracker.Application.Features.FoodEntry;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodTracker.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodEntriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FoodEntriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        [ProducesResponseType(typeof(FoodEntryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(CreateFoodEntryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                return Unauthorized();
            }

            var query = new CreateFoodEntryCommand
            {
                ConsumedAtUtc = request.ConsumedAtUtc,
                FoodId = request.FoodId,
                GramsConsumed = request.GramsConsumed,
                PortionsConsumed = request.PortionsConsumed,
                UserId = userId
            };

            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return result.ToActionResult(Ok);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin, user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                return Unauthorized();
            }

            var command = new DeleteFoodEntryCommand
            {
                FoodEntryId = id,
                UserId = userId
            };

            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return result.ToActionResult(NoContent);
        }

        [HttpPatch]
        [Authorize(Roles = "admin, user")]
        [ProducesResponseType(typeof(FoodEntryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Patch(PatchFoodEntryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                return Unauthorized();
            }

            var command = new PatchFoodEntryCommand
            {
                ConsumedAtUtc = request.ConsumedAtUtc,
                FoodItemId = request.FoodItemId,
                GramsConsumed = request.GramsConsumed,
                PortionConsumed = request.PortionConsumed,
                UserId = userId
            };

            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return result.ToActionResult(Ok);
        }
    }
}