using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public record GetMeQuery : IRequest<Result<AuthUserDto>>
{
    public required Guid UserId { get; init; }
}
