using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.FoodEntry
{
    public sealed class DeleteFoodEntryCommand : IRequest<Result>
    {
        public Guid FoodEntryId { get; set; }
        public Guid UserId { get; init; }
    }
}