using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class SearchFoodItemsQuery : IRequest<Result<IReadOnlyList<FoodItemDto>>>
{
    public required string Q { get; init; }
}
