using FoodTracker.Application.Abstractions.Services;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, Result<AuthTokensDto>>
{
    private readonly IAuthAccountService _auth;

    public RefreshCommandHandler(IAuthAccountService auth)
    {
        _auth = auth;
    }

    public Task<Result<AuthTokensDto>> Handle(RefreshCommand request, CancellationToken cancellationToken) =>
        _auth.RefreshAsync(request, cancellationToken);
}
