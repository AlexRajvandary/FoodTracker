using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodCatalogQuery : IRequest<Result<PagedList<ShortFoodItemDto>>>
{
    public string? Query { get; init; }
    public string? Category { get; init; }
    public int? Page { get; init; }
    public int? PageSize { get; init; }
}