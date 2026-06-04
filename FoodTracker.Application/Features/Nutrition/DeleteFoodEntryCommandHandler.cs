using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class DeleteFoodEntryCommandHandler : IRequestHandler<DeleteFoodEntryCommand, Result>
{
    private readonly IFoodEntryRepository _entries;

    public DeleteFoodEntryCommandHandler(IFoodEntryRepository entries)
    {
        _entries = entries;
    }

    public async Task<Result> Handle(DeleteFoodEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = await _entries.GetOwnedAsync(request.UserId, request.EntryId, cancellationToken).ConfigureAwait(false);
        if (entry is null)
        {
            return Result.Failure(new Error(FoodErrorCodes.EntryNotFound, "Запись не найдена."));
        }

        await _entries.DeleteAsync(entry, cancellationToken).ConfigureAwait(false);
        return Result.Success();
    }
}
