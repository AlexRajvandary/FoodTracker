using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Activities;

namespace FoodTracker.Application.Features.Activities;

internal static class ActivityMapper
{
    public static ActivityTypeShortDto ToShortDto(this ActivityType activityType) =>
    new()
    {
        Id = activityType.Id,
        Name = activityType.Name,
        Category = activityType.Category,
        CaloriesPer100Reps = activityType.CaloriesPer100Reps,
        CaloriesPerHour = activityType.CaloriesPerHour
    };


    public static ActivityTypeDto ToDto(this ActivityType activityType) =>
    new()
    {
        Id = activityType.Id,
        Name = activityType.Name,
        Category = activityType.Category,
        CaloriesPer100Reps = activityType.CaloriesPer100Reps,
        CaloriesPerHour = activityType.CaloriesPerHour,
        Description = activityType.Description
    };
}