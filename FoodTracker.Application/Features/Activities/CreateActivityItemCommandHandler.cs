using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Activities;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class CreateActivityTypeCommandHandler : IRequestHandler<CreateActivityTypeCommand, Result<ActivityTypeDto>>
{
    private readonly IActivityTypeRepository _activityTypes;

    public CreateActivityTypeCommandHandler(IActivityTypeRepository types)
    {
        _activityTypes = types;
    }

    public async Task<Result<ActivityTypeDto>> Handle(CreateActivityTypeCommand request, CancellationToken cancellationToken)
    {
        var activityType = new ActivityType
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CaloriesPerHour = request.CaloriesPerHour,
            CaloriesPer100Reps = request.CaloriesPer100Reps,
            Category = request.Category,
            CreatedAtUtc = DateTime.UtcNow,
        };

        await _activityTypes.CreateAsync(activityType, cancellationToken).ConfigureAwait(false);
        return Result<ActivityTypeDto>.Success(activityType.ToDto());
    }
}