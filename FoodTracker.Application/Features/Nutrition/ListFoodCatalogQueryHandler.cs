using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodCatalogQueryHandler : IRequestHandler<ListFoodCatalogQuery, Result<PagedList<ShortFoodItemDto>>>
{
    private readonly IFoodItemRepository _foodItemsRepository;

    public ListFoodCatalogQueryHandler(IFoodItemRepository foodItems)
    {
        _foodItemsRepository = foodItems;
    }

    public async Task<Result<PagedList<ShortFoodItemDto>>> Handle(ListFoodCatalogQuery request, CancellationToken cancellationToken)
    {
        var foodItemsPagedQueryResult = await _foodItemsRepository
            .ListCatalogAsync(
                request.CategoryIds,
                request.Query,
                request.SortBy,
                request.SortDescending,
                request.Page,
                request.PageSize,
                cancellationToken)
            .ConfigureAwait(false);

        var foodItemsDtos = foodItemsPagedQueryResult.Items.Select(fi => fi.ToShortDto()).ToList();

        var pagedList = new PagedList<ShortFoodItemDto>
        (
            foodItemsDtos,
            request.Page,
            request.PageSize,
            foodItemsPagedQueryResult.TotalCount
        );

        return Result<PagedList<ShortFoodItemDto>>.Success(pagedList);
    }
}