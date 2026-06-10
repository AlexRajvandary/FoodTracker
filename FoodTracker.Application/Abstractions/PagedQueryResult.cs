namespace FoodTracker.Application.Abstractions;

public sealed class PagedQueryResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];
    public int TotalCount { get; init; }
}