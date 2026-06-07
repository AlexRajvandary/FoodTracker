using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class GetFoodItemQueryHandler : IRequestHandler<GetFoodItemQuery, Result<FoodItemDto>>
{
    private readonly IFoodItemRepository _foodItems;

    public GetFoodItemQueryHandler(IFoodItemRepository items)
    {
        _foodItems = items;
    }

    public async Task<Result<FoodItemDto>> Handle(GetFoodItemQuery request, CancellationToken cancellationToken)
    {
        var foodItem = await _foodItems.GetByIdAsNoTrackingAsync(request.FoodItemId, cancellationToken).ConfigureAwait(false);
        if (foodItem == null)
        {
            return Result<FoodItemDto>.Failure(new Error(FoodErrorCodes.EntryNotFound, "Неизвестный продукт."));
        }

        return Result<FoodItemDto>.Success(foodItem.ToDto());
    }
}