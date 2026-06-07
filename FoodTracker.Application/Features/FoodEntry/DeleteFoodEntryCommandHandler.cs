using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.Features.Nutrition;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.FoodEntry
{
    public class DeleteFoodEntryCommandHandler : IRequestHandler<DeleteFoodEntryCommand, Result>
    {
        private readonly IFoodEntryRepository _foodEntries;

        public DeleteFoodEntryCommandHandler(IFoodEntryRepository foodEntries)
        {
            _foodEntries = foodEntries;
        }

        public async Task<Result> Handle(DeleteFoodEntryCommand request, CancellationToken cancellationToken)
        {
            var foodEntry = await _foodEntries.GetOwnedAsync(request.UserId, request.FoodEntryId, cancellationToken).ConfigureAwait(false);
            if (foodEntry is null || foodEntry.UserId != request.UserId)
            {
                return Result.Failure(new Error(FoodErrorCodes.EntryNotFound, "Food entry not found."));
            }

            await _foodEntries.DeleteAsync(foodEntry, cancellationToken).ConfigureAwait(false);
            return Result.Success();
        }
    }
}