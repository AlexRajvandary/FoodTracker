namespace FoodTracker.Api.Contracts;

public sealed class FoodCatalogRequest
{
    public string? Brand { get; init; }
    public IReadOnlyList<Guid> CategoryIds { get; init; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Query { get; init; }
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; }
}