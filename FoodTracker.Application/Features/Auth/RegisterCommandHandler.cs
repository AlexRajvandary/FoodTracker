using FoodTracker.Application.Abstractions;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthTokensDto>>
{
    private readonly IAuthAccountService _auth;

    public RegisterCommandHandler(IAuthAccountService auth)
    {
        _auth = auth;
    }

    public Task<Result<AuthTokensDto>> Handle(RegisterCommand request, CancellationToken cancellationToken) =>
        _auth.RegisterAsync(request, cancellationToken);
}
