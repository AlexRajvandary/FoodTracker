using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.Features.Nutrition;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class DeleteActivityTypeCommandHandler : IRequestHandler<DeleteActivityTypeCommand, Result>
{
    private readonly IActivityTypeRepository _activityTypes;

    public DeleteActivityTypeCommandHandler(IActivityTypeRepository activityTypes)
    {
        _activityTypes = activityTypes;
    }

    public async Task<Result> Handle(DeleteActivityTypeCommand request, CancellationToken cancellationToken)
    {
        var activityType = await _activityTypes.GetByIdAsync(request.ActivityTypeId, cancellationToken).ConfigureAwait(false);
        if (activityType is null)
        {
            return Result.Failure(new Error(FoodErrorCodes.EntryNotFound, "Запись не найдена."));
        }

        await _activityTypes.DeleteAsync(activityType, cancellationToken).ConfigureAwait(false);
        return Result.Success();
    }
}