using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Application.Features.Nutrition;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.FoodEntry
{
    public class CreateFoodEntryCommandHandler : IRequestHandler<CreateFoodEntryCommand, Result<FoodEntryDto>>
    {
        private readonly IFoodEntryRepository _foodEntryRepository;
        private readonly IFoodItemRepository _foodItemRepository;

        public CreateFoodEntryCommandHandler(IFoodEntryRepository foodEntryRepository, IFoodItemRepository foodItemRepository)
        {
            _foodEntryRepository = foodEntryRepository;
            _foodItemRepository = foodItemRepository;
        }

        public async Task<Result<FoodEntryDto>> Handle(CreateFoodEntryCommand command, CancellationToken cancellationToken)
        {
            var foodItem = await _foodItemRepository.GetByIdAsync(command.FoodId, cancellationToken);

            if (foodItem is null)
            {
                return Result<FoodEntryDto>.Failure(new Error(FoodErrorCodes.FoodItemNotFound, $"Food item with ID {command.FoodId} was not found."));
            }

            double gramsConsumed;

            if (command.GramsConsumed is not null)
            {
                gramsConsumed = command.GramsConsumed.Value;
            }
            else if (command.PortionsConsumed is null)
            {
                return Result<FoodEntryDto>.Failure(new Error(FoodErrorCodes.MissingConsumptionAmount, $"Either {nameof(command.GramsConsumed)} or {nameof(command.PortionsConsumed)} must be specified."));
            }
            else if (foodItem.PortionGrams is null)
            {
                return Result<FoodEntryDto>.Failure(new Error(FoodErrorCodes.MissingPortionSize, $"Food item '{foodItem.Name}' does not have a portion size configured."));
            }
            else
            {
                gramsConsumed = command.PortionsConsumed.Value * foodItem.PortionGrams.Value;
            }

            var foodEntry = new Domain.Nutrition.FoodEntry
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                FoodId = command.FoodId,
                ConsumedAtUtc = command.ConsumedAtUtc,
                GramsConsumed = gramsConsumed
            };

            var result = await _foodEntryRepository.AddAsync(foodEntry, cancellationToken);
            return Result<FoodEntryDto>.Success(result.ToDto());
        }
    }
}