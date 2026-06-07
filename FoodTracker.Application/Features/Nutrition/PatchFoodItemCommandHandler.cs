using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class PatchFoodItemCommandHandler : IRequestHandler<PatchFoodItemCommand, Result<FoodItemDto>>
{
    private readonly IFoodItemRepository _foodItemsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PatchFoodItemCommandHandler(IFoodItemRepository foodItemsRepository, IUnitOfWork unitOfWork)
    {
        _foodItemsRepository = foodItemsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<FoodItemDto>> Handle(PatchFoodItemCommand command, CancellationToken cancellationToken)
    {
        var foodItem = await _foodItemsRepository
            .GetByIdAsync(command.FoodItemId, cancellationToken)
            .ConfigureAwait(false);

        if (foodItem is null)
        {
            return Result<FoodItemDto>.Failure(new Error(FoodErrorCodes.FoodItemNotFound, "Продукт не найден."));
        }

        if (command.Name is not null)
        {
            foodItem.Name = command.Name.Trim();
        }

        if (command.Description is not null)
        {
            foodItem.Description = string.IsNullOrWhiteSpace(command.Description)
                ? null
                : command.Description.Trim();
        }

        if (command.CaloriesPer100g.HasValue)
        {
            foodItem.CaloriesPer100g = command.CaloriesPer100g.Value;
        }

        if (command.ProteinsPer100g.HasValue)
        {
            foodItem.ProteinsPer100g = command.ProteinsPer100g.Value;
        }

        if (command.FatsPer100g.HasValue)
        {
            foodItem.FatsPer100g = command.FatsPer100g.Value;
        }

        if (command.CarbsPer100g.HasValue)
        {
            foodItem.CarbsPer100g = command.CarbsPer100g.Value;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Result<FoodItemDto>.Success(foodItem.ToDto());
    }
}