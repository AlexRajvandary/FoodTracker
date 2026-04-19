using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FoodTracker.Application.Abstractions;
using FoodTracker.Application.Configuration;
using FoodTracker.Application.Features.Auth;
using FoodTracker.Domain.Auth;
using FoodTracker.Domain.Common.Results;
using FoodTracker.Infrastructure.Identity;
using FoodTracker.Infrastructure.Persistence;
using FoodTracker.Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FoodTracker.Infrastructure.Services;

public class AuthAccountService : IAuthAccountService
{
    private static readonly TimeSpan TelegramInitDataMaxAge = TimeSpan.FromHours(24);

    private readonly DataContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtOptions _jwt;
    private readonly TelegramAuthOptions _telegram;
    private readonly IHttpContextAccessor? _httpContextAccessor;

    public AuthAccountService(
        DataContext db,
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> jwtOptions,
        IOptions<TelegramAuthOptions> telegramOptions,
        IHttpContextAccessor? httpContextAccessor = null)
    {
        _db = db;
        _userManager = userManager;
        _jwt = jwtOptions.Value;
        _telegram = telegramOptions.Value;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<AuthTokensDto>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = command.Email,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
        };

        var create = await _userManager.CreateAsync(user, command.Password).ConfigureAwait(false);
        if (!create.Succeeded)
        {
            return Result<AuthTokensDto>.Failure(
                new Error(AuthErrorCodes.Identity, string.Join(' ', create.Errors.Select(e => e.Description))));
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "user").ConfigureAwait(false);
        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user).ConfigureAwait(false);
            return Result<AuthTokensDto>.Failure(
                new Error(AuthErrorCodes.Identity, string.Join(' ', roleResult.Errors.Select(e => e.Description))));
        }

        var normalizedEmail = _userManager.NormalizeEmail(command.Email);
        if (string.IsNullOrEmpty(normalizedEmail))
        {
            await _userManager.DeleteAsync(user).ConfigureAwait(false);
            return Result<AuthTokensDto>.Failure(new Error(AuthErrorCodes.Identity, "Не удалось нормализовать email."));
        }

        var providerError = await TryAddAuthProviderExclusiveAsync(
                user.Id,
                AuthProviderKind.EmailPassword,
                normalizedEmail,
                cancellationToken)
            .ConfigureAwait(false);

        if (providerError is not null)
        {
            await _userManager.DeleteAsync(user).ConfigureAwait(false);
            return Result<AuthTokensDto>.Failure(providerError);
        }

        var tokens = await IssueTokensAsync(user, cancellationToken).ConfigureAwait(false);
        return Result<AuthTokensDto>.Success(tokens);
    }

    public async Task<Result<AuthTokensDto>> LoginAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email).ConfigureAwait(false);
        if (user is null
            || !await _userManager.CheckPasswordAsync(user, command.Password).ConfigureAwait(false))
        {
            return Result<AuthTokensDto>.Failure(
                new Error(AuthErrorCodes.Unauthorized, "Неверный email или пароль."));
        }

        var tokens = await IssueTokensAsync(user, cancellationToken).ConfigureAwait(false);
        return Result<AuthTokensDto>.Success(tokens);
    }

    public async Task<Result<AuthTokensDto>> RefreshAsync(RefreshCommand command, CancellationToken cancellationToken)
    {
        var tokens = await TryRefreshTokensAsync(command.RefreshToken, cancellationToken).ConfigureAwait(false);
        if (tokens is null)
        {
            return Result<AuthTokensDto>.Failure(
                new Error(AuthErrorCodes.Unauthorized, "Недействительный refresh-токен."));
        }

        return Result<AuthTokensDto>.Success(tokens);
    }

    public async Task<Result> LogoutAsync(LogoutCommand command, CancellationToken cancellationToken)
    {
        if (command.AuthenticatedUserId is { } uid)
        {
            await RevokeAllRefreshForUserAsync(uid, cancellationToken).ConfigureAwait(false);
        }

        if (!string.IsNullOrWhiteSpace(command.RefreshToken))
        {
            await RevokeSingleRefreshAsync(command.RefreshToken, cancellationToken).ConfigureAwait(false);
        }

        return Result.Success();
    }

    public async Task<Result<AuthUserDto>> GetMeAsync(GetMeQuery query, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(query.UserId.ToString()).ConfigureAwait(false);
        if (user is null)
        {
            return Result<AuthUserDto>.Failure(new Error(AuthErrorCodes.NotFound, "Пользователь не найден."));
        }

        var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        return Result<AuthUserDto>.Success(MapUser(user, roles));
    }

    public async Task<Result<AuthTokensDto>> TelegramSignInAsync(
        TelegramAuthCommand command,
        CancellationToken cancellationToken)
    {
        var botError = ValidateTelegramBotConfigured();
        if (botError is not null)
        {
            return Result<AuthTokensDto>.Failure(botError);
        }

        if (!TelegramMiniAppInitDataParser.TryValidate(
                command.InitData,
                _telegram.BotToken,
                TelegramInitDataMaxAge,
                out var fields))
        {
            return Result<AuthTokensDto>.Failure(
                new Error(AuthErrorCodes.TelegramInvalidInitData, "Неверная подпись initData."));
        }

        if (!TelegramMiniAppInitDataParser.TryGetTelegramUserId(fields!, out var telegramUserId))
        {
            return Result<AuthTokensDto>.Failure(
                new Error(AuthErrorCodes.TelegramMissingUser, "В initData нет user.id."));
        }

        var key = telegramUserId.ToString(CultureInfo.InvariantCulture);
        var existingLink = await _db
            .UserAuthProviders.AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.ProviderKind == AuthProviderKind.Telegram && x.ProviderKey == key,
                cancellationToken)
            .ConfigureAwait(false);

        if (existingLink is not null)
        {
            var existingUser = await _userManager.FindByIdAsync(existingLink.UserId.ToString()).ConfigureAwait(false);
            if (existingUser is null)
            {
                return Result<AuthTokensDto>.Failure(new Error(AuthErrorCodes.NotFound, "Пользователь не найден."));
            }

            var tokens = await IssueTokensAsync(existingUser, cancellationToken).ConfigureAwait(false);
            return Result<AuthTokensDto>.Success(tokens);
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
            return Result<AuthTokensDto>.Failure(
                new Error(AuthErrorCodes.Identity, string.Join(' ', create.Errors.Select(e => e.Description))));
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "user").ConfigureAwait(false);
        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user).ConfigureAwait(false);
            return Result<AuthTokensDto>.Failure(
                new Error(AuthErrorCodes.Identity, string.Join(' ', roleResult.Errors.Select(e => e.Description))));
        }

        var providerError = await TryAddAuthProviderExclusiveAsync(user.Id, AuthProviderKind.Telegram, key, cancellationToken)
            .ConfigureAwait(false);
        if (providerError is not null)
        {
            await _userManager.DeleteAsync(user).ConfigureAwait(false);
            return Result<AuthTokensDto>.Failure(providerError);
        }

        var issued = await IssueTokensAsync(user, cancellationToken).ConfigureAwait(false);
        return Result<AuthTokensDto>.Success(issued);
    }

    public async Task<Result> LinkTelegramAsync(LinkTelegramCommand command, CancellationToken cancellationToken)
    {
        var botError = ValidateTelegramBotConfigured();
        if (botError is not null)
        {
            return Result.Failure(botError);
        }

        if (!TelegramMiniAppInitDataParser.TryValidate(
                command.InitData,
                _telegram.BotToken,
                TelegramInitDataMaxAge,
                out var fields))
        {
            return Result.Failure(
                new Error(AuthErrorCodes.TelegramInvalidInitData, "Неверная подпись initData."));
        }

        if (!TelegramMiniAppInitDataParser.TryGetTelegramUserId(fields!, out var telegramUserId))
        {
            return Result.Failure(
                new Error(AuthErrorCodes.TelegramMissingUser, "В initData нет user.id."));
        }

        var key = telegramUserId.ToString(CultureInfo.InvariantCulture);
        var link = await _db
            .UserAuthProviders.AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.ProviderKind == AuthProviderKind.Telegram && x.ProviderKey == key,
                cancellationToken)
            .ConfigureAwait(false);

        if (link is not null)
        {
            if (link.UserId != command.UserId)
            {
                return Result.Failure(
                    new Error(AuthErrorCodes.Conflict, "Этот Telegram уже привязан к другому аккаунту."));
            }

            return Result.Success();
        }

        var user = await _userManager.FindByIdAsync(command.UserId.ToString()).ConfigureAwait(false);
        if (user is null)
        {
            return Result.Failure(new Error(AuthErrorCodes.Unauthorized, "Пользователь не найден."));
        }

        var providerError = await TryAddAuthProviderExclusiveAsync(command.UserId, AuthProviderKind.Telegram, key, cancellationToken)
            .ConfigureAwait(false);
        if (providerError is not null)
        {
            return Result.Failure(providerError);
        }

        return Result.Success();
    }

    private Error? ValidateTelegramBotConfigured()
    {
        if (string.IsNullOrWhiteSpace(_telegram.BotToken)
            || _telegram.BotToken.Contains("PASTE", StringComparison.OrdinalIgnoreCase))
        {
            return new Error(AuthErrorCodes.TelegramNotConfigured, "Telegram BotToken не настроен.");
        }

        return null;
    }

    private async Task<Error?> TryAddAuthProviderExclusiveAsync(
        Guid userId,
        AuthProviderKind kind,
        string providerKey,
        CancellationToken cancellationToken)
    {
        var exists = await _db.UserAuthProviders.AnyAsync(
                x => x.ProviderKind == kind && x.ProviderKey == providerKey,
                cancellationToken)
            .ConfigureAwait(false);
        if (exists)
        {
            return new Error(AuthErrorCodes.Conflict, "Такой провайдер уже привязан к другому аккаунту.");
        }

        _db.UserAuthProviders.Add(
            new UserAuthProvider
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ProviderKind = kind,
                ProviderKey = providerKey,
                CreatedAtUtc = DateTime.UtcNow,
            });
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return null;
    }

    private async Task<AuthTokensDto> IssueTokensAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        var access = CreateAccessToken(user, roles);
        var refreshPlain = CreateOpaqueRefreshToken();
        var refreshHash = Sha256(refreshPlain);

        var refreshDays = _jwt.RefreshTokenDays > 0 ? _jwt.RefreshTokenDays : 14;
        _db.RefreshTokens.Add(
            new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                TokenHash = refreshHash,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(refreshDays),
                CreatedAtUtc = DateTime.UtcNow,
                CreatedByIp = ClientIp(),
            });

        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new AuthTokensDto
        {
            AccessToken = access,
            RefreshToken = refreshPlain,
            User = MapUser(user, roles),
        };
    }

    private async Task<AuthTokensDto?> TryRefreshTokensAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var hash = Sha256(refreshToken);
        var existing = await _db
            .RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == hash, cancellationToken)
            .ConfigureAwait(false);

        if (existing is null || existing.RevokedAtUtc is not null || existing.ExpiresAtUtc < DateTime.UtcNow)
        {
            return null;
        }

        var user = await _userManager.FindByIdAsync(existing.UserId.ToString()).ConfigureAwait(false);
        if (user is null)
        {
            return null;
        }

        existing.RevokedAtUtc = DateTime.UtcNow;

        var refreshDays = _jwt.RefreshTokenDays > 0 ? _jwt.RefreshTokenDays : 14;
        var refreshPlain = CreateOpaqueRefreshToken();
        _db.RefreshTokens.Add(
            new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                TokenHash = Sha256(refreshPlain),
                ExpiresAtUtc = DateTime.UtcNow.AddDays(refreshDays),
                CreatedAtUtc = DateTime.UtcNow,
                CreatedByIp = ClientIp(),
            });

        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        return new AuthTokensDto
        {
            AccessToken = CreateAccessToken(user, roles),
            RefreshToken = refreshPlain,
            User = MapUser(user, roles),
        };
    }

    private async Task RevokeSingleRefreshAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var hash = Sha256(refreshToken);
        var existing = await _db.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == hash, cancellationToken)
            .ConfigureAwait(false);
        if (existing is null || existing.RevokedAtUtc is not null)
        {
            return;
        }

        existing.RevokedAtUtc = DateTime.UtcNow;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private async Task RevokeAllRefreshForUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var tokens = await _db
            .RefreshTokens.Where(x => x.UserId == userId && x.RevokedAtUtc == null)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var now = DateTime.UtcNow;
        foreach (var t in tokens)
        {
            t.RevokedAtUtc = now;
        }

        if (tokens.Count > 0)
        {
            await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    private string? ClientIp() => _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString();

    private string CreateAccessToken(ApplicationUser user, IList<string> roles)
    {
        var minutes = _jwt.AccessTokenMinutes > 0 ? _jwt.AccessTokenMinutes : 60;
        var expires = DateTime.UtcNow.AddMinutes(minutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        if (!string.IsNullOrEmpty(user.Email))
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        }

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static AuthUserDto MapUser(ApplicationUser user, IList<string> roles)
    {
        var role = roles.FirstOrDefault() ?? "user";
        return new AuthUserDto
        {
            Id = user.Id.ToString(),
            Email = user.Email,
            FirstName = user.FirstName ?? string.Empty,
            LastName = user.LastName,
            Role = role,
            AvatarUrl = user.AvatarUrl,
        };
    }

    private static string CreateOpaqueRefreshToken()
    {
        Span<byte> buffer = stackalloc byte[64];
        RandomNumberGenerator.Fill(buffer);
        return Convert.ToBase64String(buffer)
            .Replace("+", "-", StringComparison.Ordinal)
            .Replace("/", "_", StringComparison.Ordinal)
            .TrimEnd('=');
    }

    private static byte[] Sha256(string value) => SHA256.HashData(Encoding.UTF8.GetBytes(value));
}
