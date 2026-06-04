using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class PatchActivityTypeCommandHandler : IRequestHandler<PatchActivityTypeCommand, Result<ActivityTypeDto>>
{
    private readonly IActivityTypeRepository _activityTypesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PatchActivityTypeCommandHandler(IActivityTypeRepository activityTypesRepository, IUnitOfWork unitOfWork)
    {
        _activityTypesRepository = activityTypesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ActivityTypeDto>> Handle(PatchActivityTypeCommand command, CancellationToken cancellationToken)
    {
        var activityType = await _activityTypesRepository.GetByIdAsync(command.ActivityTypeId, cancellationToken).ConfigureAwait(false);
        if (activityType == null)
        {
            return Result<ActivityTypeDto>.Failure(new Error(ActivityErrorCodes.ActivityTypeNotFound, "Activity not found."));
        }

        if (!string.IsNullOrWhiteSpace(command.Name))
        {
            activityType.Name = command.Name.Trim();
        }

        if (!string.IsNullOrWhiteSpace(command.Description))
        {
            activityType.Description = command.Description.Trim();
        }

        if (command.CaloriesPerHour.HasValue)
        {
            activityType.CaloriesPerHour = command.CaloriesPerHour.Value;
        }

        if (command.CaloriesPer100Reps.HasValue)
        {
            activityType.CaloriesPer100Reps = command.CaloriesPer100Reps.Value;
        }

        if (!string.IsNullOrWhiteSpace(command.Category))
        {
            activityType.Category = command.Category.Trim();
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return Result<ActivityTypeDto>.Success(activityType.ToDto());
    }
}