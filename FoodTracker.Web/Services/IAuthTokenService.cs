using FoodTracker.Infrastructure.Identity;
using FoodTracker.Web.Contracts;

namespace FoodTracker.Web.Services;

public interface IAuthTokenService
{
    Task<AuthTokensResponse> IssueAsync(ApplicationUser user, string? clientIp, CancellationToken cancellationToken);

    Task<AuthTokensResponse?> RefreshAsync(string refreshToken, string? clientIp, CancellationToken cancellationToken);

    Task RevokeRefreshAsync(string refreshToken, CancellationToken cancellationToken);

    Task RevokeAllForUserAsync(Guid userId, CancellationToken cancellationToken);
}
