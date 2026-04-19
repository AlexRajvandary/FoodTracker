using FoodTracker.Application.Features.Auth;
using FoodTracker.Domain.Common.Results;

namespace FoodTracker.Application.Abstractions;

public interface IAuthAccountService
{
    Task<Result<AuthTokensDto>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken);

    Task<Result<AuthTokensDto>> LoginAsync(LoginCommand command, CancellationToken cancellationToken);

    Task<Result<AuthTokensDto>> RefreshAsync(RefreshCommand command, CancellationToken cancellationToken);

    Task<Result> LogoutAsync(LogoutCommand command, CancellationToken cancellationToken);

    Task<Result<AuthUserDto>> GetMeAsync(GetMeQuery query, CancellationToken cancellationToken);

    Task<Result<AuthTokensDto>> TelegramSignInAsync(TelegramAuthCommand command, CancellationToken cancellationToken);

    Task<Result> LinkTelegramAsync(LinkTelegramCommand command, CancellationToken cancellationToken);
}
