using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Auth;

public record LogoutCommand : IRequest<Result>
{
    public Guid? AuthenticatedUserId { get; init; }
    public string? RefreshToken { get; init; }
}
