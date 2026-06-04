using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.Features.Nutrition;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class GetActivityTypeQueryHandler : IRequestHandler<GetActivityTypeQuery, Result<ActivityTypeDto>>
{
    private readonly IActivityTypeRepository _activityTypesRepository;

    public GetActivityTypeQueryHandler(IActivityTypeRepository repository)
    {
        _activityTypesRepository = repository;
    }

    public async Task<Result<ActivityTypeDto>> Handle(GetActivityTypeQuery request, CancellationToken cancellationToken)
    {
        var activityType = await _activityTypesRepository.GetByIdAsNoTrackingAsync(request.ActivityTypeId, cancellationToken).ConfigureAwait(false);
        if (activityType is null)
        {
            return Result<ActivityTypeDto>.Failure(new Error(ActivityErrorCodes.EntryNotFound, "Тип активности не найден."));
        }
        return Result<ActivityTypeDto>.Success(activityType.ToDto());
    }
}