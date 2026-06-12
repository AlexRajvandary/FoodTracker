using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodCategoriesQueryHandler : IRequestHandler<ListFoodCategoriesQuery, Result<IReadOnlyList<FoodCategoryWithItemsCountDto>>>
{
    private readonly IFoodItemRepository _foodItemRepository;

    public ListFoodCategoriesQueryHandler(IFoodItemRepository foodItemRepository)
    {
        _foodItemRepository = foodItemRepository;
    }

    public async Task<Result<IReadOnlyList<FoodCategoryWithItemsCountDto>>> Handle(ListFoodCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _foodItemRepository.ListCategoriesWithItemsCountAsync(cancellationToken).ConfigureAwait(false);
        return Result<IReadOnlyList<FoodCategoryWithItemsCountDto>>.Success(categories);
    }
}