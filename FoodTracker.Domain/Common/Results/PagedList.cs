namespace FoodTracker.Domain.Common.Results;

public sealed class PagedList<T>(IReadOnlyList<T> items, int page, int pageSize, int totalCount)
{
    public IReadOnlyList<T> Items { get; init; } = items;
    public int Page { get; init; } = page;
    public int PageSize { get; init; } = pageSize;
    public int TotalCount { get; init; } = totalCount;
}