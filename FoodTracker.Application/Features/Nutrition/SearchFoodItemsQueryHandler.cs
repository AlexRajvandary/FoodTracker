using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class SearchFoodItemsQueryHandler : IRequestHandler<SearchFoodItemsQuery, Result<IReadOnlyList<FoodItemDto>>>
{
    private readonly IFoodItemRepository _items;

    public SearchFoodItemsQueryHandler(IFoodItemRepository items)
    {
        _items = items;
    }

    public async Task<Result<IReadOnlyList<FoodItemDto>>> Handle(SearchFoodItemsQuery request, CancellationToken cancellationToken)
    {
        var list = await _items.SearchByNameAsync(request.Q, cancellationToken).ConfigureAwait(false);
        return Result<IReadOnlyList<FoodItemDto>>.Success(list.Select(x => x.ToDto()).ToList());
    }
}
