using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using FoodTracker.Domain.Nutrition;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class CreateFoodItemCommandHandler : IRequestHandler<CreateFoodItemCommand, Result<FoodItemDto>>
{
    private readonly IFoodItemRepository _foodItemsRepository;

    public CreateFoodItemCommandHandler(IFoodItemRepository foodItemsRepository)
    {
        _foodItemsRepository = foodItemsRepository;
    }

    public async Task<Result<FoodItemDto>> Handle(CreateFoodItemCommand command, CancellationToken cancellationToken)
    {
        var barcode = string.IsNullOrWhiteSpace(command.Barcode) ? null : command.Barcode.Trim();

        if (barcode is not null)
        {
            var existingFoodItem = await _foodItemsRepository
                .GetByBarcodeAsync(barcode, cancellationToken)
                .ConfigureAwait(false);

            if (existingFoodItem is not null)
            {
                return Result<FoodItemDto>.Success(existingFoodItem.ToDto());
            }
        }

        var foodItem = new FoodItem
        {
            Id = Guid.NewGuid(),
            Barcode = string.IsNullOrWhiteSpace(command.Barcode) ? null : command.Barcode.Trim(),
            Brand = string.IsNullOrWhiteSpace(command.Brand) ? null : command.Brand.Trim(),
            CaloriesPer100g = command.CaloriesPer100g,
            CarbsPer100g = command.CarbsPer100g ?? 0,
            CreatedAtUtc = DateTime.UtcNow,
            Description = string.IsNullOrWhiteSpace(command.Description) ? null : command.Description.Trim(),
            FatsPer100g = command.FatsPer100g ?? 0,
            FiberPer100g = command.FiberPer100g,
            ImageUrl = string.IsNullOrWhiteSpace(command.ImageUrl) ? null : command.ImageUrl.Trim(),
            Name = command.Name.Trim(),
            OwnerUserId = command.UserId,
            ProteinsPer100g = command.ProteinsPer100g ?? 0,
            SaltPer100g = command.SaltPer100g,
            SaturatedFatPer100g = command.SaturatedFatPer100g,
            ServingSizeGrams = command.ServingSizeGrams,
            SugarsPer100g = command.SugarsPer100g,
        };

        foreach (var categoryId in command.CategoryIds.Distinct())
        {
            foodItem.FoodItemCategories.Add(new FoodItemCategory
            {
                FoodItemId = foodItem.Id,
                FoodCategoryId = categoryId,
            });
        }

        foreach (var countryId in command.CountryIds.Distinct())
        {
            foodItem.FoodItemCountries.Add(new FoodItemCountry
            {
                FoodItemId = foodItem.Id,
                CountryId = countryId
            });
        }

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

            foodItem.FoodItemCategories.Add(new FoodItemCategory
            {
                FoodItemId = foodItem.Id,
                FoodCategoryId = category.Id,
            });
        }

        await _foodItemsRepository
            .CreateAsync(foodItem, cancellationToken)
            .ConfigureAwait(false);

        return Result<FoodItemDto>.Success(foodItem.ToDto());
    }
}