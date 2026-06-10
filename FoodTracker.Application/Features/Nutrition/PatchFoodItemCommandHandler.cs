using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using FoodTracker.Domain.Nutrition;
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
            return Result<FoodItemDto>.Failure(
                new Error(FoodErrorCodes.FoodItemNotFound, "Продукт не найден."));
        }

        if (!string.IsNullOrWhiteSpace(command.Barcode))
        {
            foodItem.Barcode = command.Barcode.Trim();
        }

        if (command.Brand is not null)
        {
            foodItem.Brand = string.IsNullOrWhiteSpace(command.Brand)
                ? null
                : command.Brand.Trim();
        }

        if (command.CaloriesPer100g.HasValue)
        {
            foodItem.CaloriesPer100g = command.CaloriesPer100g.Value;
        }

        if (command.CarbsPer100g.HasValue)
        {
            foodItem.CarbsPer100g = command.CarbsPer100g.Value;
        }

        if (command.Description is not null)
        {
            foodItem.Description = string.IsNullOrWhiteSpace(command.Description)
                ? null
                : command.Description.Trim();
        }

        if (command.FatsPer100g.HasValue)
        {
            foodItem.FatsPer100g = command.FatsPer100g.Value;
        }

        if (command.FiberPer100g.HasValue)
        {
            foodItem.FiberPer100g = command.FiberPer100g.Value;
        }

        if (command.ImageUrl is not null)
        {
            foodItem.ImageUrl = string.IsNullOrWhiteSpace(command.ImageUrl)
                ? null
                : command.ImageUrl.Trim();
        }

        if (command.Name is not null)
        {
            foodItem.Name = command.Name.Trim();
        }

        if (command.ProteinsPer100g.HasValue)
        {
            foodItem.ProteinsPer100g = command.ProteinsPer100g.Value;
        }

        if (command.SaltPer100g.HasValue)
        {
            foodItem.SaltPer100g = command.SaltPer100g.Value;
        }

        if (command.SaturatedFatPer100g.HasValue)
        {
            foodItem.SaturatedFatPer100g = command.SaturatedFatPer100g.Value;
        }

        if (command.ServingSizeGrams.HasValue)
        {
            foodItem.ServingSizeGrams = command.ServingSizeGrams.Value;
        }

        if (command.SugarsPer100g.HasValue)
        {
            foodItem.SugarsPer100g = command.SugarsPer100g.Value;
        }

        if (command.CategoryIds is not null)
        {
            foodItem.FoodItemCategories.Clear();

            foreach (var categoryId in command.CategoryIds.Distinct())
            {
                foodItem.FoodItemCategories.Add(new FoodItemCategory
                {
                    FoodItemId = foodItem.Id,
                    FoodCategoryId = categoryId,
                });
            }
        }

        if (command.NewCategoryNames is not null)
        {
            var newCategoryNames = command.NewCategoryNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (var categoryName in newCategoryNames)
            {
                var category = await _foodItemsRepository
                    .GetCategoryByNameAsync(categoryName, cancellationToken)
                    .ConfigureAwait(false);

                if (category is null)
                {
                    category = new FoodCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = categoryName,
                        CreatedAtUtc = DateTime.UtcNow,
                    };

                    await _foodItemsRepository
                        .CreateCategoryAsync(category, cancellationToken)
                        .ConfigureAwait(false);
                }

                if (foodItem.FoodItemCategories.All(x => x.FoodCategoryId != category.Id))
                {
                    foodItem.FoodItemCategories.Add(new FoodItemCategory
                    {
                        FoodItemId = foodItem.Id,
                        FoodCategoryId = category.Id,
                    });
                }
            }
        }

        foodItem.UpdatedAtUtc = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return Result<FoodItemDto>.Success(foodItem.ToDto());
    }
}