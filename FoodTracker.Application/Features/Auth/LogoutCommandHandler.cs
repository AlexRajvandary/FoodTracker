using FoodTracker.Application.Abstractions.Services;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IAuthAccountService _auth;

    public LogoutCommandHandler(IAuthAccountService auth)
    {
        _auth = auth;
    }

    public Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken) =>
        _auth.LogoutAsync(request, cancellationToken);
}
