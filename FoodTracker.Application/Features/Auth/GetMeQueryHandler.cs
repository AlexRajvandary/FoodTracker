using FoodTracker.Application.Abstractions.Services;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public class GetMeQueryHandler : IRequestHandler<GetMeQuery, Result<AuthUserDto>>
{
    private readonly IAuthAccountService _auth;

    public GetMeQueryHandler(IAuthAccountService auth)
    {
        _auth = auth;
    }

    public Task<Result<AuthUserDto>> Handle(GetMeQuery request, CancellationToken cancellationToken) =>
        _auth.GetMeAsync(request, cancellationToken);
}
