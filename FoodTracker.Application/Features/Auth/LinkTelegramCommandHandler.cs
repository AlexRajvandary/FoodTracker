using FoodTracker.Application.Abstractions;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public class LinkTelegramCommandHandler : IRequestHandler<LinkTelegramCommand, Result>
{
    private readonly IAuthAccountService _auth;

    public LinkTelegramCommandHandler(IAuthAccountService auth)
    {
        _auth = auth;
    }

    public Task<Result> Handle(LinkTelegramCommand request, CancellationToken cancellationToken) =>
        _auth.LinkTelegramAsync(request, cancellationToken);
}
