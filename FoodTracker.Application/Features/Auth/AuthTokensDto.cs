namespace FoodTracker.Application.Features.Auth;

public class AuthTokensDto
{
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
    public AuthUserDto User { get; init; } = null!;
}
