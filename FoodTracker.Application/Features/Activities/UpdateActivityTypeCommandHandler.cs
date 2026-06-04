using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class UpdateActivityTypeCommandHandler : IRequestHandler<UpdateActivityTypeCommand, Result<ActivityTypeDto>>
{
    private readonly IActivityTypeRepository _activityTypesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateActivityTypeCommandHandler(IActivityTypeRepository activityTypesRepository, IUnitOfWork unitOfWork)
    {
        _activityTypesRepository = activityTypesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ActivityTypeDto>> Handle(UpdateActivityTypeCommand command, CancellationToken cancellationToken)
    {
        var activityType = await _activityTypesRepository
            .GetByIdAsync(command.ActivityTypeId, cancellationToken)
            .ConfigureAwait(false);

        if (activityType is null)
        {
            return Result<ActivityTypeDto>.Failure(new Error(ActivityErrorCodes.ActivityTypeNotFound, "Activity not found."));
        }

        activityType.Name = command.Name.Trim();
        activityType.Description = string.IsNullOrWhiteSpace(command.Description)
            ? null
            : command.Description.Trim();

        activityType.CaloriesPerHour = command.CaloriesPerHour;
        activityType.CaloriesPer100Reps = command.CaloriesPer100Reps;
        activityType.Category = command.Category;
        activityType.UpdatedAtUtc = DateTime.UtcNow;

        await _unitOfWork
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result<ActivityTypeDto>.Success(activityType.ToDto());
    }
}