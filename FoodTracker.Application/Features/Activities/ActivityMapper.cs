using FoodTracker.Domain.Activities;

namespace FoodTracker.Application.Features.Activities;

internal static class ActivityMapper
{
    public static ActivityTypeDto ToDto(this ActivityType x) =>
        new()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            CaloriesPerHour = x.CaloriesPerHour,
            CaloriesPer100Reps = x.CaloriesPer100Reps,
        };

    public static ActivityEntryDto ToDto(this ActivityEntry x) =>
        new()
        {
            Id = x.Id,
            ActivityTypeId = x.ActivityTypeId,
            ActivityName = x.ActivityName,
            OccurredAt = x.OccurredAtUtc,
            DurationMinutes = x.DurationMinutes,
            RepetitionsCount = x.RepetitionsCount,
            CaloriesBurned = x.CaloriesBurned,
        };
}
