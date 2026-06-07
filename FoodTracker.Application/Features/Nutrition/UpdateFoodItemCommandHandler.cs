using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class UpdateFoodItemCommandHandler : IRequestHandler<UpdateFoodItemCommand, Result<FoodItemDto>>
{
    private readonly IFoodItemRepository _foodItemsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFoodItemCommandHandler(IFoodItemRepository foodItemsRepository, IUnitOfWork unitOfWork)
    {
        _foodItemsRepository = foodItemsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<FoodItemDto>> Handle(UpdateFoodItemCommand command, CancellationToken cancellationToken)
    {
        var foodItem = await _foodItemsRepository
            .GetByIdAsync(command.FoodItemId, cancellationToken)
            .ConfigureAwait(false);

        if (foodItem is null)
        {
            return Result<FoodItemDto>.Failure(new Error(FoodErrorCodes.FoodItemNotFound, "Продукт не найден."));
        }

        //if (foodItem.OwnerUserId != command.UserId)
        //{
        //    return Result<FoodItemDto>.Failure(new Error(FoodErrorCodes.FoodItemForbidden, "Доступ к продукту запрещен."));
        //}

        foodItem.Name = command.Name.Trim();
        foodItem.Description = string.IsNullOrWhiteSpace(command.Description)
            ? null
            : command.Description.Trim();

        foodItem.CaloriesPer100g = command.CaloriesPer100g;
        foodItem.ProteinsPer100g = command.ProteinsPer100g;
        foodItem.FatsPer100g = command.FatsPer100g;
        foodItem.CarbsPer100g = command.CarbsPer100g;
        foodItem.PortionGrams = command.PortionGrams;
        foodItem.Category = command.Category;

        await _unitOfWork
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result<FoodItemDto>.Success(foodItem.ToDto());
    }
}