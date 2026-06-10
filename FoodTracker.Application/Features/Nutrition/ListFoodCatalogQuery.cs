using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodCatalogQuery : IRequest<Result<PagedList<ShortFoodItemDto>>>
{
    public string? Brand { get; init; }
    public IReadOnlyList<Guid> CategoryIds { get; set; } = [];
    public int Page { get; init; }
    public int PageSize { get; init; }
    public string? Query { get; init; }
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; }
}