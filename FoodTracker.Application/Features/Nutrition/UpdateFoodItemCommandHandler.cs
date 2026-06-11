using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using FoodTracker.Domain.Nutrition;
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
            return Result<FoodItemDto>.Failure(
                new Error(FoodErrorCodes.FoodItemNotFound, "Продукт не найден."));
        }

        if (string.IsNullOrWhiteSpace(command.Barcode))
        {
            return Result<FoodItemDto>.Failure(
                new Error(FoodErrorCodes.MissingBarCode, "Штрих-код обязателен."));
        }

        foodItem.Barcode = command.Barcode.Trim();
        foodItem.Brand = string.IsNullOrWhiteSpace(command.Brand) ? null : command.Brand.Trim();
        foodItem.CaloriesPer100g = command.CaloriesPer100g;
        foodItem.CarbsPer100g = command.CarbsPer100g;
        foodItem.Description = string.IsNullOrWhiteSpace(command.Description) ? null : command.Description.Trim();
        foodItem.FatsPer100g = command.FatsPer100g;
        foodItem.FiberPer100g = command.FiberPer100g;
        foodItem.ImageUrl = string.IsNullOrWhiteSpace(command.ImageUrl) ? null : command.ImageUrl.Trim();
        foodItem.Name = command.Name.Trim();
        foodItem.ProteinsPer100g = command.ProteinsPer100g;
        foodItem.SaltPer100g = command.SaltPer100g;
        foodItem.SaturatedFatPer100g = command.SaturatedFatPer100g;
        foodItem.ServingSizeGrams = command.ServingSizeGrams;
        foodItem.SugarsPer100g = command.SugarsPer100g;
        foodItem.UpdatedAtUtc = DateTime.UtcNow;

        foodItem.FoodItemCategories.Clear();

        foreach (var categoryId in command.CategoryIds.Distinct())
        {
            foodItem.FoodItemCategories.Add(new FoodItemCategory
            {
                FoodItemId = foodItem.Id,
                FoodCategoryId = categoryId,
            });
        }

        foodItem.FoodItemCountries.Clear();

        foreach (var countryId in command.CountryIds.Distinct())
        {
            foodItem.FoodItemCountries.Add(new FoodItemCountry
            {
                FoodItemId = foodItem.Id,
                CountryId = countryId
            });
        }

        foreach (var categoryName in command.NewCategoryNames
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var category = await _foodItemsRepository.GetCategoryByNameAsync(categoryName, cancellationToken).ConfigureAwait(false);

            if (category is null)
            {
                category = new FoodCategory
                {
                    Id = Guid.NewGuid(),
                    Name = categoryName,
                    CreatedAtUtc = DateTime.UtcNow,
                };

                await _foodItemsRepository.CreateCategoryAsync(category, cancellationToken).ConfigureAwait(false);
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

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return Result<FoodItemDto>.Success(foodItem.ToDto());
    }
}