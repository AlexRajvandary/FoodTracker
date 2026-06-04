using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodCatalogQueryHandler : IRequestHandler<ListFoodCatalogQuery, Result<IReadOnlyList<FoodItemDto>>>
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

    public async Task<Result<IReadOnlyList<FoodItemDto>>> Handle(ListFoodCatalogQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Category) && !AllowedCategories.Contains(request.Category))
        {
            return Result<IReadOnlyList<FoodItemDto>>.Failure(
                new Error(FoodErrorCodes.InvalidCategory, "Неизвестная категория."));
        }

        var list = await _foodItems.ListCatalogAsync(request.Query, request.Category, cancellationToken).ConfigureAwait(false);
        var dto = list.Select(x => x.ToDto()).ToList();
        return Result<IReadOnlyList<FoodItemDto>>.Success(dto);
    }
}