using FoodTracker.Application.Abstractions.Services;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthTokensDto>>
{
    private readonly IAuthAccountService _auth;

    public LoginCommandHandler(IAuthAccountService auth)
    {
        _auth = auth;
    }

    public Task<Result<AuthTokensDto>> Handle(LoginCommand request, CancellationToken cancellationToken) =>
        _auth.LoginAsync(request, cancellationToken);
}
