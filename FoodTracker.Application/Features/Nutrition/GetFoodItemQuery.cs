using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class GetFoodItemQuery : IRequest<Result<FoodItemDto>>
{
    public Guid FoodItemId { get; init; }
}