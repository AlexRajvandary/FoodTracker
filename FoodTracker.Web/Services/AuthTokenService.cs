using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FoodTracker.Infrastructure.Identity;
using FoodTracker.Infrastructure.Persistence;
using FoodTracker.Web.Contracts;
using FoodTracker.Web.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FoodTracker.Web.Services;

public sealed class AuthTokenService : IAuthTokenService
{
    private readonly DataContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtOptions _options;

    public AuthTokenService(
        DataContext db,
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> options)
    {
        _db = db;
        _userManager = userManager;
        _options = options.Value;
    }

    public async Task<AuthTokensResponse> IssueAsync(ApplicationUser user, string? clientIp, CancellationToken cancellationToken)
    {
        var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        var access = CreateAccessToken(user, roles);
        var refreshPlain = CreateOpaqueRefreshToken();
        var refreshHash = Sha256(refreshPlain);

        var refreshDays = _options.RefreshTokenDays > 0 ? _options.RefreshTokenDays : 14;
        _db.RefreshTokens.Add(
            new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                TokenHash = refreshHash,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(refreshDays),
                CreatedAtUtc = DateTime.UtcNow,
                CreatedByIp = clientIp,
            });

        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new AuthTokensResponse
        {
            AccessToken = access,
            RefreshToken = refreshPlain,
            User = MapUser(user, roles),
        };
    }

    public async Task<AuthTokensResponse?> RefreshAsync(string refreshToken, string? clientIp, CancellationToken cancellationToken)
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

        var refreshDays = _options.RefreshTokenDays > 0 ? _options.RefreshTokenDays : 14;
        var refreshPlain = CreateOpaqueRefreshToken();
        _db.RefreshTokens.Add(
            new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                TokenHash = Sha256(refreshPlain),
                ExpiresAtUtc = DateTime.UtcNow.AddDays(refreshDays),
                CreatedAtUtc = DateTime.UtcNow,
                CreatedByIp = clientIp,
            });

        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        return new AuthTokensResponse
        {
            AccessToken = CreateAccessToken(user, roles),
            RefreshToken = refreshPlain,
            User = MapUser(user, roles),
        };
    }

    public async Task RevokeRefreshAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var hash = Sha256(refreshToken);
        var existing = await _db.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == hash, cancellationToken).ConfigureAwait(false);
        if (existing is null || existing.RevokedAtUtc is not null)
        {
            return;
        }

        existing.RevokedAtUtc = DateTime.UtcNow;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task RevokeAllForUserAsync(Guid userId, CancellationToken cancellationToken)
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

    private string CreateAccessToken(ApplicationUser user, IList<string> roles)
    {
        var minutes = _options.AccessTokenMinutes > 0 ? _options.AccessTokenMinutes : 60;
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
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

    private static byte[] Sha256(string value)
    {
        return SHA256.HashData(Encoding.UTF8.GetBytes(value));
    }
}
