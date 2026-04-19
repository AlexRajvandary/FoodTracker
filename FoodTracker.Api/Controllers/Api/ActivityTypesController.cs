using FoodTracker.Api.Extensions;
using FoodTracker.Application.Features.Activities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] string? q, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ListActivityTypesQuery { Q = q }, cancellationToken).ConfigureAwait(false);
        return result.ToAuthActionResult();
    }
}
