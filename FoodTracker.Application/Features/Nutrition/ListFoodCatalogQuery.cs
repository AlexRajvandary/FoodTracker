using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodCatalogQuery : IRequest<Result<IReadOnlyList<FoodCatalogItemDto>>>
{
    public string? Q { get; init; }
    public string? Category { get; init; }
}
