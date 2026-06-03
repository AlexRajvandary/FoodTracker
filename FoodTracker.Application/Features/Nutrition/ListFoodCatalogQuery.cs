using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodCatalogQuery : IRequest<Result<IReadOnlyList<FoodItemDto>>>
{
    public string? Query { get; init; }
    public string? Category { get; init; }
}
