using System.Globalization;
using System.Security.Claims;
using FoodTracker.Infrastructure.Identity;
using FoodTracker.Web.Contracts;
using FoodTracker.Web.Security;
using FoodTracker.Web.Options;
using FoodTracker.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FoodTracker.Web.Controllers.Api;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private const string TelegramLoginProvider = "Telegram";
    private static readonly TimeSpan TelegramInitDataMaxAge = TimeSpan.FromHours(24);

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthTokenService _authTokenService;
    private readonly IOptions<TelegramAuthOptions> _telegramOptions;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        IAuthTokenService authTokenService,
        IOptions<TelegramAuthOptions> telegramOptions)
    {
        _userManager = userManager;
        _authTokenService = authTokenService;
        _telegramOptions = telegramOptions;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthTokensResponse>> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        var create = await _userManager.CreateAsync(user, request.Password).ConfigureAwait(false);
        if (!create.Succeeded)
        {
            return BadRequest(new { message = string.Join(' ', create.Errors.Select(e => e.Description)) });
        }

        await _userManager.AddToRoleAsync(user, "user").ConfigureAwait(false);
        var tokens = await _authTokenService
            .IssueAsync(user, HttpContext.Connection.RemoteIpAddress?.ToString(), cancellationToken)
            .ConfigureAwait(false);
        return StatusCode(StatusCodes.Status201Created, tokens);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthTokensResponse>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
        if (user is null)
        {
            return Unauthorized(new { message = "Неверный email или пароль." });
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password).ConfigureAwait(false))
        {
            return Unauthorized(new { message = "Неверный email или пароль." });
        }

        var tokens = await _authTokenService
            .IssueAsync(user, HttpContext.Connection.RemoteIpAddress?.ToString(), cancellationToken)
            .ConfigureAwait(false);
        return Ok(tokens);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthTokensResponse>> Refresh(
        [FromBody] RefreshRequest request,
        CancellationToken cancellationToken)
    {
        var tokens = await _authTokenService
            .RefreshAsync(request.RefreshToken, HttpContext.Connection.RemoteIpAddress?.ToString(), cancellationToken)
            .ConfigureAwait(false);

        if (tokens is null)
        {
            return Unauthorized(new { message = "Недействительный refresh-токен." });
        }

        return Ok(tokens);
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest? request, CancellationToken cancellationToken)
    {
        if (User.Identity?.IsAuthenticated == true
            && Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            await _authTokenService.RevokeAllForUserAsync(userId, cancellationToken).ConfigureAwait(false);
        }

        if (!string.IsNullOrWhiteSpace(request?.RefreshToken))
        {
            await _authTokenService.RevokeRefreshAsync(request.RefreshToken, cancellationToken).ConfigureAwait(false);
        }

        return NoContent();
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<AuthUserDto>> Me(CancellationToken cancellationToken)
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(sub) || !Guid.TryParse(sub, out var id))
        {
            return Unauthorized();
        }

        var user = await _userManager.FindByIdAsync(sub).ConfigureAwait(false);
        if (user is null)
        {
            return Unauthorized();
        }

        var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        return Ok(
            new AuthUserDto
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName,
                Role = roles.FirstOrDefault() ?? "user",
                AvatarUrl = user.AvatarUrl,
            });
    }

    /// <summary>Вход из Telegram Mini App: проверка <c>initData</c>, выдача JWT.</summary>
    [HttpPost("telegram")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthTokensResponse>> Telegram(
        [FromBody] TelegramAuthRequest request,
        CancellationToken cancellationToken)
    {
        var botToken = _telegramOptions.Value.BotToken;
        if (string.IsNullOrWhiteSpace(botToken) || botToken.Contains("PASTE", StringComparison.OrdinalIgnoreCase))
        {
            return StatusCode(
                StatusCodes.Status503ServiceUnavailable,
                new { message = "Telegram BotToken не настроен." });
        }

        if (!TelegramMiniAppInitDataParser.TryValidate(
                request.InitData,
                botToken,
                TelegramInitDataMaxAge,
                out var fields))
        {
            return Unauthorized(new { message = "Неверная подпись initData." });
        }

        if (!TelegramMiniAppInitDataParser.TryGetTelegramUserId(fields!, out var telegramUserId))
        {
            return BadRequest(new { message = "В initData нет user.id." });
        }

        var key = telegramUserId.ToString(CultureInfo.InvariantCulture);
        var existingUser = await _userManager.FindByLoginAsync(TelegramLoginProvider, key).ConfigureAwait(false);
        if (existingUser is not null)
        {
            var tokens = await _authTokenService
                .IssueAsync(existingUser, HttpContext.Connection.RemoteIpAddress?.ToString(), cancellationToken)
                .ConfigureAwait(false);
            return Ok(tokens);
        }

        TelegramMiniAppInitDataParser.TryFillNamesFromUserJson(fields!, out var fn, out var ln);
        var user = new ApplicationUser
        {
            UserName = $"tg_{key}",
            Email = null,
            FirstName = fn,
            LastName = ln,
        };

        var create = await _userManager.CreateAsync(user).ConfigureAwait(false);
        if (!create.Succeeded)
        {
            return BadRequest(new { message = string.Join(' ', create.Errors.Select(e => e.Description)) });
        }

        await _userManager.AddToRoleAsync(user, "user").ConfigureAwait(false);
        var addLogin = await _userManager
            .AddLoginAsync(user, new UserLoginInfo(TelegramLoginProvider, key, TelegramLoginProvider))
            .ConfigureAwait(false);

        if (!addLogin.Succeeded)
        {
            await _userManager.DeleteAsync(user).ConfigureAwait(false);
            return BadRequest(new { message = string.Join(' ', addLogin.Errors.Select(e => e.Description)) });
        }

        var issued = await _authTokenService
            .IssueAsync(user, HttpContext.Connection.RemoteIpAddress?.ToString(), cancellationToken)
            .ConfigureAwait(false);
        return StatusCode(StatusCodes.Status201Created, issued);
    }

    /// <summary>Привязать Telegram к уже залогиненному аккаунту.</summary>
    [HttpPost("link/telegram")]
    [Authorize]
    public async Task<IActionResult> LinkTelegram(
        [FromBody] TelegramAuthRequest request,
        CancellationToken cancellationToken)
    {
        var botToken = _telegramOptions.Value.BotToken;
        if (string.IsNullOrWhiteSpace(botToken) || botToken.Contains("PASTE", StringComparison.OrdinalIgnoreCase))
        {
            return StatusCode(
                StatusCodes.Status503ServiceUnavailable,
                new { message = "Telegram BotToken не настроен." });
        }

        if (!TelegramMiniAppInitDataParser.TryValidate(
                request.InitData,
                botToken,
                TelegramInitDataMaxAge,
                out var fields))
        {
            return Unauthorized(new { message = "Неверная подпись initData." });
        }

        if (!TelegramMiniAppInitDataParser.TryGetTelegramUserId(fields!, out var telegramUserId))
        {
            return BadRequest(new { message = "В initData нет user.id." });
        }

        var key = telegramUserId.ToString(CultureInfo.InvariantCulture);
        var other = await _userManager.FindByLoginAsync(TelegramLoginProvider, key).ConfigureAwait(false);
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(sub) || !Guid.TryParse(sub, out var myId))
        {
            return Unauthorized();
        }

        if (other is not null && other.Id != myId)
        {
            return Conflict(new { message = "Этот Telegram уже привязан к другому аккаунту." });
        }

        if (other is not null && other.Id == myId)
        {
            return Ok(new { message = "Уже привязано." });
        }

        var user = await _userManager.FindByIdAsync(sub).ConfigureAwait(false);
        if (user is null)
        {
            return Unauthorized();
        }

        var add = await _userManager
            .AddLoginAsync(user, new UserLoginInfo(TelegramLoginProvider, key, TelegramLoginProvider))
            .ConfigureAwait(false);

        if (!add.Succeeded)
        {
            return BadRequest(new { message = string.Join(' ', add.Errors.Select(e => e.Description)) });
        }

        return NoContent();
    }
}
