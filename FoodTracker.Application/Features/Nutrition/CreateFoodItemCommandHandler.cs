using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using FoodTracker.Domain.Nutrition;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class CreateFoodItemCommandHandler : IRequestHandler<CreateFoodItemCommand, Result<FoodItemDto>>
{
    private readonly IFoodItemRepository _foodItemsRepository;

    public CreateFoodItemCommandHandler(IFoodItemRepository items)
    {
        _foodItemsRepository = items;
    }

    public async Task<Result<FoodItemDto>> Handle(CreateFoodItemCommand command, CancellationToken cancellationToken)
    {
        var foodItem = new FoodItem
        {
            Id = Guid.NewGuid(),
            Name = command.Name.Trim(),
            Description = string.IsNullOrWhiteSpace(command.Description) ? null : command.Description.Trim(),
            CaloriesPer100g = command.CaloriesPer100g,
            ProteinsPer100g = command.ProteinsPer100g ?? 0,
            FatsPer100g = command.FatsPer100g ?? 0,
            CarbsPer100g = command.CarbsPer100g ?? 0,
            PortionGrams = command.PortionGrams,
            PortionHint = command.PortionHint,
            Category = command.Category,
            OwnerUserId = command.UserId,
            CreatedAtUtc = DateTime.UtcNow,
        };

        await _foodItemsRepository.CreateAsync(foodItem, cancellationToken).ConfigureAwait(false);
        return Result<FoodItemDto>.Success(foodItem.ToDto());
    }
}