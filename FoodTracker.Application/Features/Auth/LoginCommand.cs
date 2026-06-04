using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public record LoginCommand : IRequest<Result<AuthTokensDto>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
