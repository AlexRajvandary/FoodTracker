using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class DeleteFoodItemCommand : IRequest<Result>
{
    public Guid UserId { get; init; }
    public Guid FoodItemId { get; init; }
}
