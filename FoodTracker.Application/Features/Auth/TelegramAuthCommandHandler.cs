using FoodTracker.Application.Abstractions.Services;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public class TelegramAuthCommandHandler : IRequestHandler<TelegramAuthCommand, Result<AuthTokensDto>>
{
    private readonly IAuthAccountService _auth;

    public TelegramAuthCommandHandler(IAuthAccountService auth)
    {
        _auth = auth;
    }

    public Task<Result<AuthTokensDto>> Handle(TelegramAuthCommand request, CancellationToken cancellationToken) =>
        _auth.TelegramSignInAsync(request, cancellationToken);
}
