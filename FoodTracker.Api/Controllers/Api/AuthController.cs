using System.Security.Claims;
using FoodTracker.Application.Features.Auth;
using FoodTracker.Api.Contracts;
using FoodTracker.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracker.Api.Controllers.Api;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToActionResult(t => new ObjectResult(t) { StatusCode = StatusCodes.Status201Created });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToActionResult();
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] RefreshCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToActionResult();
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout([FromBody] LogoutRequestBody? body, CancellationToken cancellationToken)
    {
        Guid? userId = null;
        if (User.Identity?.IsAuthenticated == true
            && Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var parsed))
        {
            userId = parsed;
        }

        var command = new LogoutCommand { AuthenticatedUserId = userId, RefreshToken = body?.RefreshToken };
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToActionResult();
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id))
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(new GetMeQuery { UserId = id }, cancellationToken).ConfigureAwait(false);
        return result.ToActionResult();
    }

    [HttpPost("telegram")]
    [AllowAnonymous]
    public async Task<IActionResult> Telegram([FromBody] TelegramAuthCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return result.ToActionResult();
    }

    [HttpPost("link/telegram")]
    [Authorize]
    public async Task<IActionResult> LinkTelegram([FromBody] TelegramAuthCommand body, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id))
        {
            return Unauthorized();
        }

        var result = await _mediator
            .Send(new LinkTelegramCommand { UserId = id, InitData = body.InitData }, cancellationToken)
            .ConfigureAwait(false);
        return result.ToActionResult();
    }
}
