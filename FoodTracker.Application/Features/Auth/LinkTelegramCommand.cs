using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public record LinkTelegramCommand : IRequest<Result>
{
    public required Guid UserId { get; init; }
    public required string InitData { get; init; }
}
