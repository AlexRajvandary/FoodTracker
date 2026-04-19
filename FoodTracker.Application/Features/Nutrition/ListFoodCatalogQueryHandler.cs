using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodCatalogQueryHandler : IRequestHandler<ListFoodCatalogQuery, Result<IReadOnlyList<FoodCatalogItemDto>>>
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

    private readonly IFoodItemRepository _items;

    public ListFoodCatalogQueryHandler(IFoodItemRepository items)
    {
        _items = items;
    }

    public async Task<Result<IReadOnlyList<FoodCatalogItemDto>>> Handle(ListFoodCatalogQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Category) && !AllowedCategories.Contains(request.Category))
        {
            return Result<IReadOnlyList<FoodCatalogItemDto>>.Failure(
                new Error(FoodErrorCodes.InvalidCategory, "Неизвестная категория."));
        }

        var list = await _items.ListCatalogAsync(request.Q, request.Category, cancellationToken).ConfigureAwait(false);
        var dto = list.Where(x => !string.IsNullOrEmpty(x.Category)).Select(x => x.ToCatalogDto()).ToList();
        return Result<IReadOnlyList<FoodCatalogItemDto>>.Success(dto);
    }
}
