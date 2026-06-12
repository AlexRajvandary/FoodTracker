using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodCategoriesQuery : IRequest<Result<IReadOnlyList<FoodCategoryWithItemsCountDto>>>
{
}