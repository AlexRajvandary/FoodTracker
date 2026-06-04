using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodCatalogQueryHandler : IRequestHandler<ListFoodCatalogQuery, Result<PagedList<ShortFoodItemDto>>>
{
    private static readonly HashSet<string> AllowedCategories =
    [
        "напитки",
        "гарниры",
        "мясо",
        "сладкое",
        "выпечка",
        "фрукты",
        "овощи",
    ];

    private readonly IFoodItemRepository _foodItems;

    public ListFoodCatalogQueryHandler(IFoodItemRepository items)
    {
        _foodItems = items;
    }

    public async Task<Result<PagedList<ShortFoodItemDto>>> Handle(ListFoodCatalogQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Category) && !AllowedCategories.Contains(request.Category))
        {
            return Result<PagedList<ShortFoodItemDto>>.Failure(
                new Error(FoodErrorCodes.InvalidCategory, "Неизвестная категория."));
        }

        var list = await _foodItems.ListCatalogAsync(request.Query, request.Category, request.Page ?? 1, request.PageSize ?? 10, cancellationToken).ConfigureAwait(false);
        var dto = list.Select(x => x.ToShortDto()).ToList();
        return Result<PagedList<ShortFoodItemDto>>.Success(new PagedList<ShortFoodItemDto>(dto, request.Page ?? 1, request.PageSize ?? 10));
    }
}