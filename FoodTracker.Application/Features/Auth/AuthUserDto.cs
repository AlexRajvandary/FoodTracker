namespace FoodTracker.Application.Features.Auth;

public class AuthUserDto
{
    public string Id { get; init; } = string.Empty;
    public string? Email { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string? LastName { get; init; }
    public string Role { get; init; } = "user";
    public string? AvatarUrl { get; init; }
}
