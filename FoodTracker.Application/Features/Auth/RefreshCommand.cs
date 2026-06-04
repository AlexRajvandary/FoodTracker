using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public record RefreshCommand : IRequest<Result<AuthTokensDto>>
{
    public required string RefreshToken { get; init; }
}
