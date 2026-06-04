using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class DeleteFoodItemCommandHandler : IRequestHandler<DeleteFoodItemCommand, Result>
{
    private readonly IFoodItemRepository _foodItems;

    public DeleteFoodItemCommandHandler(IFoodItemRepository foodItems)
    {
        _foodItems = foodItems;
    }

    public async Task<Result> Handle(DeleteFoodItemCommand request, CancellationToken cancellationToken)
    {
        var foodItem = await _foodItems.GetByIdAsync(request.FoodItemId, cancellationToken).ConfigureAwait(false);
        if (foodItem is null)
        {
            return Result.Failure(new Error(FoodErrorCodes.EntryNotFound, "Запись не найдена."));
        }

        await _foodItems.DeleteAsync(foodItem, cancellationToken).ConfigureAwait(false);
        return Result.Success();
    }
}