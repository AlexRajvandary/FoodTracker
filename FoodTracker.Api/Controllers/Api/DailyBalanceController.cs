using FoodTracker.Api.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodTracker.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyBalanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DailyBalanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> GetDailyBalance(GetDailyBalanceRequest getDailyBalanceRequest, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                return Unauthorized();
            }
            var query = new GetDailyBalanceQuery { UserId = userId };
            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
    }
}