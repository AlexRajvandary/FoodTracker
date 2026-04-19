using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public record TelegramAuthCommand : IRequest<Result<AuthTokensDto>>
{
    public required string InitData { get; init; }
}
