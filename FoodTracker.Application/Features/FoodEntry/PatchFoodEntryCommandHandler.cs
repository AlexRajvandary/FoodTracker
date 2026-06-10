using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Application.Features.Nutrition;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.FoodEntry
{
    public sealed class PatchFoodEntryCommandHandler : IRequestHandler<PatchFoodEntryCommand, Result<FoodEntryDto>>
    {
        private readonly IFoodEntryRepository _foodEntries;
        private readonly IFoodItemRepository _foodItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatchFoodEntryCommandHandler(IFoodEntryRepository foodEntries, IFoodItemRepository foodItemRepository, IUnitOfWork unitOfWork)
        {
            _foodEntries = foodEntries;
            _foodItemRepository = foodItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<FoodEntryDto>> Handle(PatchFoodEntryCommand request, CancellationToken cancellationToken)
        {
            var foodEntry = await _foodEntries.GetOwnedAsync(request.UserId, request.FoodItemId, cancellationToken).ConfigureAwait(false);

            if (foodEntry is null || foodEntry.UserId != request.UserId)
            {
                return Result<FoodEntryDto>.Failure(new Error(FoodErrorCodes.FoodEntryNotFound, "Food entry not found."));
            }

            var foodItem = await _foodItemRepository.GetByIdAsNoTrackingAsync(foodEntry.FoodId, cancellationToken).ConfigureAwait(false);

            if (foodItem is null)
            {
                return Result<FoodEntryDto>.Failure(new Error(FoodErrorCodes.FoodItemNotFound, "Food item not found."));
            }

            if (request.ConsumedAtUtc.HasValue)
            {
                foodEntry.ConsumedAtUtc = request.ConsumedAtUtc.Value;
            }

            if (request.GramsConsumed.HasValue)
            {
                var multiplier = request.GramsConsumed.Value / 100d;
                foodEntry.GramsConsumed = request.GramsConsumed.Value;
                foodEntry.Calories = foodItem.CaloriesPer100g * multiplier;
                foodEntry.Proteins = foodItem.ProteinsPer100g * multiplier;
                foodEntry.Fats = foodItem.FatsPer100g * multiplier;
                foodEntry.Carbs = foodItem.CarbsPer100g * multiplier;
                foodEntry.Fiber = foodItem.FiberPer100g * multiplier;
                foodEntry.Salt = foodItem.SaltPer100g * multiplier;
                foodEntry.SaturatedFat = foodItem.SaturatedFatPer100g * multiplier;
                foodEntry.Sugars = foodItem.SugarsPer100g * multiplier;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<FoodEntryDto>.Success(foodEntry.ToDto());
        }
    }
}