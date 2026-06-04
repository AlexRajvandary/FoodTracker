using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class DeleteActivityEntryCommandHandler : IRequestHandler<DeleteActivityEntryCommand, Result>
{
    private readonly IActivityEntryRepository _entries;

    public DeleteActivityEntryCommandHandler(IActivityEntryRepository entries)
    {
        _entries = entries;
    }

    public async Task<Result> Handle(DeleteActivityEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = await _entries.GetOwnedAsync(request.UserId, request.EntryId, cancellationToken).ConfigureAwait(false);
        if (entry is null)
        {
            return Result.Failure(new Error(ActivityErrorCodes.EntryNotFound, "Запись не найдена."));
        }

        await _entries.DeleteAsync(entry, cancellationToken).ConfigureAwait(false);
        return Result.Success();
    }
}
