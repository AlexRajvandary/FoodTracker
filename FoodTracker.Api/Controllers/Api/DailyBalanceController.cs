using FoodTracker.Api.Contracts;
using FoodTracker.Application.DTOs;
using FoodTracker.Application.Features.Diary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        [ProducesResponseType(typeof(DailyBalanceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetDailyBalance(GetDailyBalanceRequest getDailyBalanceRequest, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                return Unauthorized();
            }

            var query = new GetDailyBalanceQuery 
            {
                UserId = userId,
                Date = getDailyBalanceRequest.Date 
            };

            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("period")]
        [Authorize(Roles = "admin, user")]
        [ProducesResponseType(typeof(PeriodBalanceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPeriodBalance(GetPeriodBalanceRequest getPeriodBalanceRequest, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                return Unauthorized();
            }

            var query = new GetPeriodBalanceQuery
            {
                UserId = userId,
                StartDate = getPeriodBalanceRequest.StartDate,
                EndDate = getPeriodBalanceRequest.EndDate
            };

            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
    }
}